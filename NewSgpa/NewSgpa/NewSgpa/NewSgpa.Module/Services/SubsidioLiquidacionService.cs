using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// Internal data accumulated during subsidio calculation for one afiliado.
/// Replaces VB6 Type tAfiliados.
/// </summary>
public class SubsidioAfiliadoData
{
    public long CI { get; set; }
    public double ValorJornal { get; set; }
    public int DiasCertificados { get; set; }
    public double ImpNominal { get; set; }
    public double ImpAguinaldo { get; set; }
    public double ImpLiquido { get; set; }
    public double CoefAporteNominal { get; set; } = 1;
    public double CoefAporteAguinaldo { get; set; } = 1;
    public double ImpNominalSeguro { get; set; }
    public double ImpAguinaldoSeguro { get; set; }
    public double ImpNominalCasemed { get; set; }
    public double ImpAguinaldoCasemed { get; set; }
}

/// <summary>
/// Merged certification period used during liquidation.
/// Replaces VB6 CertificacionesTmp table.
/// </summary>
public class CertificacionMerged
{
    public long CI { get; set; }
    public DateTime FechaIni { get; set; }
    public DateTime FechaFin { get; set; }
    public double ImporteDeducible { get; set; }
    public int? CodSalidaTipo { get; set; }
}

/// <summary>
/// Result of a subsidio liquidation run.
/// </summary>
public class LiquidacionResult
{
    public bool Success { get; set; }
    public int Processed { get; set; }
    public string? ErrorMessage { get; set; }
    public long? LastCIProcessed { get; set; }
}

/// <summary>
/// Core subsidio liquidation engine.
/// Migrated from VB6 frmLiquidaSubsidio: LiquidarSubsidio, ValorJornal,
/// ProcesarCertificaciones, ProcesarItems, GenerarCertificaciones, etc.
/// </summary>
public class SubsidioLiquidacionService(NewSgpaEFCoreDbContext db)
{
    // Business constants (from VB6 Bcpart.bas)
    private const int GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO = 0;
    private const int GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL = 1000;
    private const int SalidaTipoInternado = 5;
    private const int SalidaTipoBSE = 7;
    private const int DiasDescuentoBPS = 4;
    private const int DiasDescuentoBSE = 5;
    private const double PctAportes = 0.181;

    private double _smn;
    private double _topeLiquidoBPS;
    private double _topeJubilatorio;
    private int _codCasemed;

