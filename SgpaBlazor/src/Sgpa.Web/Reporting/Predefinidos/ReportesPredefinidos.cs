using DevExpress.XtraReports.UI;
using Sgpa.Web.Reports;
using Sgpa.Web.Reports.Afiliados;
using Sgpa.Web.Reports.Empresas;
using Sgpa.Web.Reports.Mutualistas;
using Sgpa.Web.Reports.Certificaciones;
using Sgpa.Web.Reports.Subsidios;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>
/// Un reporte predefinido asociado a una pantalla/entidad (lo que muestra el diálogo genérico de impresión).
/// <see cref="Entidad"/> es el nombre de la tabla/entidad (p.ej. "Afiliado") por la que se filtra el catálogo.
/// </summary>
public sealed record ReportePredefDef(string Entidad, string Key, string Nombre, string Icono);

/// <summary>
/// Catálogo + builder genérico de reportes predefinidos por entidad (mismo enfoque que el de préstamos, pero
/// reutilizable desde cualquier pantalla). El diálogo lista <see cref="Para"/> con checkboxes; <see cref="BuildAsync"/>
/// arma los tildados (clave del registro + id del registro actual) y los <b>fusiona</b> en un único documento.
/// </summary>
public interface IReportesPredefinidos
{
    IReadOnlyList<ReportePredefDef> Para(string entidad);

    /// <param name="ids">
    /// Registros sobre los que corre el reporte. Para pantallas per-registro es un único id (CI, código…);
    /// para reportes por selección (p.ej. certificaciones) son las claves seleccionadas en la grilla.
    /// </param>
    Task<XtraReport?> BuildAsync(string entidad, IReadOnlyCollection<string> keys, IReadOnlyList<long> ids, CancellationToken ct = default);
}

public sealed class ReportesPredefinidos(IAfiliadoReporteData afiliado, IEmpresaReporteData empresa, IMutualistaReporteData mutualista, ICertificacionReporteData certificacion, ISubsidioReporteData subsidio, IWebHostEnvironment env) : IReportesPredefinidos
{
    // Registro de todos los reportes predefinidos por entidad. Al sumar una pantalla nueva, agregar acá su set
    // y el case correspondiente en BuildOneAsync.
    private static readonly ReportePredefDef[] _todos =
    {
        new("Afiliado", "afiliado-datos",      "Ficha del afiliado",     "fa-id-card"),
        new("Afiliado", "afiliado-empleos",    "Empleos del afiliado",   "fa-briefcase"),
        new("Afiliado", "afiliado-apuntes",    "Apuntes del afiliado",   "fa-note-sticky"),
        new("Afiliado", "afiliado-imponibles", "Imponibles del afiliado", "fa-coins"),
        new("Empresa",  "empresa-aportes",     "Resumen de aportes",     "fa-money-bill-trend-up"),
        new("Mutualista", "mutualista-listado", "Listado de mutualistas", "fa-hospital"),
        new("Certificacion", "certificacion-detalle",      "Detalle de certificaciones",  "fa-file-medical"),
        new("Certificacion", "certificacion-certificador", "Informe por certificador",    "fa-user-doctor"),
        new("Certificacion", "certificacion-mutualista",   "Por mutualista",              "fa-hospital"),
        new("Certificacion", "certificacion-mutualista-afeccion", "Por mutualista (con afección)", "fa-notes-medical"),
        new("Certificacion", "certificacion-dias-afiliado", "Días por afiliado",          "fa-calendar-day"),
        new("Certificacion", "certificacion-ficha",        "Ficha de certificación",      "fa-id-card-clip"),
        new("Subsidio", "subsidio-resumen", "Resumen de liquidación", "fa-table-list"),
        new("Subsidio", "subsidio-detalle", "Detalle de liquidación", "fa-list-ul"),
        new("Subsidio", "subsidio-recibos", "Recibos",                "fa-receipt"),
        new("Subsidio", "subsidio-bps",     "Obligación mensual (BPS)", "fa-landmark"),
    };

    private string? Logo => ReportBrand.LogoPath(env.WebRootPath);

    public IReadOnlyList<ReportePredefDef> Para(string entidad)
        => _todos.Where(d => string.Equals(d.Entidad, entidad, StringComparison.OrdinalIgnoreCase)).ToList();

