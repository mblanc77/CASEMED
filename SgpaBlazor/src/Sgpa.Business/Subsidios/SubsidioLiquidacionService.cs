using Sgpa.Data;
using Sgpa.Domain.Security;

namespace Sgpa.Business.Subsidios;

/// <summary>
/// Orquestador de la liquidación de subsidios. Corre dentro de una transacción (atómico, como el
/// BeginTrans/Commit/Rollback del VB6). Esta etapa implementa el bucle de creación de cabezales +
/// <c>ValorJornal</c> (apoyado en los SPs acc_sgpa_300_*). Pendiente: ProcesarCertificaciones /
/// ProcesarItems (IRPF) / InsertarBPS.
/// </summary>
public sealed class SubsidioLiquidacionService : ISubsidioLiquidacionService
{
    // CodEmpresa de la línea de aporte que recibe el tratamiento "Casemed" en el cálculo del jornal:
    // se promedia con ≥1 mes (no ≥3 como los empleadores reales) y se divide por la ventana completa.
    // Es la empresa 900 = "SUBSIDIO POR ENF." (los subsidios previos del propio afiliado, que CASEMED
    // paga). En el VB6 viene de GetUsrParam("CodCasemed"); acá se lee de xUsrParam(clave='CodCasemed')
    // al liquidar (ver _codCasemed). Este valor es el fallback si la config no está.
    // OJO: NO es 902 ("CASEMED", Liquidar=0, que las queries filtran y nunca devuelven) — usar 902 hacía
    // que emp900 cayera al camino xEmpresa (≥3) y se excluyera cuando el afiliado tenía 1-2 meses de
    // subsidio, dando un ValorJornal menor que producción. Verificado: con 900 los casos cierran exacto.
    private const int DefaultCodCasemed = 900;

    private readonly IDbExecutor _db;
    private readonly ICurrentUser _user;

    // Resuelto desde xUsrParam al inicio de cada LiquidarAsync (config del proceso).
    private int _codCasemed = DefaultCodCasemed;

    public SubsidioLiquidacionService(IDbExecutor db, ICurrentUser user)
    {
        _db = db;
        _user = user;
    }

    public async Task<LiquidacionResultado> LiquidarAsync(int anio, int mes, bool liquidar, long? ci = null,
        bool genNroRecibo = false, IProgress<LiquidacionProgreso>? progreso = null,
        CancellationToken cancellationToken = default)
    {
        var periodo = new SubsidioPeriodo(anio, mes);
        var usr = _user.UserName;
        var avisos = new List<string>();

        // Todo el proceso es atómico: si algo falla, no queda nada a medio liquidar.
        await using var uow = await _db.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
        var repo = new SubsidioRepository(uow);

        // VerificarConsistencia (port): el mes anterior no debe tener cabezales sin su SubsidioEnfermedad
        // (síntoma de datos borrados/incompletos). Si los hay, abortar antes de tocar nada.
        var mesAnterior = new DateTime(anio, mes, 1).AddMonths(-1);
        if (await repo.ExistenCabezalesSinEnfermedadAsync(mesAnterior.Year, mesAnterior.Month, cancellationToken).ConfigureAwait(false))
            throw new InvalidOperationException(
                "No se encontraron todos los registros del mes anterior; no se puede procesar la liquidación hasta solucionarlo.");

        _codCasemed = await repo.GetCodCasemedAsync(cancellationToken).ConfigureAwait(false);
        var prm = await repo.GetParametrosAsync(cancellationToken).ConfigureAwait(false);

        var afiliados = await repo.SeleccionarAfiliadosAsync(periodo, liquidar, ci, cancellationToken).ConfigureAwait(false);
        await repo.BorrarPeriodoAsync(periodo, liquidar, ci, cancellationToken).ConfigureAwait(false);

        int procesados = 0, generados = 0;
        var total = afiliados.Count;
        progreso?.Report(new LiquidacionProgreso(0, total));
        foreach (var afiliado in afiliados)
        {
            procesados++;
            var certs = await repo.GenerarCertificacionesAsync(afiliado, periodo, cancellationToken).ConfigureAwait(false);
            for (int i = 0; i < certs.Count; i++)
            {
                var idSub = await repo.ProximoIdSubsidioAsync(cancellationToken).ConfigureAwait(false);
                await repo.InsertarCabezalAsync(idSub, afiliado, periodo, liquidar, usr, cancellationToken).ConfigureAwait(false);

                var acum = new LiquidacionAcumulador { CI = afiliado };
                await ValorJornalAsync(uow, repo, acum, idSub, afiliado, periodo, esPrimera: i == 0, liquidar,
                    certs[i].FechaIni, cancellationToken).ConfigureAwait(false);
                await ProcesarCertificacionesAsync(repo, acum, idSub, periodo, certs[i], cancellationToken).ConfigureAwait(false);
                await ProcesarItemsAsync(repo, acum, idSub, afiliado, periodo, prm, cancellationToken).ConfigureAwait(false);
                await InsertarBpsAsync(repo, acum, idSub, afiliado, periodo, prm, cancellationToken).ConfigureAwait(false);

                await repo.ActualizarCabezalMontosAsync(idSub, SubsidioMath.Rdo(acum.ValorJornal), acum.DiasCertif,
                    SubsidioMath.Rdo(acum.ImpNominal), SubsidioMath.Rdo(acum.ImpAguinaldo),
                    SubsidioMath.Rdo(acum.ImpLiquido), usr, cancellationToken).ConfigureAwait(false);
                generados++;
            }
            progreso?.Report(new LiquidacionProgreso(procesados, total));
        }

        // chkGenNroRecibo: asignar nros de recibo correlativos a los cabezales recién generados,
        // arrancando en MAX(NroRecibo)+1 (equivale a llamar GetProxNroRecibo por cada cabezal del VB6).
        if (genNroRecibo)
            await repo.GenerarNrosReciboAsync(periodo, liquidar, ci, cancellationToken).ConfigureAwait(false);

        await uow.CommitAsync(cancellationToken).ConfigureAwait(false);
        return new LiquidacionResultado(procesados, generados, avisos);
    }

