using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using Microsoft.Data.SqlClient;

namespace Sgpa.Web.Reporting;

/// <summary>
/// Almacenamiento de reportes de DevExpress en dbo.Reporte (el "url" es el Id del reporte).
/// El layout se guarda como REPX (XML) serializado del XtraReport.
/// </summary>
public sealed class SgpaReportStorage : ReportStorageWebExtension
{
    private readonly string _cs;

    public SgpaReportStorage(IConfiguration config)
        => _cs = config.GetConnectionString("NewSgpa2")
                 ?? throw new InvalidOperationException("Falta la cadena de conexión NewSgpa2.");

    public override bool CanSetData(string url) => true;

    public override bool IsValidUrl(string url) => int.TryParse(url, out _);

    public override byte[] GetData(string url)
    {
        using var cn = new SqlConnection(_cs);
        cn.Open();
        using var cmd = new SqlCommand("SELECT Layout FROM dbo.Reporte WHERE Id = @id", cn);
        cmd.Parameters.AddWithValue("@id", int.Parse(url));
        return cmd.ExecuteScalar() as byte[] ?? Array.Empty<byte>();
    }

    public override Dictionary<string, string> GetUrls()
    {
        var dict = new Dictionary<string, string>();
        using var cn = new SqlConnection(_cs);
        cn.Open();
        using var cmd = new SqlCommand("SELECT Id, Nombre FROM dbo.Reporte ORDER BY Nombre", cn);
        using var rd = cmd.ExecuteReader();
        while (rd.Read())
            dict[rd.GetInt32(0).ToString()] = rd.GetString(1);
        return dict;
    }

    public override void SetData(XtraReport report, string url)
    {
        using var cn = new SqlConnection(_cs);
        cn.Open();
        using var cmd = new SqlCommand("UPDATE dbo.Reporte SET Layout = @layout, Fecha = SYSDATETIME() WHERE Id = @id", cn);
        cmd.Parameters.AddWithValue("@id", int.Parse(url));
        cmd.Parameters.AddWithValue("@layout", Serialize(report));
        cmd.ExecuteNonQuery();
    }

    public override string SetNewData(XtraReport report, string defaultUrl)
    {
        using var cn = new SqlConnection(_cs);
        cn.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO dbo.Reporte (Nombre, Layout) VALUES (@nombre, @layout); SELECT CAST(SCOPE_IDENTITY() AS int);", cn);
        cmd.Parameters.AddWithValue("@nombre", string.IsNullOrWhiteSpace(defaultUrl) ? "Reporte" : defaultUrl);
        cmd.Parameters.AddWithValue("@layout", Serialize(report));
        return Convert.ToInt32(cmd.ExecuteScalar()).ToString();
    }

    private static byte[] Serialize(XtraReport report)
    {
        using var ms = new MemoryStream();
        report.SaveLayoutToXml(ms);
        return ms.ToArray();
    }
}
