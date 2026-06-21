using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Radzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

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
    return new HttpClient{BaseAddress = new Uri(baseAddress)};
});
builder.Services.AddScoped<SGPA.Server.CMUService>();
builder.Services.AddDbContext<SGPA.Server.Data.CMUContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CMUConnection"));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderCMU = new ODataConventionModelBuilder();
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ActaConsejo>("ActaConsejos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AgenteCobranza>("AgenteCobranzas");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AgenteCobranzaDebito>("AgenteCobranzaDebitos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AgenteCobranzaTipo>("AgenteCobranzaTipos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AgenteGrupo>("AgenteGrupos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AjusteDetalle>("AjusteDetalles");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AjusteRetroactivo>("AjusteRetroactivos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Analysis>("Analyses");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AreaContacto>("AreaContactos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AuditDataItemPersistent>("AuditDataItemPersistents");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.AuditedObjectWeakReference>("AuditedObjectWeakReferences");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.BajaMotivo>("BajaMotivos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.BajaTemporalMotivo>("BajaTemporalMotivos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Banco>("Bancos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.CargoContacto>("CargoContactos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.CategoriaColegiado>("CategoriaColegiados");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.CategoriaColegiadoValor>("CategoriaColegiadoValors");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Cjp>("Cjps");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.CjpMat>("CjpMats");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.CjpOld>("CjpOlds");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Cobro>("Cobros");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.CobroNomina>("CobroNominas");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Colegiado>("Colegiados");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>("ColegiadoActualizacionDps");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoBitacora>("ColegiadoBitacoras");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>("ColegiadoBitacoraEMailEnvios");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>("ColegiadoBitacoraEMailRecepcions");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>("ColegiadoBitacoraNota");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>("ColegiadoCambioCategoria");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>("ColegiadoCertificadoExpedidos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>("ColegiadoDebitoBancarioAsociados");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>("ColegiadoDeclaracionJurada");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoImagene>("ColegiadoImagenes");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoMovimiento>("ColegiadoMovimientos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Colegiados2011>("Colegiados2011S");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>("ColegiadoTarjetaDebitoAsociada");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Contacto>("Contactos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ContactoInfoAdicional>("ContactoInfoAdicionals");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Convenio>("Convenios");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ConvenioFinanciacion>("ConvenioFinanciacions");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.CuentaBancarium>("CuentaBancaria");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Debito>("Debitos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DebitoAdjunto>("DebitoAdjuntos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DebitoNomina>("DebitoNominas");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>("DeclaracionJuradaAdjuntos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>("DeclaracionJuradaTipos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Departamento>("Departamentos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Deposito>("Depositos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DepositoNomina>("DepositoNominas");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>("DepositoNominaMultiBrous");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>("DepositoNominaNoIdentificada");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DepositoNominaRedPago>("DepositoNominaRedPagos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DjInactividadMotivo>("DjInactividadMotivos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.DynamicListViewFilter>("DynamicListViewFilters");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.EmailEnvio>("EmailEnvios");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Especialidad>("Especialidads");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.FacultadTitulo>("FacultadTitulos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.FileDatum>("FileData");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.GrupoContacto>("GrupoContactos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>("GrupoLugarRetiroCarnes");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.KpiDefinition>("KpiDefinitions");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.KpiHistoryItem>("KpiHistoryItems");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.KpiInstance>("KpiInstances");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.KpiScorecard>("KpiScorecards");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>("KpiscorecardscorecardsKpiinstanceindicators");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.LugarRetiroCarne>("LugarRetiroCarnes");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.MensajePush>("MensajePushes");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.MensajePushAdd>("MensajePushAdds");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.MensajeSegmento>("MensajeSegmentos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ModuleInfo>("ModuleInfos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.MovimientoCuentum>("MovimientoCuenta");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>("MovimientoCuentaCuota");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.MovimientoCuentaManual>("MovimientoCuentaManuals");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.MovimientoTipo>("MovimientoTipos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.MyFileDatum>("MyFileData");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.OrigenMovimiento>("OrigenMovimientos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Pai>("Pais");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Parametro>("Parametros");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Region>("Regions");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Regional>("Regionals");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>("RegionalregionalesCuentabancariacuentabancaria");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.RegistroColegiado>("RegistroColegiados");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>("RegistroColegiadoNotificacions");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>("RegistroColegiadoRechazoParams");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ReportDatum>("ReportData");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.ReportDataV2>("ReportDataV2S");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Rol>("Rols");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>("RolrolesMovimientotipomovimientostipos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SalaCmu>("SalaCmus");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SalaOrganizador>("SalaOrganizadors");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SalaReserva>("SalaReservas");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SalaReservaRegistro>("SalaReservaRegistros");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>("SecuritySystemMemberPermissionsObjects");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>("SecuritySystemObjectPermissionsObjects");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SecuritySystemRole>("SecuritySystemRoles");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>("SecuritysystemroleparentrolesSecuritysystemrolechildroles");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>("SecuritySystemTypePermissionsObjects");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SecuritySystemUser>("SecuritySystemUsers");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>("SecuritysystemuserusersSecuritysystemroleroles");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SolicitudBaja>("SolicitudBajas");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>("SolicitudBajaFileAttachments");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TmpCarneEntregado>("TmpCarneEntregados");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TmpCarneRetirar>("TmpCarneRetirars");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TmpFecha>("TmpFechas");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TmpMese>("TmpMeses").EntityType.HasKey(entity => new
    {
    entity.Mes, entity.Año
    });
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>("TramiteInfoadjuntabases");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>("TramiteInfoadjuntacedulas");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>("TramiteInfoadjuntaespecialidads");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>("TramiteInfoadjuntafotocarnes");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>("TramiteInfoadjuntatitulos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteCarne>("TramiteCarnes");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteCarneEstado>("TramiteCarneEstados");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>("TramiteCarneEstadoCodigos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>("TramiteCarneEstadoWorkFlows");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Universidad>("Universidads");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.UniversidadTituloGrado>("UniversidadTituloGrados");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.UserResetPasswordRequest>("UserResetPasswordRequests");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.Usuario>("Usuarios");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.UsuarioAcceso>("UsuarioAccesos");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.UsuarioInstitucion>("UsuarioInstitucions");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.UsuarioRegional>("UsuarioRegionals");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.XpObjectModified>("XpObjectModifieds");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.XpObjectType>("XpObjectTypes");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.XpoState>("XpoStates");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.XpoStateAppearance>("XpoStateAppearances");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.XpoStateMachine>("XpoStateMachines");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.XpoTransition>("XpoTransitions");
    oDataBuilderCMU.EntitySet<SGPA.Server.Models.CMU.XpWeakReference>("XpWeakReferences");
    opt.AddRouteComponents("odata/CMU", oDataBuilderCMU.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<SGPA.Client.CMUService>();
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
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/_Host");
app.Run();