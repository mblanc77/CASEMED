using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Radzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using SgpaNew.Server.Data;
using Microsoft.AspNetCore.Identity;
using SgpaNew.Server.Models;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddSingleton(sp =>
{
    // Get the address that the app is currently running at
    var server = sp.GetRequiredService<IServer>();
    var addressFeature = server.Features.Get<IServerAddressesFeature>();
    string baseAddress = addressFeature.Addresses.First();
    return new HttpClient
    {
        BaseAddress = new Uri(baseAddress)
    };
});
builder.Services.AddScoped<SgpaNew.Server.SgpaService>();
builder.Services.AddDbContext<SgpaNew.Server.Data.SgpaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SgpaConnection"));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderSgpa = new ODataConventionModelBuilder();
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.AdPreJub>("AdPreJubs");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.AdPreJubPago>("AdPreJubPagos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>("AfeccionGrupos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.AfeccionTipo>("AfeccionTipos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Afiliado>("Afiliados");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>("AfiliadoApuntes");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>("AfiliadoEspecialidads");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.AporteTipo>("AporteTipos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.BajaMotivo>("BajaMotivos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Banco>("Bancos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Certificacion>("Certificacions");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>("CertificacionProrrogas");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Certificador>("Certificadors");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Ctasbrou>("Ctasbrous");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Departamento>("Departamentos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Empresa>("Empresas");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.EmpresaPago>("EmpresaPagos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Especialidad>("Especialidads");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.FormaPago>("FormaPagos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.FranjaIrpf>("FranjaIrpfs");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Imponible>("Imponibles");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.InformeEstadistico>("InformeEstadisticos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.LiquidacionBp>("LiquidacionBps");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.MaeFun>("MaeFuns");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Monedum>("Moneda");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Mutualistum>("Mutualista");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.NoCargadoHl>("NoCargadoHls");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Patologium>("Patologia");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Prestacion>("Prestacions");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.PrestacionTipo>("PrestacionTipos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>("PrimaFallecimientos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Recetum>("Receta");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.RecetaDistancium>("RecetaDistancia");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.RegimenAporte>("RegimenAportes");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>("RegimenJubilatorios");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.ReintegroMutual>("ReintegroMutuals");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SalidaTipo>("SalidaTipos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Seleccion>("Seleccions");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SituacionMutual>("SituacionMutuals");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SituacionPago>("SituacionPagos");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>("SubsidioCabezals");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>("SubsidiocabezalBps");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>("SubsidioCabezalEmpresas");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>("SubsidioEnfermedads");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SubsidioItem>("SubsidioItems");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>("SubsidioItemCods");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>("SubsidioitemcodAfiliados");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>("SubsidioItemEmpresas");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.Trabaja>("Trabajas");
    oDataBuilderSgpa.EntitySet<SgpaNew.Server.Models.Sgpa.XUsrParam>("XUsrParams");
    oDataBuilderSgpa.Function("Cuenta").Returns<SgpaNew.Server.Models.Sgpa.Cuentum>();
    oDataBuilderSgpa.Function("Discounts").Returns<SgpaNew.Server.Models.Sgpa.Discount>();
    oDataBuilderSgpa.Function("Imps").Returns<SgpaNew.Server.Models.Sgpa.Imp>();
    oDataBuilderSgpa.Function("Parametros").Returns<SgpaNew.Server.Models.Sgpa.Parametro>();
    oDataBuilderSgpa.Function("SubsidioImponibles").Returns<SgpaNew.Server.Models.Sgpa.SubsidioImponible>();
    opt.AddRouteComponents("odata/Sgpa", oDataBuilderSgpa.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<SgpaNew.Client.SgpaService>();
builder.Services.AddHttpClient("SgpaNew.Server").AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddScoped<SgpaNew.Client.SecurityService>();
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SgpaConnection"));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers().AddOData(o =>
{
    var oDataBuilder = new ODataConventionModelBuilder();
    oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
    var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.Password)));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.ConfirmPassword)));
    oDataBuilder.EntitySet<ApplicationRole>("ApplicationRoles");
    o.AddRouteComponents("odata/Identity", oDataBuilder.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<AuthenticationStateProvider, SgpaNew.Client.ApplicationAuthenticationStateProvider>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseHeaderPropagation();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/_Host");
app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>().Database.Migrate();
app.Run();