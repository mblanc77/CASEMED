using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sgpa.Business.Afiliados;
using Sgpa.Business.Certificaciones;
using Sgpa.Business.Dashboard;
using Sgpa.Business.Empresas;
using Sgpa.Business.Mutualistas;
using Sgpa.Business.Pagos;
using Sgpa.Business.Prestaciones;
using Sgpa.Business.Reintegros;
using Sgpa.Business.Retenciones;
using Sgpa.Business.Prestamos;
using Sgpa.Business.Subsidios;
using Sgpa.Business.Subsidios.Export;
using Sgpa.Business.Subsidios.Import;

namespace Sgpa.Business.DependencyInjection;

public static class BusinessServiceCollectionExtensions
{
    /// <summary>Registra la lógica de negocio (cálculo de subsidios, IRPF, exportadores bancarios).</summary>
    public static IServiceCollection AddSgpaBusiness(this IServiceCollection services)
    {
        services.AddScoped<IIrpfCalculator, IrpfCalculator>();
        services.AddScoped<ISubsidioRepository, SubsidioRepository>();
        services.AddScoped<ISubsidioLiquidacionService, SubsidioLiquidacionService>();

        // Orquestación CUD: el subsidio (liquidación masiva o edición manual del ABM) mantiene el imponible
        // emp900 que alimenta el jornal de liquidaciones futuras.
        services.AddScoped<IImponibleSubsidioSync, ImponibleSubsidioSync>();
        services.AddScoped<Sgpa.Data.Crud.IEntityChangeHandler<Sgpa.Domain.Entities.SubsidioCabezal>,
            SubsidioCabezalImponibleHandler>();
        services.AddScoped<NbcExporter>();
        services.AddScoped<BrouExporter>();
        services.AddScoped<SubsidioExtraExporter>();
        services.AddScoped<SubsidioImporter>();
        services.AddScoped<PrestamoRepository>();
        services.AddScoped<PrestamoGrabadoService>();
        services.AddScoped<PrestamoAccionesService>();
        services.AddScoped<RefinanciacionService>();
        services.AddScoped<PagoService>();
        services.AddScoped<RetencionService>();
        services.AddScoped<CertificacionService>();
        services.AddScoped<PrimaService>();
        services.AddScoped<AfiliadoService>();
        services.AddScoped<AdPreJubService>();
        services.AddScoped<ReintegroService>();
        services.AddScoped<PrestacionService>();
        services.AddScoped<RecetaService>();
        services.AddScoped<EmpresaService>();
        services.AddScoped<MutualistaService>();
        services.AddScoped<DashboardService>();

        // Validadores de negocio (FluentValidation): se descubren todos los AbstractValidator<T> de este
        // assembly y quedan disponibles como IValidator<T> para el CRUD genérico (por campo + gate de guardado).
        services.AddValidatorsFromAssemblyContaining<AfiliadoValidator>(ServiceLifetime.Scoped);
        return services;
    }
}