    private async Task<XtraReport?> BuildOneAsync(string key, IReadOnlyList<long> ids, CancellationToken ct)
    {
        // Per-registro: el primer id. Por selección (certificaciones): toda la colección.
        long id = ids.Count > 0 ? ids[0] : 0;
        switch (key)
        {
            case "afiliado-datos":
                var datos = await afiliado.GetDatosAsync(id, ct);
                if (datos is null) return null;
                var rDatos = new AfiliadoDatosReport();
                rDatos.Bind(new[] { datos }, Logo);
                return rDatos;

            case "afiliado-empleos":
                // El encabezado (nombre + CI) sale de la ficha; las filas, de la lista de empleos.
                var ficha = await afiliado.GetDatosAsync(id, ct);
                var empleos = await afiliado.GetEmpleosAsync(id, ct);
                var rEmp = new AfiliadoEmpleosReport();
                rEmp.Bind(empleos, ficha?.NombreCompleto ?? $"Afiliado {id}", ficha?.CIFormato ?? id.ToString("#,#"), Logo);
                return rEmp;

            case "afiliado-apuntes":
                var fichaAp = await afiliado.GetDatosAsync(id, ct);
                var apuntes = await afiliado.GetApuntesAsync(id, ct);
                var rAp = new AfiliadoApuntesReport();
                rAp.Bind(apuntes, fichaAp?.NombreCompleto ?? $"Afiliado {id}", fichaAp?.CIFormato ?? id.ToString("#,#"), Logo);
                return rAp;

            case "afiliado-imponibles":
                var fichaImp = await afiliado.GetDatosAsync(id, ct);
                var imponibles = await afiliado.GetImponiblesAsync(id, ct);
                var rImp = new AfiliadoImponiblesReport();
                rImp.Bind(imponibles, fichaImp?.NombreCompleto ?? $"Afiliado {id}", fichaImp?.CIFormato ?? id.ToString("#,#"), Logo);
                return rImp;

            case "empresa-aportes":
                var aportes = await empresa.GetAportesAsync((int)id, ct);
                var nombreEmp = aportes.FirstOrDefault()?.DescEmpresa;
                var rApo = new EmpresaAportesReport();
                rApo.Bind(aportes, string.IsNullOrWhiteSpace(nombreEmp) ? $"Empresa {id}" : nombreEmp!.Trim(), id.ToString(), Logo);
                return rApo;

            case "mutualista-listado":
                var muts = await mutualista.GetListadoAsync(ct);
                var rMut = new MutualistaListadoReport();
                rMut.Bind(muts, Logo);
                return rMut;

            case "certificacion-detalle":
                var certs = await certificacion.GetByLlamadosAsync(ids, ct);
                if (certs.Count == 0) return null;
                var rCert = new CertificacionDetalleReport();
                rCert.Bind(certs, Logo);
                return rCert;

            case "certificacion-certificador":
                var certsC = await certificacion.GetByLlamadosAsync(ids, ct);
                if (certsC.Count == 0) return null;
                var rCertC = new CertificacionPorCertificadorReport();
                rCertC.Bind(certsC, Logo);
                return rCertC;

            case "certificacion-mutualista":
                var certsM = await certificacion.GetByLlamadosAsync(ids, ct);
                if (certsM.Count == 0) return null;
                var rCertM = new CertificacionPorMutualistaReport();
                rCertM.Bind(certsM, Logo);
                return rCertM;

            case "certificacion-mutualista-afeccion":
                var certsMA = await certificacion.GetByLlamadosAsync(ids, ct);
                if (certsMA.Count == 0) return null;
                var rCertMA = new CertificacionPorMutualistaAfeccionReport();
                rCertMA.Bind(certsMA, Logo);
                return rCertMA;

            case "certificacion-dias-afiliado":
                var afecs = await certificacion.GetAfeccionesByLlamadosAsync(ids, ct);
                if (afecs.Count == 0) return null;
                var rDias = new CertificacionDiasAfiliadoReport();
                rDias.Bind(afecs, Logo);
                return rDias;

            case "certificacion-ficha":
                var afecsF = await certificacion.GetAfeccionesByLlamadosAsync(ids, ct);
                if (afecsF.Count == 0) return null;
                var rFicha = new CertificacionFichaReport();
                rFicha.Bind(afecsF, Logo);
                return rFicha;

            case "subsidio-resumen":
                var resumen = await subsidio.GetResumenAsync(ids, ct);
                if (resumen.Count == 0) return null;
                var rResumen = new SubsidioResumenReport();
                rResumen.Bind(resumen, Logo);
                return rResumen;

            case "subsidio-detalle":
                var detalle = await subsidio.GetDetalleAsync(ids, ct);
                if (detalle.Count == 0) return null;
                var rDetalle = new SubsidioDetalleReport();
                rDetalle.Bind(detalle, Logo);
                return rDetalle;

            case "subsidio-recibos":
                var recibos = await subsidio.GetRecibosAsync(ids, ct);
                if (recibos.Count == 0) return null;
                var rRecibos = new SubsidioReciboReport();
                rRecibos.Bind(recibos, Logo);
                return rRecibos;

            case "subsidio-bps":
                var bps = await subsidio.GetBpsAsync(ids, ct);
                if (bps.Personas == 0) return null;
                var rBps = new SubsidioBpsReport();
                rBps.Bind(bps, Logo);
                return rBps;

            default:
                return null;
        }
    }

    public async Task<XtraReport?> BuildAsync(string entidad, IReadOnlyCollection<string> keys, IReadOnlyList<long> ids, CancellationToken ct = default)
    {
        XtraReport? master = null;
        // Respeta el orden del catálogo, no el de selección.
        foreach (var key in Para(entidad).Select(d => d.Key).Where(keys.Contains))
        {
            var r = await BuildOneAsync(key, ids, ct);
            if (r is null) continue;
            ReportPie.Aplicar(r);
            r.CreateDocument();
            if (master is null) master = r;
            else master.Pages.AddRange(r.Pages);
        }
        if (master is not null) master.PrintingSystem.ContinuousPageNumbering = true;
        return master;
    }
}