    /// <summary>Port de ValorJornal: determina el valor del jornal y arma el detalle por empresa.</summary>
    private async Task ValorJornalAsync(IDbExecutor db, ISubsidioRepository repo, LiquidacionAcumulador acum,
        int idSubsidio, long ci, SubsidioPeriodo periodo, bool esPrimera, bool liquidar, DateTime fechaIni, CancellationToken ct)
    {
        int lMes = periodo.YyyyMm, lMesIni = periodo.MesInicioVentana, lMesIniImp = periodo.MesInicioAportes, mesFin = periodo.MesAnterior;
        var usr = _user.UserName;
        var calcularPromedio = true;

        if (esPrimera)
        {
            // ¿Tiene una enfermedad "enganchada" del mes anterior? Reusar su jornal e imponibles.
            var ant = (await db.QueryProcAsync<JornalAnteriorRow>("dbo.acc_sgpa_300_JornalAnterior2",
                new { pCI = ci, pLiquidar = liquidar, pMes = lMes, pFecha = fechaIni }, ct).ConfigureAwait(false))
                .FirstOrDefault();

            if (ant is not null)
            {
                acum.ValorJornal = (decimal)ant.ValorJornal;
                await db.ExecuteProcAsync("dbo.acc_sgpa_300_Insert_SubsidioImponibleAnterior",
                    new { pIdSubsidioAnt = ant.IdSubsidio, pIdSubsidio = idSubsidio, pUsr = usr }, ct).ConfigureAwait(false);
                calcularPromedio = false;

                var empAnt = await db.QueryProcAsync<EmpresaAnteriorRow>("dbo.acc_sgpa_300_SubsidioEmpresaAnterior",
                    new { pIdSubsidio = ant.IdSubsidio }, ct).ConfigureAwait(false);

                if (empAnt.Count > 0)
                    foreach (var e in empAnt)
                        await repo.InsertarCabezalEmpresaAsync(idSubsidio, e.CodEmpresa, SubsidioMath.Rdo((decimal)e.ValorJornal),
                            e.Dias, SubsidioMath.Rdo((decimal)e.ImpNominal), SubsidioMath.Rdo((decimal)e.ImpAguinaldo),
                            SubsidioMath.Rdo((decimal)e.ImpLiquido), usr, ct).ConfigureAwait(false);
                else
                    await ValorJornalAnteriorAsync(db, repo, acum, idSubsidio, ant.IdSubsidio, liquidar, usr, ct).ConfigureAwait(false);
            }
        }

        if (calcularPromedio)
        {
            // Promedio de imponibles por empresa (≥3 meses de aporte), excepto CASEMED.
            decimal promedio = 0m;
            var emp = await db.QueryProcAsync<PromedioEmpresaRow>("dbo.acc_sgpa_300_AfiliadoValorJornalxEmpresa",
                new { pCodCasemed = _codCasemed, pCI = ci, pMesIni = lMesIni, pMesFin = mesFin, pLiquidar = liquidar, pDias = 3, pMes = lMes, pMesIniImp = lMesIniImp },
                ct).ConfigureAwait(false);
            foreach (var e in emp)
            {
                var v = SubsidioMath.Rdo((decimal)e.Promedio);
                await repo.InsertarCabezalEmpresaAsync(idSubsidio, e.CodEmpresa, v, 0, 0, 0, 0, usr, ct).ConfigureAwait(false);
                promedio += v;
            }
            acum.ValorJornal = promedio;

            // Promedio sólo de CASEMED (≥1 mes).
            var cas = (await db.QueryProcAsync<PromedioRow>("dbo.acc_sgpa_300_AfiliadoValorJornalCasemed",
                new { pCodCasemed = _codCasemed, pCI = ci, pMesIni = lMesIni, pMesFin = mesFin, pLiquidar = liquidar, pDias = 1, pMes = lMes, pMesIniImp = lMesIniImp },
                ct).ConfigureAwait(false)).FirstOrDefault();
            if (cas is not null)
            {
                var v = SubsidioMath.Rdo((decimal)cas.Promedio);
                acum.ValorJornal += v;
                await repo.InsertarCabezalEmpresaAsync(idSubsidio, _codCasemed, v, 0, 0, 0, 0, usr, ct).ConfigureAwait(false);
            }

            // Persistir los imponibles utilizados.
            var p = new { pCI = ci, pMesIni = lMesIni, pMesFin = mesFin, pUsr = usr, pIdSubsidio = idSubsidio, pLiquidar = liquidar, pDias = 3, pCodCasemed = _codCasemed, pMes = lMes, pMesIniImp = lMesIniImp };
            await db.ExecuteProcAsync("dbo.acc_sgpa_300_InsertSubsidioImponible", p, ct).ConfigureAwait(false);
            await db.ExecuteProcAsync("dbo.acc_sgpa_300_InsertSubsidioImponibleCasemed",
                new { p.pCI, p.pMesIni, p.pMesFin, p.pUsr, p.pIdSubsidio, p.pLiquidar, pDias = 1, p.pCodCasemed, p.pMes, p.pMesIniImp }, ct).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Port de ProcesarCertificaciones (para el tramo actual): calcula los días subsidiados,
    /// inserta el período de enfermedad y reparte el importe nominal/aguinaldo entre las empresas.
    /// </summary>
    private async Task ProcesarCertificacionesAsync(ISubsidioRepository repo, LiquidacionAcumulador acum,
        int idSubsidio, SubsidioPeriodo periodo, CertificacionSpan cert, CancellationToken ct)
    {
        var primerDia = new DateTime(periodo.Anio, periodo.Mes, 1);
        var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

        var dIni = cert.FechaIni < primerDia ? primerDia : cert.FechaIni;
        var dFin = cert.FechaFin > ultimoDia ? ultimoDia : cert.FechaFin;

        var dias = (dFin - dIni).Days + 1;
        if (dIni == primerDia && dFin == ultimoDia)
            dias = 30; // mes completo cuenta como 30 días

        if (dIni == cert.FechaIni && await DescontarPrimerDiaAsync(repo, cert, ct).ConfigureAwait(false))
            dias = Math.Max(dias - 1, 0);

        acum.DiasCertif = dias;
        var deducible = (decimal)cert.ImporteDeducible;

        await repo.InsertarEnfermedadAsync(idSubsidio, cert.FechaIni, cert.FechaFin, dIni, dFin, _user.UserName, ct).ConfigureAwait(false);

        // Nominal total del afiliado = días·jornal − deducible.
        acum.ImpNominal += SubsidioMath.Rdo(dias * acum.ValorJornal - deducible);

        // Reparto por empresa: nominal proporcional y aguinaldo = nominal/12.
        var empresas = await repo.GetCabezalEmpresasAsync(idSubsidio, ct).ConfigureAwait(false);
        if (empresas.Count > 0)
        {
            foreach (var e in empresas)
            {
                var impNominal = SubsidioMath.Rdo(dias * (decimal)e.ValorJornal - deducible / empresas.Count);
                var impAguinaldo = SubsidioMath.Rdo(impNominal / 12m);
                await repo.ActualizarCabezalEmpresaMontosAsync(idSubsidio, e.CodEmpresa, impNominal, impAguinaldo, _user.UserName, ct).ConfigureAwait(false);
                acum.ImpAguinaldo += impAguinaldo;
            }
        }
    }

    /// <summary>Port de DescontarDia: se descuenta el primer día salvo internado o continuidad del día previo.</summary>
    private static async Task<bool> DescontarPrimerDiaAsync(ISubsidioRepository repo, CertificacionSpan cert, CancellationToken ct)
    {
        const int salidaTipoInternado = 5;
        if (cert.CodSalidaTipo == salidaTipoInternado) return false;
        var huboCertPrevia = await repo.ExisteCertificacionEnFechaAsync(cert.CI, cert.FechaIni.AddDays(-1), ct).ConfigureAwait(false);
        return !huboCertPrevia;
    }

    /// <summary>
    /// Port de ProcesarItems: aplica los ítems de subsidio (comparaciones, operadores, control IRP),
    /// los aportes jubilatorios (obrero/patronal según régimen) y calcula el importe líquido.
    /// NOTA: requiere que dbo.SubsidioItemCod tenga los valores cargados (hoy NULL en la base migrada
    /// → las deducciones dan 0; el cálculo de líquido es correcto una vez exista esa configuración).
    /// </summary>
    private async Task ProcesarItemsAsync(ISubsidioRepository repo, LiquidacionAcumulador acum, int idSubsidio,
        long ci, SubsidioPeriodo periodo, SubsidioParametros prm, CancellationToken ct)
    {
        const long aporteObrero = 0L, aportePatronal = 1000L;
        const int regimenNuevo = 2;
        var usr = _user.UserName;
        var smn = prm.SMN;
        var tope = prm.TopeJubilatorio;
        decimal totalDeducciones = 0m;

        var items = await repo.GetItemsCodFullAsync(ci, periodo, ct).ConfigureAwait(false);
        foreach (var it in items)
        {
            var impComp = it.CompararContra switch
            {
                1 => acum.ImpNominal,
                2 => acum.ImpAguinaldo,
                3 => acum.ImpNominal + acum.ImpAguinaldo,
                _ => 0m
            };

            var calc = await EvaluarItemAsync(repo, it, impComp, smn, ct).ConfigureAwait(false);
            if (calc is null) continue;
            var importe = SubsidioMath.Rdo(calc.Value);
            await repo.InsertarItemAsync(idSubsidio, it.CodSubsidioItemCod, importe, usr, ct).ConfigureAwait(false);

            if (it.ModificaNominal == true)
            {
                acum.ImpNominal -= importe * (it.Signo ?? 0);
                acum.ImpAguinaldo = acum.ImpNominal / 12m;
            }
            else if (it.Tipo == "O")
            {
                totalDeducciones += importe;
            }
        }

        // Aportes jubilatorios.
        var esNuevo = await repo.GetRegimenJubilatorioAsync(ci, ct).ConfigureAwait(false) == regimenNuevo;
        if (esNuevo)
        {
            acum.CoefAporteNominal = acum.ImpNominal > tope ? tope / acum.ImpNominal : 1m;
            acum.CoefAporteAguinaldo = acum.ImpAguinaldo > tope / 2m ? tope / 2m / acum.ImpAguinaldo : 1m;
        }

        var impObrero = await AporteJubilatorioAsync(repo, acum, esNuevo, tope, aporteObrero, ct).ConfigureAwait(false);
        await repo.InsertarItemAsync(idSubsidio, aporteObrero, impObrero, usr, ct).ConfigureAwait(false);
        totalDeducciones += impObrero; // sólo el aporte obrero descuenta del líquido

        var impPatronal = await AporteJubilatorioAsync(repo, acum, esNuevo, tope, aportePatronal, ct).ConfigureAwait(false);
        await repo.InsertarItemAsync(idSubsidio, aportePatronal, impPatronal, usr, ct).ConfigureAwait(false);

        acum.ImpLiquido = acum.ImpNominal + acum.ImpAguinaldo - totalDeducciones;

        // Detalle de ítems por empresa (no afecta el líquido del afiliado).
        await ProcesarItemsXEmpresaAsync(repo, acum, idSubsidio, items, smn, tope, esNuevo,
            aporteObrero, aportePatronal, usr, ct).ConfigureAwait(false);
    }

    /// <summary>Evalúa un ítem: devuelve el importe calculado (con control IRP) o null si no corresponde.</summary>
    private static async Task<decimal?> EvaluarItemAsync(ISubsidioRepository repo, ItemCodConfig it, decimal impComp,
        decimal smn, CancellationToken ct)
    {
        var valor = (decimal)(it.Valor ?? 0d);
        var valorMin = (decimal)(it.ValorMin ?? 0d);
        var valorMax = (decimal)(it.ValorMax ?? 0d);
        if (it.TipoComp == "S") { valorMin *= smn; valorMax *= smn; }

        var procesar = it.Comparar != true || (it.Operador switch
        {
            "<" => impComp < valorMax,
            ">" => impComp > valorMin,
            "<=" => impComp <= valorMax,
            ">=" => impComp >= valorMin,
            "><" => impComp > valorMin && impComp < valorMax,
            ">=<=" => impComp >= valorMin && impComp <= valorMax,
            "><=" => impComp > valorMin && impComp <= valorMax,
            ">=<" => impComp >= valorMin && impComp < valorMax,
            _ => impComp >= valorMin && impComp <= valorMax
        });
        if (!procesar) return null;

        var impCalculado = it.ValorTipo switch
        {
            "%" => impComp / 100m * valor,
            "*" => impComp * valor,
            "+" => impComp + valor,
            "-" => impComp - valor,
            _ => valor
        };

        var franja = await repo.GetImporteFranjaAnteriorAsync(it.CodSubsidioItemCod, impComp - impCalculado, smn, ct).ConfigureAwait(false);
        return franja is not null ? -(franja.Value - impComp) : impCalculado;
    }

    /// <summary>
    /// Port de ProcesarItemsXEmpresa: replica el cálculo de ítems a nivel de cada empresa, escribe
    /// SubsidioItemEmpresa y fija los importes finales (nominal/aguinaldo/líquido/días) por empresa.
    /// No modifica los totales del afiliado.
    /// </summary>
    private async Task ProcesarItemsXEmpresaAsync(ISubsidioRepository repo, LiquidacionAcumulador acum, int idSubsidio,
        IReadOnlyList<ItemCodConfig> items, decimal smn, decimal tope, bool esNuevo,
        long aporteObrero, long aportePatronal, string usr, CancellationToken ct)
    {
        var valObrero = await repo.GetSubsidioItemValorAsync(aporteObrero, ct).ConfigureAwait(false);
        var valPatronal = await repo.GetSubsidioItemValorAsync(aportePatronal, ct).ConfigureAwait(false);

        foreach (var e in await repo.GetCabezalEmpresasMontosAsync(idSubsidio, ct).ConfigureAwait(false))
        {
            var nominal = (decimal)e.ImpNominal;
            var aguinaldo = (decimal)e.ImpAguinaldo;
            decimal totalEmpresa = 0m;

            foreach (var it in items)
            {
                var impComp = it.CompararContra switch
                {
                    1 => nominal,
                    2 => aguinaldo,
                    3 => nominal + aguinaldo,
                    _ => 0m
                };
                var calc = await EvaluarItemAsync(repo, it, impComp, smn, ct).ConfigureAwait(false);
                if (calc is null) continue;
                var importe = SubsidioMath.Rdo(calc.Value);
                await repo.InsertarItemEmpresaAsync(idSubsidio, it.CodSubsidioItemCod, e.CodEmpresa, importe, usr, ct).ConfigureAwait(false);

                if (it.ModificaNominal == true)
                {
                    // Prorrateo de la modificación según la participación de la empresa en el nominal del afiliado.
                    if (acum.ImpNominal != 0m)
                        nominal -= importe * (it.Signo ?? 0) * nominal / acum.ImpNominal;
                    aguinaldo = nominal / 12m;
                }
                else if (it.Tipo == "O")
                {
                    totalEmpresa += importe * (it.Signo ?? 0);
                }
            }

            // Aporte jubilatorio obrero por empresa.
            var impObreroEmp = esNuevo
                ? SubsidioMath.Rdo(Math.Min(nominal, tope) * valObrero + Math.Min(aguinaldo, tope / 2m) * valObrero)
                : SubsidioMath.Rdo((nominal + aguinaldo) * valObrero);
            await repo.InsertarItemEmpresaAsync(idSubsidio, aporteObrero, e.CodEmpresa, impObreroEmp, usr, ct).ConfigureAwait(false);
            totalEmpresa += impObreroEmp;

            // Aporte jubilatorio patronal por empresa (con prorrateo si se llegó al tope).
            decimal impPatronalEmp;
            if (esNuevo)
            {
                impPatronalEmp = SubsidioMath.Rdo(Math.Min(nominal, tope) * valPatronal + Math.Min(aguinaldo, tope / 2m) * valPatronal);
                impPatronalEmp = SubsidioMath.Rdo(impPatronalEmp * acum.CoefAporteAguinaldo);
            }
            else
            {
                impPatronalEmp = SubsidioMath.Rdo((nominal + aguinaldo) * valPatronal);
            }
            await repo.InsertarItemEmpresaAsync(idSubsidio, aportePatronal, e.CodEmpresa, impPatronalEmp, usr, ct).ConfigureAwait(false);

            var liquidoEmp = nominal + aguinaldo - totalEmpresa;
            await repo.ActualizarCabezalEmpresaFinalAsync(idSubsidio, e.CodEmpresa, nominal, aguinaldo, liquidoEmp, acum.DiasCertif, usr, ct).ConfigureAwait(false);
        }
    }

    private static async Task<decimal> AporteJubilatorioAsync(ISubsidioRepository repo, LiquidacionAcumulador acum,
        bool regimenNuevo, decimal tope, long codItem, CancellationToken ct)
    {
        var valor = await repo.GetSubsidioItemValorAsync(codItem, ct).ConfigureAwait(false);
        if (regimenNuevo)
            return SubsidioMath.Rdo(Math.Min(acum.ImpNominal, tope) * valor + Math.Min(acum.ImpAguinaldo, tope / 2m) * valor);
        return SubsidioMath.Rdo((acum.ImpNominal + acum.ImpAguinaldo) * valor);
    }

    /// <summary>
    /// Port de InsertarBPS: calcula el líquido que cubre BPS (jornal·díasBPS·0.7·(1+1/12)·(1−aportes),
    /// topeado), inserta la fila SubsidioCabezal_BPS y deja en el cabezal el líquido a pagar
    /// (ImpLiquido − líquidoBPS), que es el valor final que recibe el afiliado.
    /// </summary>
    private async Task InsertarBpsAsync(ISubsidioRepository repo, LiquidacionAcumulador acum, int idSubsidio,
        long ci, SubsidioPeriodo periodo, SubsidioParametros prm, CancellationToken ct)
    {
        var diasBps = await GetDiasBpsAsync(repo, idSubsidio, ci, ct).ConfigureAwait(false);

        var liquidoBps = acum.ValorJornal * diasBps * 0.7m;
        liquidoBps += liquidoBps / 12m;                       // aguinaldo
        liquidoBps *= 1m - prm.PctBPS;                        // menos aportes

        var descuento = await repo.GetTotalLiquidoBpsAsync(ci, periodo.Mes, periodo.Anio, ct).ConfigureAwait(false);
        if (liquidoBps > prm.TopeLiquidoBPS - descuento)      // no topear el BPS más de una vez en el mes
            liquidoBps = Math.Max(prm.TopeLiquidoBPS - descuento, 0m);

        var liquidoPagar = Math.Round(acum.ImpLiquido - liquidoBps, 0, MidpointRounding.AwayFromZero);
        await repo.InsertarCabezalBpsAsync(idSubsidio, diasBps, liquidoBps, liquidoPagar, _user.UserName, ct).ConfigureAwait(false);

        // SubsidioCabezal.ImpLiquido queda en BRUTO (resultado de ProcesarItems), igual que la liquidación
        // cruda del VB6 y que el período reciente sin post-procesar (202604, verificado al centavo).
        // El neto a pagar (lo que cubre BPS lo paga BPS) queda en SubsidioCabezal_BPS.LiquidoPagar.
        // El neteo del ImpLiquido en períodos viejos es un post-proceso manual (ActualizarLiquidos/Excel).
    }

    /// <summary>Port de GetDiasBPS: días que cubre BPS (tope 30; descuenta 3 días salvo internado/BSE o continuidad).</summary>
    private static async Task<int> GetDiasBpsAsync(ISubsidioRepository repo, int idSubsidio, long ci, CancellationToken ct)
    {
        var enf = await repo.GetEnfermedadFechasAsync(idSubsidio, ct).ConfigureAwait(false);
        if (enf is null) return 0;

        var dias = (enf.FechaFinSubsidio - enf.FechaIniSubsidio).Days + 1;
        var primero = new DateTime(enf.FechaIniSubsidio.Year, enf.FechaIniSubsidio.Month, 1);
        var ultimo = primero.AddMonths(1).AddDays(-1);
        if (enf.FechaIniSubsidio == primero && enf.FechaFinSubsidio == ultimo) dias = 30;
        if (dias > 30) dias = 30;

        var codSalida = await repo.GetCodSalidaTipoAsync(ci, enf.FechaIni, ct).ConfigureAwait(false);
        if (codSalida is 5 or 7) return dias; // internado / BSE: no descuenta días

        const int diasDescontar = 3;
        var diasAnteriores = (enf.FechaIniSubsidio - enf.FechaIni).Days;
        var fechaIni = enf.FechaIni;
        while (true)
        {
            var prev = await repo.GetCertificacionPorFechaFinAsync(ci, fechaIni.AddDays(-1), ct).ConfigureAwait(false);
            if (prev is not null)
            {
                diasAnteriores += (prev.FechaFin - prev.FechaIni).Days;
                fechaIni = prev.FechaIni;
            }
            if (diasAnteriores >= diasDescontar) return dias; // ya hubo ≥3 días previos → no descuenta
            if (prev is null) break;
        }

        if (diasAnteriores > diasDescontar) diasAnteriores = diasDescontar;
        dias -= diasDescontar - diasAnteriores;
        return dias < 0 ? 0 : dias;
    }

    /// <summary>Port de ValorJornalAnterior: caso de cambio de sistema (sin detalle por empresa anterior).</summary>
    private async Task ValorJornalAnteriorAsync(IDbExecutor db, ISubsidioRepository repo, LiquidacionAcumulador acum,
        int idSubsidio, int idSubsidioAnt, bool liquidar, string usr, CancellationToken ct)
    {
        decimal promedio = 0m;
        foreach (var spName in new[] { "dbo.acc_sgpa_300_SubsidioEmpresaAnteriorVacia", "dbo.acc_sgpa_300_SubsidioEmpresaAnteriorVaciaCasemed" })
        {
            var rows = await db.QueryProcAsync<PromedioEmpresaRow>(spName,
                new { pIdSubsidio = idSubsidioAnt, pLiquidar = liquidar, pCodCasemed = _codCasemed }, ct).ConfigureAwait(false);
            foreach (var e in rows)
            {
                var v = SubsidioMath.Rdo((decimal)e.Promedio);
                await repo.InsertarCabezalEmpresaAsync(idSubsidio, e.CodEmpresa, v, 0, 0, 0, 0, usr, ct).ConfigureAwait(false);
                promedio += v;
            }
        }
        acum.ValorJornal = promedio;
    }

    private sealed class JornalAnteriorRow { public double ValorJornal { get; set; } public int IdSubsidio { get; set; } }
    private sealed class EmpresaAnteriorRow { public int CodEmpresa { get; set; } public double ValorJornal { get; set; } public int Dias { get; set; } public double ImpNominal { get; set; } public double ImpAguinaldo { get; set; } public double ImpLiquido { get; set; } }
    private sealed class PromedioEmpresaRow { public int CodEmpresa { get; set; } public double Promedio { get; set; } }
    private sealed class PromedioRow { public double Promedio { get; set; } }
}
