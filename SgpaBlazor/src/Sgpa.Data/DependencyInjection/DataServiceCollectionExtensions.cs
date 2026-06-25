using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sgpa.Data.Auditoria;
using Sgpa.Data.Configuracion;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Data.Errors;
using Sgpa.Data.Preferencias;
using Sgpa.Data.Security;
using Sgpa.Domain.Security;

namespace Sgpa.Data.DependencyInjection;

public static class DataServiceCollectionExtensions
{
    /// <summary>
    /// Registra el acceso a datos contra NewSgpa2 (factory de conexión + executor ADO.NET/Dapper),
    /// el CRUD genérico (open generic) y un usuario actual por defecto (reemplazable en Fase 5).
    /// </summary>
    public static IServiceCollection AddSgpaData(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(new SqlDbConnectionFactory(connectionString));
        services.AddScoped<IDbExecutor, DbExecutor>();
        services.AddScoped(typeof(ISgpaCrudService<>), typeof(DapperCrudService<>));
        services.AddScoped<ISgpaLookupService, DapperLookupService>();
        services.AddScoped<ISavedFilterService, DapperSavedFilterService>();
        services.AddScoped<ISecurityService, DapperSecurityService>();
        // Compilador de criterios de seguridad por registro: no-op por defecto (sin DevExpress); la capa Web
        // registra la implementación real que traduce el CriteriaOperator a FilterNode.
        services.TryAddScoped<ISecurityCriteriaCompiler, NoopSecurityCriteriaCompiler>();
        services.AddScoped<Security.Admin.ISeguridadAdminService, Security.Admin.SeguridadAdminService>();
        services.AddScoped<IErrorLog, ErrorLog>();
        services.AddScoped<IPreferenciaVistaStore, DapperPreferenciaVistaStore>();
        // Config dinámica por tabla (inline / confirmar borrado / auditar): singleton con cache en memoria.
        services.AddSingleton<ITablaConfigService, TablaConfigService>();
        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<Reporting.IReportGraphLoader, Reporting.ReportGraphLoader>();
        services.AddScoped<Reporting.IReporteDinamicoService, Reporting.DapperReporteDinamicoService>();
        services.AddScoped<Reporting.IReporteSqlService, Reporting.DapperReporteSqlService>();
        services.AddScoped<Reporting.IReportesMedidaCatalogo, Reporting.ReportesMedidaCatalogo>();
        services.TryAddScoped<ICurrentUser, DefaultCurrentUser>();
        return services;
    }
}
