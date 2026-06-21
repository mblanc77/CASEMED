using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Sgpa.Business.DependencyInjection;
using Sgpa.Data.DependencyInjection;
using Sgpa.Data.Security;
using Sgpa.Domain.Security;
using Serilog;
using DevExpress.AspNetCore.Reporting;
using DevExpress.Blazor.Reporting;
using DevExpress.XtraReports.Web.Extensions;
using Sgpa.Web.Components;
using Sgpa.Web.Reporting;
using Sgpa.Web.Security;

var builder = WebApplication.CreateBuilder(args);

// Logging a archivo por fecha (logs/sgpa-AAAAMMDD.log, retención 31 días) + consola.
// Además, un sink a dbo.Z_ErrorLog para que las excepciones NO controladas (render/circuito de Blazor) también
// queden en base, no sólo en el archivo. Las controladas ya las escribe IErrorLog.
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/sgpa-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}");

    var cs = context.Configuration.GetConnectionString("NewSgpa2");
    if (!string.IsNullOrEmpty(cs))
        configuration.WriteTo.Sink(new Sgpa.Web.Logging.DbErrorLogSink(cs));
});

var connectionString = builder.Configuration.GetConnectionString("NewSgpa2")
    ?? throw new InvalidOperationException("Falta la cadena de conexión 'NewSgpa2' en appsettings.json.");
builder.Services.AddSgpaData(connectionString);
builder.Services.AddSgpaBusiness();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    // Operaciones pesadas (carga de grafo de reportes) pueden tener al circuito ocupado un rato: subimos el
    // timeout del cliente para que no se desconecte. Las exportaciones/impresión van por HTTP fuera del circuito.
    .AddHubOptions(o =>
    {
        o.ClientTimeoutInterval = TimeSpan.FromMinutes(3);
        o.KeepAliveInterval = TimeSpan.FromSeconds(15);
        o.HandshakeTimeout = TimeSpan.FromSeconds(30);
    });

builder.Services.AddDevExpressBlazor(options =>
{
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});

// --- DevExpress Reporting: visor + diseñador End-User + almacenamiento de reportes en dbo.Reporte ---
// El visor/diseñador montan controllers MVC (WebDocumentViewer/ReportDesigner/QueryBuilder), que requieren
// la infraestructura MVC (IUrlHelperFactory, ApplicationPartManager). La app es Blazor (AddRazorComponents),
// así que hay que sumar AddControllers() explícitamente o falla la validación del contenedor en Build().
// ObjectDataSource sólo deserializa tipos "confiables" (protección anti-deserialización de DevExpress).
// Nuestros reportes bindean a entidades de Sgpa.Domain, así que registramos esa assembly como confiable.
DevExpress.Utils.DeserializationSettings.RegisterTrustedAssembly(typeof(Sgpa.Domain.Metadata.EntityCatalog).Assembly);
// Los DTOs de los reportes predefinidos viven en el assembly Web (ObjectDataSource los deserializa del .repx).
DevExpress.Utils.DeserializationSettings.RegisterTrustedAssembly(typeof(Sgpa.Web.Reporting.Predefinidos.PrestamoCuadroLinea).Assembly);

builder.Services.AddControllers();
builder.Services.AddDevExpressBlazorReporting();
builder.Services.AddDevExpressServerSideBlazorReportViewer();
builder.Services.AddScoped<ReportStorageWebExtension, SgpaReportStorage>();
builder.Services.AddScoped<Sgpa.Web.Reporting.IReporteCatalogo, Sgpa.Web.Reporting.ReporteCatalogo>();
builder.Services.AddScoped<Sgpa.Web.Reporting.IReportRenderService, Sgpa.Web.Reporting.ReportRenderService>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.IPrestamoReporteData, Sgpa.Web.Reporting.Predefinidos.PrestamoReporteData>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.IPrestamoReportes, Sgpa.Web.Reporting.Predefinidos.PrestamoReportes>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.IAfiliadoReporteData, Sgpa.Web.Reporting.Predefinidos.AfiliadoReporteData>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.IEmpresaReporteData, Sgpa.Web.Reporting.Predefinidos.EmpresaReporteData>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.IMutualistaReporteData, Sgpa.Web.Reporting.Predefinidos.MutualistaReporteData>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.ICertificacionReporteData, Sgpa.Web.Reporting.Predefinidos.CertificacionReporteData>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.ISubsidioReporteData, Sgpa.Web.Reporting.Predefinidos.SubsidioReporteData>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.IReportesPredefinidos, Sgpa.Web.Reporting.Predefinidos.ReportesPredefinidos>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.IEstadisticaReporteData, Sgpa.Web.Reporting.Predefinidos.EstadisticaReporteData>();
builder.Services.AddScoped<Sgpa.Web.Reporting.Predefinidos.IEstadisticaReportes, Sgpa.Web.Reporting.Predefinidos.EstadisticaReportes>();

