using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Services;

/// <summary>
/// File generation services for bank and BPS exports.
/// Replaces VB6: GenerarArchivoNBC, GenerarArchivoBROU, GenerarArchivoBROUExcel,
/// GenerarArchivoDiscount, ExportarBPS.
/// </summary>
public class ExportService(NewSgpaEFCoreDbContext db)
{
    /// <summary>
    /// Generates the NBC (Nuevo Banco Comercial) payment file.
    /// Replaces VB6 GenerarArchivoNBC + Rs_Export_NBC query.
    /// </summary>
    public async Task<byte[]> GenerarArchivoNbcAsync(int mes, int anio, bool liquidar, DateTime fecha)
    {
        var data = await (from s in db.SubsidioCabezales.AsNoTracking()
                          join a in db.Afiliados.AsNoTracking() on s.CI equals a.CI
                          where s.Mes == mes && s.Anio == anio && s.Liquidar == liquidar
                                && a.CodBanco == 2 // NBC
                                && s.ImpLiquido > 0
                          select new
                          {
                              a.CI,
                              s.ImpLiquido,
                              a.NroCuenta,
                              Fecha = fecha
                          })
                          .OrderBy(x => x.CI)
                          .ToListAsync();

        var sb = new StringBuilder();
        foreach (var item in data)
        {
            sb.AppendLine($"{item.CI}\t{item.NroCuenta}\t{item.ImpLiquido:F2}\t{item.Fecha:dd/MM/yyyy}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    /// <summary>
    /// Generates the BROU payment file.
    /// Replaces VB6 GenerarArchivoBROU + Rs_Export_BROU query.
    /// </summary>
    public async Task<byte[]> GenerarArchivoBrouAsync(int mes, int anio, bool liquidar, DateTime fecha)
    {
        var data = await (from s in db.SubsidioCabezales.AsNoTracking()
                          join a in db.Afiliados.AsNoTracking() on s.CI equals a.CI
                          where s.Mes == mes && s.Anio == anio && s.Liquidar == liquidar
                                && a.CodBanco == 1 // BROU
                                && s.ImpLiquido > 0
                          select new
                          {
                              a.CI,
                              s.ImpLiquido,
                              a.NroCuenta,
                              Fecha = fecha,
                              Nombre = a.Apellido1 + " " + (a.Apellido2 ?? "") + ", " + a.Nombres
                          })
                          .OrderBy(x => x.CI)
                          .ToListAsync();

        var sb = new StringBuilder();
        foreach (var item in data)
        {
            sb.AppendLine($"{item.CI}\t{item.NroCuenta}\t{item.ImpLiquido:F2}\t{item.Fecha:dd/MM/yyyy}\t{item.Nombre}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    /// <summary>
    /// Generates the BPS export data.
    /// Replaces VB6 ExportarBPS.
    /// </summary>
    public async Task<byte[]> GenerarArchivoBpsAsync(int mes, int anio, bool liquidar)
    {
        var data = await (from s in db.SubsidioCabezales.AsNoTracking()
                          join a in db.Afiliados.AsNoTracking() on s.CI equals a.CI
                          join bps in db.SubsidioCabezalesBps.AsNoTracking() on s.IdSubsidio equals bps.IdSubsidio into bpsJoin
                          from bps in bpsJoin.DefaultIfEmpty()
                          where s.Mes == mes && s.Anio == anio && s.Liquidar == liquidar
                          select new
                          {
                              a.CI,
                              Nombre = a.Nombres,
                              Apellido = a.Apellido1 + " " + (a.Apellido2 ?? ""),
                              s.ImpNominal,
                              s.ImpAguinaldo,
                              s.ImpLiquido,
                              s.Dias,
                              s.ValorJornal,
                              DiasBPS = bps != null ? bps.DiasBPS : 0,
                              LiquidoBPS = bps != null ? bps.LiquidoBPS : 0,
                              LiquidoPagar = bps != null ? bps.LiquidoPagar : 0
                          })
                          .OrderBy(x => x.Apellido)
                          .ToListAsync();

        var sb = new StringBuilder();
        // Header
        sb.AppendLine("CI\tNombre\tApellido\tDias\tValorJornal\tNominal\tAguinaldo\tLiquido\tDiasBPS\tLiquidoBPS\tLiquidoPagar");

        foreach (var item in data)
        {
            sb.AppendLine(string.Join("\t",
                item.CI,
                item.Nombre,
                item.Apellido,
                item.Dias,
                item.ValorJornal?.ToString("F3", CultureInfo.InvariantCulture),
                item.ImpNominal?.ToString("F2", CultureInfo.InvariantCulture),
                item.ImpAguinaldo?.ToString("F2", CultureInfo.InvariantCulture),
                item.ImpLiquido?.ToString("F2", CultureInfo.InvariantCulture),
                item.DiasBPS,
                item.LiquidoBPS?.ToString("F2", CultureInfo.InvariantCulture),
                item.LiquidoPagar?.ToString("F2", CultureInfo.InvariantCulture)));
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    /// <summary>
    /// Generates the subsidio summary report data.
    /// Replaces VB6 GenRptResumen.
    /// </summary>
    public async Task<List<SubsidioResumenDto>> GetSubsidioResumenAsync(int mes, int anio)
    {
        return await (from s in db.SubsidioCabezales.AsNoTracking()
                      join a in db.Afiliados.AsNoTracking() on s.CI equals a.CI
                      where s.Mes == mes && s.Anio == anio
                      orderby a.Apellido1, a.Apellido2, a.Nombres
                      select new SubsidioResumenDto
                      {
                          CI = s.CI ?? 0,
                          Nombre = a.Apellido1 + " " + (a.Apellido2 ?? "") + ", " + a.Nombres,
                          Dias = s.Dias ?? 0,
                          ValorJornal = s.ValorJornal ?? 0,
                          ImpNominal = s.ImpNominal ?? 0,
                          ImpAguinaldo = s.ImpAguinaldo ?? 0,
                          ImpLiquido = s.ImpLiquido ?? 0,
                          Liquidar = s.Liquidar,
                          FechaNacimiento = a.FechaNacimiento
                      })
                      .ToListAsync();
    }
}

public class SubsidioResumenDto
{
    public long CI { get; set; }
    public string? Nombre { get; set; }
    public int Dias { get; set; }
    public float ValorJornal { get; set; }
    public double ImpNominal { get; set; }
    public double ImpAguinaldo { get; set; }
    public double ImpLiquido { get; set; }
    public bool Liquidar { get; set; }
    public DateTime? FechaNacimiento { get; set; }
}