    /// <summary>
    /// Main liquidation entry point.
    /// Replaces VB6 LiquidarSubsidio function.
    /// </summary>
    /// <param name="mes">Period as YYYYMM</param>
    /// <param name="liquidar">True for definitive, false for simulation</param>
    /// <param name="ciFilter">Optional: process only this CI</param>
    /// <param name="genNroRecibo">Generate receipt numbers</param>
    /// <param name="usuario">Current user login</param>
    public async Task<LiquidacionResult> LiquidarAsync(
        int mes, bool liquidar, long? ciFilter, bool genNroRecibo, string usuario)
    {
        var result = new LiquidacionResult();

        try
        {
            await LoadParametersAsync();

            int anio = mes / 100;
            int mesNum = mes % 100;
            int mesIni = CalcularMesInicio(mes);
            int mesIniImp = CalcularMesInicioImponible(mes);

            // Delete previous data for this period
            await DeletePreviousDataAsync(anio, mesNum, liquidar, ciFilter);

            // Get afiliados with active certifications for this period
            var afiliados = await GetAfiliadosConCertificaciones(mes, ciFilter);

            int processed = 0;
            using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                foreach (var ci in afiliados)
                {
                    result.LastCIProcessed = ci;

                    var certMerged = GenerarCertificacionesMerged(ci, mes);
                    if (certMerged.Count == 0)
                    {
                        processed++;
                        continue;
                    }

                    foreach (var cert in certMerged)
                    {
                        var afiliadoData = new SubsidioAfiliadoData { CI = ci };

                        // Create SubsidioCabezal
                        var cabezal = new SubsidioCabezal
                        {
                            CI = ci,
                            Mes = (byte)mesNum,
                            Anio = anio,
                            Liquidar = liquidar,
                            Usr = usuario,
                            Ts = DateTime.Now
                        };

                        if (liquidar && genNroRecibo)
                            cabezal.NroRecibo = await GetProxNroReciboAsync();

                        db.SubsidioCabezales.Add(cabezal);
                        await db.SaveChangesAsync();

                        var empresaImponibles = new List<SubsidioCabezalEmpresa>();

                        // Calculate ValorJornal
                        await CalcularValorJornal(cabezal, mes, mesIni, mesIniImp,
                            afiliadoData, liquidar, empresaImponibles, usuario);

                        // Process certifications â†’ calculate days & nominal
                        ProcesarCertificaciones(cert, cabezal, mes, afiliadoData,
                            liquidar, empresaImponibles);

                        // Process items (retentions, deductions)
                        await ProcesarItemsAsync(cabezal, afiliadoData, mes, empresaImponibles, usuario);

                        // Update cabezal with calculated values
                        cabezal.ImpNominal = Round3(afiliadoData.ImpNominal);
                        cabezal.ImpAguinaldo = Round3(afiliadoData.ImpAguinaldo);
                        cabezal.ImpLiquido = Round3(afiliadoData.ImpLiquido);
                        cabezal.ValorJornal = (float)Round3(afiliadoData.ValorJornal);
                        cabezal.Dias = afiliadoData.DiasCertificados;
                        cabezal.Usr = usuario;
                        cabezal.Ts = DateTime.Now;

                        // Save empresa breakdowns
                        foreach (var ei in empresaImponibles)
                        {
                            ei.IdSubsidio = cabezal.IdSubsidio;
                            db.SubsidioCabezalEmpresas.Add(ei);
                        }

                        await db.SaveChangesAsync();

                        // Insert BPS record
                        await InsertarBpsAsync(cabezal);
                    }

                    processed++;
                }

                await transaction.CommitAsync();
                result.Success = true;
                result.Processed = processed;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    private async Task LoadParametersAsync()
    {
        var param = await db.Parametros.FirstOrDefaultAsync()
            ?? throw new InvalidOperationException("No se encontraron parÃ¡metros del sistema.");

        _smn = param.SMN ?? 0;
        _topeLiquidoBPS = param.TopeLiquidoBPS ?? 0;
        _topeJubilatorio = param.TopeJubilatorio ?? 0;
        _codCasemed = 1; // Default, should come from user params
    }

    private async Task DeletePreviousDataAsync(int anio, int mes, bool liquidar, long? ciFilter)
    {
        var query = db.SubsidioCabezales
            .Where(s => s.Anio == anio && s.Mes == mes && s.Liquidar == liquidar);

        if (ciFilter.HasValue)
            query = query.Where(s => s.CI == ciFilter.Value);

        var ids = await query.Select(s => s.IdSubsidio).ToListAsync();
        if (ids.Count == 0) return;

        // Delete children first
        await db.SubsidioEnfermedades.Where(e => ids.Contains(e.IdSubsidio ?? 0)).ExecuteDeleteAsync();
        await db.SubsidioItems.Where(i => ids.Contains(i.IdSubsidio ?? 0)).ExecuteDeleteAsync();
        await db.SubsidioCabezalEmpresas.Where(e => ids.Contains(e.IdSubsidio ?? 0)).ExecuteDeleteAsync();
        await db.SubsidioCabezalesBps.Where(b => ids.Contains(b.IdSubsidio ?? 0)).ExecuteDeleteAsync();
        await db.SubsidioItemEmpresas.Where(e => ids.Contains(e.IdSubsidio ?? 0)).ExecuteDeleteAsync();
        await query.ExecuteDeleteAsync();
    }

    private async Task<List<long>> GetAfiliadosConCertificaciones(int mes, long? ciFilter)
    {
        int anio = mes / 100;
        int mesNum = mes % 100;
        var primerDia = new DateTime(anio, mesNum, 1);
        var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

        var query = db.Certificaciones.AsNoTracking()
            .Where(c => c.Efectiva && c.FechaIni <= ultimoDia && c.FechaFin >= primerDia);

        if (ciFilter.HasValue)
            query = query.Where(c => c.CI == ciFilter.Value);

        return await query.Select(c => c.CI ?? 0).Where(ci => ci != 0).Distinct().ToListAsync();
    }

    /// <summary>
    /// Merges consecutive certifications for a CI in a period.
    /// Replaces VB6 GenerarCertificaciones.
    /// </summary>
    private List<CertificacionMerged> GenerarCertificacionesMerged(long ci, int mes)
    {
        int anio = mes / 100;
        int mesNum = mes % 100;
        var primerDia = new DateTime(anio, mesNum, 1);
        var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

        var certs = db.Certificaciones.AsNoTracking()
            .Where(c => c.CI == ci && c.Efectiva && c.FechaIni <= ultimoDia && c.FechaFin >= primerDia)
            .OrderBy(c => c.FechaIni)
            .ToList();

        var merged = new List<CertificacionMerged>();

        foreach (var cert in certs)
        {
            if (merged.Count == 0)
            {
                merged.Add(new CertificacionMerged
                {
                    CI = ci,
                    FechaIni = cert.FechaIni ?? primerDia,
                    FechaFin = cert.FechaFin ?? ultimoDia,
                    ImporteDeducible = cert.ImporteDeducible ?? 0,
                    CodSalidaTipo = cert.CodSalidaTipo
                });
            }
            else
            {
                var last = merged[^1];
                // If consecutive day, merge
                if ((cert.FechaIni!.Value - last.FechaFin).Days == 1)
                {
                    last.FechaFin = cert.FechaFin ?? ultimoDia;
                    last.ImporteDeducible += cert.ImporteDeducible ?? 0;
                    last.CodSalidaTipo = cert.CodSalidaTipo;
                }
                else
                {
                    merged.Add(new CertificacionMerged
                    {
                        CI = ci,
                        FechaIni = cert.FechaIni ?? primerDia,
                        FechaFin = cert.FechaFin ?? ultimoDia,
                        ImporteDeducible = cert.ImporteDeducible ?? 0,
                        CodSalidaTipo = cert.CodSalidaTipo
                    });
                }
            }
        }

        return merged;
    }

    /// <summary>
    /// Calculates the daily wage (valor jornal) from imponible history.
    /// Replaces VB6 ValorJornal sub.
    /// </summary>
    private async Task CalcularValorJornal(
        SubsidioCabezal cabezal, int mes, int mesIni, int mesIniImp,
        SubsidioAfiliadoData data, bool liquidar,
        List<SubsidioCabezalEmpresa> empresaImponibles, string usuario)
    {
        long ci = data.CI;
        int mesFin = AddMonth(-1, mes);

        // Get average imponible per empresa (excluding Casemed)
        var empresaPromedios = await db.Imponibles.AsNoTracking()
            .Where(i => i.CI == ci
                        && i.AnioMes >= mesIniImp && i.AnioMes <= mesFin
                        && i.CodEmpresa != _codCasemed)
            .GroupBy(i => i.CodEmpresa)
            .Where(g => g.Count() >= 3) // At least 3 months
            .Select(g => new { CodEmpresa = g.Key, Promedio = g.Average(x => x.Importe ?? 0) / 30.0 })
            .ToListAsync();

        double totalJornal = 0;
        foreach (var ep in empresaPromedios)
        {
            totalJornal += Round3(ep.Promedio);
            empresaImponibles.Add(new SubsidioCabezalEmpresa
            {
                CodEmpresa = ep.CodEmpresa,
                ValorJornal = (float)Round3(ep.Promedio),
                Usr = usuario,
                Ts = DateTime.Now
            });
        }

        // Casemed separately (at least 1 month)
        var casemedPromedio = await db.Imponibles.AsNoTracking()
            .Where(i => i.CI == ci
                        && i.AnioMes >= mesIniImp && i.AnioMes <= mesFin
                        && i.CodEmpresa == _codCasemed)
            .GroupBy(i => i.CodEmpresa)
            .Where(g => g.Count() >= 1)
            .Select(g => new { CodEmpresa = g.Key, Promedio = g.Average(x => x.Importe ?? 0) / 30.0 })
            .FirstOrDefaultAsync();

        if (casemedPromedio != null)
        {
            totalJornal += Round3(casemedPromedio.Promedio);
            empresaImponibles.Add(new SubsidioCabezalEmpresa
            {
                CodEmpresa = _codCasemed,
                ValorJornal = (float)Round3(casemedPromedio.Promedio),
                Usr = usuario,
                Ts = DateTime.Now
            });
        }

        data.ValorJornal = totalJornal;
    }

    /// <summary>
    /// Processes certifications to calculate days and nominal amounts.
    /// Replaces VB6 ProcesarCertificaciones.
    /// </summary>
    private void ProcesarCertificaciones(
        CertificacionMerged cert, SubsidioCabezal cabezal, int mes,
        SubsidioAfiliadoData data, bool liquidar,
        List<SubsidioCabezalEmpresa> empresaImponibles)
    {
        int anio = mes / 100;
        int mesNum = mes % 100;
        var primerDia = new DateTime(anio, mesNum, 1);
        var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

        // Clamp dates to current month
        var fechaIni = cert.FechaIni < primerDia ? primerDia : cert.FechaIni;
        var fechaFin = cert.FechaFin > ultimoDia ? ultimoDia : cert.FechaFin;

        int dias = (fechaFin - fechaIni).Days + 1;

        // Discount initial days for BPS (first 4 days for normal, 5 for BSE)
        int diasDescuento = cert.CodSalidaTipo == SalidaTipoBSE ? DiasDescuentoBSE : DiasDescuentoBPS;

        // Only discount if this is the first period of the certification
        if (cert.FechaIni >= primerDia)
        {
            dias = Math.Max(0, dias - diasDescuento);
        }

        data.DiasCertificados = dias;
        data.ImpNominal = Round3(data.ValorJornal * dias);
        data.ImpAguinaldo = Round3(data.ImpNominal / 12.0);

        // Save enfermedad record
        db.SubsidioEnfermedades.Add(new SubsidioEnfermedad
        {
            IdSubsidio = cabezal.IdSubsidio,
            FechaIni = cert.FechaIni,
            FechaFin = cert.FechaFin,
            FechaIniSubsidio = fechaIni,
            FechaFinSubsidio = fechaFin
        });

        // Distribute nominal per empresa
        if (data.ValorJornal > 0)
        {
            foreach (var ei in empresaImponibles)
            {
                double proportion = (ei.ValorJornal ?? 0) / data.ValorJornal;
                ei.Dias = dias;
                ei.ImpNominal = Round3(data.ImpNominal * proportion);
                ei.ImpAguinaldo = Round3(data.ImpAguinaldo * proportion);
            }
        }
    }

    /// <summary>
    /// Processes deduction/retention items based on SubsidioItemCod configuration.
    /// Replaces VB6 ProcesarItems.
    /// </summary>
    private async Task ProcesarItemsAsync(
        SubsidioCabezal cabezal, SubsidioAfiliadoData data, int mes,
        List<SubsidioCabezalEmpresa> empresaImponibles, string usuario)
    {
        int anio = mes / 100;
        int mesNum = mes % 100;
        var fecha = new DateTime(anio, mesNum, 1);

        // Get item codes with possible per-afiliado overrides
        var itemCods = await db.SubsidioItemCods.AsNoTracking()
            .Where(s => s.Procesar && (s.FechaBaja == null || s.FechaBaja > fecha)
                        && (s.FechaVigencia == null || s.FechaVigencia <= fecha))
            .OrderBy(s => s.Orden)
            .ToListAsync();

        // Per-afiliado overrides
        var overrides = await db.SubsidioItemCodAfiliados.AsNoTracking()
            .Where(s => s.CI == data.CI)
            .ToDictionaryAsync(s => s.CodSubsidioItemCod ?? 0, s => s.Valor ?? 0);

        double totalDeductions = 0;

        foreach (var item in itemCods)
        {
            double valor = overrides.GetValueOrDefault(item.CodSubsidioItemCod, 0);
            // If no override, use default from item config
            // (simplified â€” full logic would need TipoComp, Comparar, etc.)

            double impComp = item.CodSubsidioItemCod switch
            {
                // CompararContra: 1=Nominal, 2=Aguinaldo, 3=Both
                _ => data.ImpNominal + data.ImpAguinaldo
            };

            double impCalculado = CalculateItemValue(item, impComp, data);

            if (impCalculado != 0)
            {
                db.SubsidioItems.Add(new SubsidioItem
                {
                    IdSubsidio = cabezal.IdSubsidio,
                    CodSubsidioItemCod = item.CodSubsidioItemCod,
                    Importe = (float)Round3(impCalculado)
                });

                if (item.ModificaNominal)
                {
                    data.ImpNominal -= Round3(impCalculado) * (item.Signo ?? 1);
                    data.ImpAguinaldo = data.ImpNominal / 12.0;
                }
                else
                {
                    totalDeductions += impCalculado;
                }
            }
        }

        // Jubilatory contribution (obrero)
        double aporteJubObrero = Round3(
            (Math.Min(data.ImpNominal, _topeJubilatorio) * GetSubsidioItemValue(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)) +
            (Math.Min(data.ImpAguinaldo, _topeJubilatorio / 2) * GetSubsidioItemValue(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO)));

        db.SubsidioItems.Add(new SubsidioItem
        {
            IdSubsidio = cabezal.IdSubsidio,
            CodSubsidioItemCod = GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_OBRERO,
            Importe = (float)aporteJubObrero
        });
        totalDeductions += aporteJubObrero;

        // Jubilatory contribution (patronal)
        double aporteJubPatronal = Round3(
            (Math.Min(data.ImpNominal, _topeJubilatorio) * GetSubsidioItemValue(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL)) +
            (Math.Min(data.ImpAguinaldo, _topeJubilatorio / 2) * GetSubsidioItemValue(GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL)));

        db.SubsidioItems.Add(new SubsidioItem
        {
            IdSubsidio = cabezal.IdSubsidio,
            CodSubsidioItemCod = GC_SUBSIDIOITEMCOD_APORTEJUBILATORIO_PATRONAL,
            Importe = (float)aporteJubPatronal
        });

        // Coefficient for empresa breakdown
        data.CoefAporteNominal = data.ImpNominal > _topeJubilatorio
            ? _topeJubilatorio / data.ImpNominal : 1;
        data.CoefAporteAguinaldo = data.ImpAguinaldo > _topeJubilatorio / 2
            ? (_topeJubilatorio / 2) / data.ImpAguinaldo : 1;

        // Calculate liquid
        data.ImpLiquido = data.ImpNominal + data.ImpAguinaldo - totalDeductions;
    }

    /// <summary>
    /// Calculates item value based on configuration.
    /// Replaces VB6 item processing loop with operator comparisons.
    /// </summary>
    private double CalculateItemValue(SubsidioItemCod item, double impComp, SubsidioAfiliadoData data)
    {
        double valor = 0;
        double valorMin = item.ValorMin ?? 0;
        double valorMax = item.ValorMax ?? 0;

        // Apply SMN multiplier if configured
        // (simplified â€” in full version, check TipoComp field)

        bool procesar = true;
        if (!string.IsNullOrEmpty(item.Operador))
        {
            procesar = item.Operador switch
            {
                "<" => impComp < valorMax,
                ">" => impComp > valorMin,
                "<=" => impComp <= valorMax,
                ">=" => impComp >= valorMin,
                "><" => impComp > valorMin && impComp < valorMax,
                ">=<=" => impComp >= valorMin && impComp <= valorMax,
                _ => impComp >= valorMin && impComp <= valorMax
            };
        }

        if (!procesar) return 0;

        // ValorTipo determines calculation: %, *, +, -, or fixed
        // (simplified â€” would need ValorTipo field in SubsidioItemCod)
        // Default: percentage of comparison base
        valor = impComp * (valor / 100.0);

        return valor;
    }

    private double GetSubsidioItemValue(int codSubsidioItemCod)
    {
        return db.SubsidioItemCods.AsNoTracking()
            .Where(s => s.CodSubsidioItemCod == codSubsidioItemCod)
            .Select(s => (double?)(s.ValorMin ?? 0))
            .FirstOrDefault() ?? 0;
    }

    /// <summary>
    /// Inserts BPS record for a subsidio.
    /// Replaces VB6 InsertarBPS.
    /// </summary>
    private async Task InsertarBpsAsync(SubsidioCabezal cabezal)
    {
        if (cabezal.Dias == null || cabezal.Dias <= 0) return;

        int diasBps = Math.Max(0, (cabezal.Dias ?? 0) - DiasDescuentoBPS);
        double liquidoBps = Math.Round((cabezal.ValorJornal ?? 0) * diasBps * PctAportes, 3);
        double aguinaldoBps = Math.Round(liquidoBps / 12.0, 3);

        if (liquidoBps > _topeLiquidoBPS)
            liquidoBps = _topeLiquidoBPS;

        double liquidoPagar = (cabezal.ImpLiquido ?? 0) - liquidoBps - aguinaldoBps;
        if (liquidoPagar < 0) liquidoPagar = 0;

        db.SubsidioCabezalesBps.Add(new SubsidioCabezalBps
        {
            IdSubsidio = cabezal.IdSubsidio,
            DiasBPS = diasBps,
            LiquidoBPS = liquidoBps,
            AguinaldoBPS = aguinaldoBps,
            LiquidoPagar = liquidoPagar
        });

        await db.SaveChangesAsync();
    }

    private async Task<int> GetProxNroReciboAsync()
    {
        var max = await db.SubsidioCabezales
            .Where(s => s.NroRecibo != null)
            .MaxAsync(s => (int?)s.NroRecibo) ?? 0;
        return max + 1;
    }

    // Period calculation helpers (from VB6 AddMonth)
    private static int AddMonth(int months, int period)
    {
        int anio = period / 100;
        int mes = period % 100;
        var date = new DateTime(anio, mes, 1).AddMonths(months);
        return date.Year * 100 + date.Month;
    }

    private static int CalcularMesInicio(int mes)
        => AddMonth(-6, mes); // 6 months back

    private static int CalcularMesInicioImponible(int mes)
        => AddMonth(-6, mes);

    private static double Round3(double value)
        => Math.Round(value, 3, MidpointRounding.AwayFromZero);

    /// <summary>
    /// Verifies consistency of prior month's data before liquidation.
    /// Replaces VB6 frmLiquidaSubsidio.VerificarConsistencia.
    /// Checks that all SubsidioCabezal records from previous month
    /// have corresponding SubsidioEnfermedad records.
    /// </summary>
    public async Task VerificarConsistenciaAsync(int anio, int mes)
    {
        var fechaAnterior = new DateTime(anio, mes, 1).AddMonths(-1);
        int mesAnt = fechaAnterior.Month;
        int anioAnt = fechaAnterior.Year;

        bool hayInconsistencia = await db.SubsidioCabezales.AsNoTracking()
            .AnyAsync(s => s.Mes == mesAnt && s.Anio == anioAnt
                           && !db.SubsidioEnfermedades.Any(e => e.IdSubsidio == s.IdSubsidio));

        if (hayInconsistencia)
        {
            throw new InvalidOperationException(
                $"No se encontraron todos los registros del mes anterior ({mesAnt}/{anioAnt}). " +
                "No se puede procesar la liquidaciÃ³n hasta que se solucione.");
        }
    }

    /// <summary>
    /// Updates ImpLiquido from an external source (Excel import).
    /// Replaces VB6 frmLiquidaSubsidio.ActualizarLiquidos
    /// (query 506_Update_Liquidos).
    /// </summary>
    public async Task<int> ActualizarLiquidosAsync(
        Dictionary<int, double> liquidosPorIdSubsidio)
    {
        int updated = 0;

        foreach (var (idSubsidio, liquido) in liquidosPorIdSubsidio)
        {
            var cabezal = await db.SubsidioCabezales
                .FirstOrDefaultAsync(s => s.IdSubsidio == idSubsidio);

            if (cabezal != null)
            {
                cabezal.ImpLiquido = liquido;
                updated++;
            }
        }

        await db.SaveChangesAsync();
        return updated;
    }

    /// <summary>
    /// Loads BPS liquidation data (DiasBPS, LiquidoBPS, LiquidoPagar)
    /// for all subsidios in a period.
    /// Replaces VB6 frmLiquidaSubsidio.CargarLiquidacionBPS.
    /// </summary>
    public async Task<int> CargarLiquidacionBpsAsync(int mes, int anio, bool liquidar)
    {
        await LoadParametersAsync();

        var cabezales = await db.SubsidioCabezales
            .Where(s => s.Mes == mes && s.Anio == anio && s.Liquidar == liquidar)
            .Include(s => s.Bps)
            .ToListAsync();

        int count = 0;
        foreach (var cab in cabezales)
        {
            int dias = cab.Dias ?? 0;
            if (dias <= 0) continue;

            int diasBps = Math.Max(0, dias - DiasDescuentoBPS);
            double jornal70 = Round3((cab.ValorJornal ?? 0) * 0.70);
            double liquidoBps = Round3(jornal70 * diasBps);
            double aguinaldoBps = Round3(liquidoBps / 12.0);

            if (liquidoBps > _topeLiquidoBPS)
                liquidoBps = _topeLiquidoBPS;

            double liquidoPagar = (cab.ImpLiquido ?? 0) - liquidoBps - aguinaldoBps;
            if (liquidoPagar < 0) liquidoPagar = 0;

            if (cab.Bps != null)
            {
                cab.Bps.DiasBPS = diasBps;
                cab.Bps.LiquidoBPS = liquidoBps;
                cab.Bps.AguinaldoBPS = aguinaldoBps;
                cab.Bps.LiquidoPagar = liquidoPagar;
            }
            else
            {
                db.SubsidioCabezalesBps.Add(new SubsidioCabezalBps
                {
                    IdSubsidio = cab.IdSubsidio,
                    DiasBPS = diasBps,
                    LiquidoBPS = liquidoBps,
                    AguinaldoBPS = aguinaldoBps,
                    LiquidoPagar = liquidoPagar
                });
            }
            count++;
        }

        await db.SaveChangesAsync();
        return count;
    }
}