// Conexión de datos del diseñador/visor: sólo NewSgpa2 (curada). El wizard ofrece esta conexión para
// armar queries y relaciones master-detail; en runtime se resuelve por nombre (no se guarda la cadena en el .repx).
builder.Services.ConfigureReportingServices(configurator =>
{
    configurator.ConfigureReportDesigner(designer =>
    {
        designer.RegisterDataSourceWizardConnectionStringsProvider<SgpaReportConnectionProvider>();
    });
});

// --- Seguridad: cookie auth + estado de autenticación para Blazor ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/denegado";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        // Endurecido: la cookie de auth no es accesible por JS (HttpOnly), va sólo a este sitio (SameSite)
        // y lleva el flag Secure cuando la conexión es HTTPS. El ticket va cifrado/firmado por Data Protection.
        options.Cookie.Name = "Sgpa.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Usuario actual real (roles + permisos por tabla). Sustituye al DefaultCurrentUser.
builder.Services.AddScoped<WebCurrentUser>();
builder.Services.AddScoped<ICurrentUser>(sp => sp.GetRequiredService<WebCurrentUser>());

// Enriquece el logging con el usuario del circuito (para que las no controladas registren quién las disparó).
builder.Services.AddScoped<Microsoft.AspNetCore.Components.Server.Circuits.CircuitHandler, Sgpa.Web.Logging.UserLogCircuitHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// Endpoints del visor/diseñador de DevExpress Reporting.
app.UseDevExpressBlazorReporting();

app.MapStaticAssets();
// Controllers MVC del backend de DevExpress Reporting (document service del visor/diseñador).
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// --- Endpoints de login / logout (escriben la cookie fuera del circuito Blazor) ---
app.MapPost("/auth/login", async (HttpContext http, ISecurityService security) =>
{
    var form = await http.Request.ReadFormAsync();
    var login = form["login"].ToString();
    var password = form["password"].ToString();
    var returnUrl = form["returnUrl"].ToString();
    var remember = !string.IsNullOrEmpty(form["remember"].ToString());

    var ctx = await security.AuthenticateAsync(login, password);
    if (ctx is null)
        return Results.Redirect($"/login?error=1&returnUrl={Uri.EscapeDataString(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl)}");

    var claims = new List<Claim>
    {
        new(ClaimTypes.Name, ctx.Login),
        new("nombre", ctx.Nombre ?? ctx.Login),
        new("admin", ctx.IsAdmin ? "1" : "0")
    };
    claims.AddRange(ctx.Roles.Select(r => new Claim(ClaimTypes.Role, r)));

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

    // "Recordarme": cookie PERSISTENTE (sobrevive al cierre del navegador) por un tiempo acotado (8 horas).
    // No se guarda la clave en ningún lado: sólo el ticket cifrado/firmado. Sin tildar → cookie de sesión (8h sliding).
    var props = new AuthenticationProperties();
    if (remember)
    {
        props.IsPersistent = true;
        props.ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8);
    }

    await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), props);
    return Results.Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
});

