using FluentValidation;
using FluentValidation.Results;
using Sgpa.Business.Afiliados;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Certificaciones;

/// <summary>
/// Reglas síncronas de la certificación (port de AbmCerti.DatosOk): si es EFECTIVA, requiere fecha de
/// certificación, inicio y fin, y que el inicio no sea posterior al fin (Severity.Error, bloquean). Los avisos
/// por días (diferencia de período / topes 365-720, consulta a base) son <b>soft validations</b>
/// (<see cref="Severity.Warning"/>): viven en el ruleset asíncrono "Avisos" y el CRUD genérico los corre en el
/// gate de guardado (IncludeAllRuleSets) para mostrarlos en el popup de continuar.
/// </summary>
public sealed class CertificacionValidator : AbstractValidator<Certificacion>
{
    public CertificacionValidator(CertificacionService service, AfiliadoService afiliados)
    {
        // Estructural: una certificación es siempre de un afiliado. En el VB6 la cédula se validaba contra
        // Afiliado (txtCI_LostFocus) antes de grabar; acá, como mínimo, no se permite una certificación huérfana.
        RuleFor(c => c.CI).NotNull().GreaterThan(0)
            .WithMessage("La cédula del afiliado es obligatoria.");

        When(c => c.Efectiva, () =>
        {
            RuleFor(c => c.FechaCertificacion).NotNull()
                .WithMessage("La fecha de certificación es obligatoria para una certificación efectiva.");
            RuleFor(c => c.FechaIni).NotNull()
                .WithMessage("La fecha de inicio es obligatoria para una certificación efectiva.");
            RuleFor(c => c.FechaFin).NotNull()
                .WithMessage("La fecha de fin es obligatoria para una certificación efectiva.");
            RuleFor(c => c.FechaFin)
                .Must((c, fin) => c.FechaIni <= fin)
                .When(c => c.FechaIni.HasValue && c.FechaFin.HasValue)
                .WithMessage("La fecha de inicio no puede ser mayor que la de fin.");
        });

        RuleSet("Avisos", () =>
        {
            // FK del afiliado (port de txtCI_LostFocus): la cédula debe existir en Afiliado —bloquea (Error)— y el
            // afiliado debería estar activo (≥3 aportes/últimos 12) —en el VB6 era un aviso, acá Warning no bloqueante.
            RuleFor(c => c).CustomAsync(async (c, ctx, ct) =>
            {
                if (c.CI is not > 0) return;
                if (!await afiliados.ExisteAsync(c.CI.Value, ct))
                {
                    ctx.AddFailure(new ValidationFailure(nameof(Certificacion.CI),
                        "No existe la cédula ingresada en los afiliados.") { Severity = Severity.Error });
                    return;
                }
                if (!await afiliados.EsActivoAsync(c.CI.Value, ct))
                    ctx.AddFailure(new ValidationFailure(nameof(Certificacion.CI),
                        "El afiliado no está activo (no aportó al menos 3 meses en los últimos 12). Verificá antes de certificar.")
                        { Severity = Severity.Warning });
            });

            RuleFor(c => c).CustomAsync(async (c, ctx, ct) =>
            {
                foreach (var aviso in await service.GetAvisosDiasAsync(c, ct))
                    ctx.AddFailure(new ValidationFailure(nameof(Certificacion.CI), aviso) { Severity = Severity.Warning });
            });
        });
    }
}
