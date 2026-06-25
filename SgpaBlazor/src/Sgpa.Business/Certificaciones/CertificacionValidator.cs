using FluentValidation;
using FluentValidation.Results;
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
    public CertificacionValidator(CertificacionService service)
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
            RuleFor(c => c).CustomAsync(async (c, ctx, ct) =>
            {
                foreach (var aviso in await service.GetAvisosDiasAsync(c, ct))
                    ctx.AddFailure(new ValidationFailure(nameof(Certificacion.CI), aviso) { Severity = Severity.Warning });
            });
        });
    }
}