app.MapPost("/auth/logout", async (HttpContext http) =>
{
    await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

// PDF del reporte fuera del circuito Blazor (sin timeout): ?keys=1,2 filtra; ?print=1 lo abre inline (para imprimir).
app.MapGet("/reportes/pdf/{id:int}", async (int id, HttpContext http, Sgpa.Web.Reporting.IReportRenderService renderer) =>
{
    var rawKeys = http.Request.Query["keys"].ToString();
    var keys = string.IsNullOrEmpty(rawKeys)
        ? null
        : rawKeys.Split(',', StringSplitOptions.RemoveEmptyEntries);

    var report = await renderer.BuildAsync(id, keys);
    if (report is null) return Results.NotFound();

    using var ms = new MemoryStream();
    report.ExportToPdf(ms);
    var nombre = string.IsNullOrWhiteSpace(report.Name) ? $"reporte-{id}" : report.Name;
    report.Dispose();

    var inline = http.Request.Query["print"] == "1";
    // fileDownloadName presente => descarga (attachment); ausente => inline (se ve en el navegador para imprimir).
    return Results.File(ms.ToArray(), "application/pdf", inline ? null : $"{nombre}.pdf");
}).RequireAuthorization();

// PDF de los reportes predefinidos de un préstamo (fusiona los tildados en ?keys=ficha,vale). Inline para imprimir.
app.MapGet("/reportes/prestamo/{id:int}/pdf", async (int id, HttpContext http, Sgpa.Web.Reporting.Predefinidos.IPrestamoReportes reportes) =>
{
    var keys = http.Request.Query["keys"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
    if (keys.Length == 0) return Results.BadRequest("Sin reportes seleccionados.");

    var report = await reportes.BuildAsync(keys, id);
    if (report is null) return Results.NotFound();

    using var ms = new MemoryStream();
    report.ExportToPdf(ms);
    report.Dispose();
    return Results.File(ms.ToArray(), "application/pdf");   // inline (impresión / vista)
}).RequireAuthorization();

// PDF de los reportes predefinidos genéricos de una pantalla/entidad (fusiona los tildados en ?keys=). Los registros
// van en ?ids= (un id per-registro, o las claves seleccionadas en la grilla). Inline.
app.MapGet("/reportes/predef/{entidad}/pdf", async (string entidad, HttpContext http, Sgpa.Web.Reporting.Predefinidos.IReportesPredefinidos reportes) =>
{
    var keys = http.Request.Query["keys"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
    if (keys.Length == 0) return Results.BadRequest("Sin reportes seleccionados.");
    var ids = http.Request.Query["ids"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)
        .Select(s => long.TryParse(s, out var v) ? v : (long?)null).Where(v => v.HasValue).Select(v => v!.Value).ToList();

    var report = await reportes.BuildAsync(entidad, keys, ids);
    if (report is null) return Results.NotFound();

    using var ms = new MemoryStream();
    report.ExportToPdf(ms);
    report.Dispose();
    return Results.File(ms.ToArray(), "application/pdf");   // inline (impresión / vista)
}).RequireAuthorization();

// PDF de un Informe Estadístico (gráfico). Parámetros por query: empresa, mes (yyyymm), fini/ffin (período),
// fecha, tipo (Circular|Barras|Area). Inline para imprimir/ver.
app.MapGet("/reportes/estadistico/{idRpt:int}/pdf", async (int idRpt, HttpContext http, Sgpa.Web.Reporting.Predefinidos.IEstadisticaReportes reportes) =>
{
    static DateTime? Fecha(Microsoft.Extensions.Primitives.StringValues v)
        => DateTime.TryParse(v.ToString(), System.Globalization.CultureInfo.GetCultureInfo("es-UY"), System.Globalization.DateTimeStyles.None, out var d) ? d : null;

    var q = http.Request.Query;
    var prm = new Sgpa.Web.Reporting.Predefinidos.InformeParametros
    {
        CodEmpresa = int.TryParse(q["empresa"], out var e) ? e : 0,
        Mes = int.TryParse(q["mes"], out var m) ? m : null,
        FechaIni = Fecha(q["fini"]),
        FechaFin = Fecha(q["ffin"]),
        Fecha = Fecha(q["fecha"]),
        GrupoEtario = int.TryParse(q["ge"], out var g) ? g : 0,
        CodPatologia = int.TryParse(q["pat"], out var pat) ? pat : null,
    };
    Sgpa.Web.Reports.Estadisticos.TipoGrafico? tipo =
        Enum.TryParse<Sgpa.Web.Reports.Estadisticos.TipoGrafico>(q["tipo"], true, out var t) ? t : null;

    var report = await reportes.BuildAsync(idRpt, prm, tipo);
    if (report is null) return Results.NotFound();

    using var ms = new MemoryStream();
    report.ExportToPdf(ms);
    report.Dispose();
    return Results.File(ms.ToArray(), "application/pdf");   // inline
}).RequireAuthorization();

app.Run();
