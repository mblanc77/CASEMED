using FluentValidation;
using FluentValidation.Results;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Prestaciones;

/// <summary>
/// Avisos de negocio de la prestación (port de la parte informativa de AbmPrest.DatosOk): período de
/// renovación demasiado reciente y elegibilidad "no llega a 1,25 SMN". Son <b>soft validations</b>
/// (<see cref="Severity.Warning"/>, en el VB6 "¿desea ingresar igual?"): viven en el ruleset asíncrono
/// "Avisos" (consultan a base) y el CRUD genérico las corre en el gate de guardado (IncludeAllRuleSets)
/// para mostrarlas en el popup de continuar. Los requeridos los cubre la validación por metadata.
/// </summary>
public sealed class PrestacionValidator : AbstractValidator<Prestacion>
{
    public PrestacionValidator(PrestacionService service)
    {
        RuleSet("Avisos", () =>
        {
            RuleFor(p => p).CustomAsync(async (p, ctx, ct) =>
            {
                foreach (var aviso in await service.GetAvisosAsync(p, ct))
                    ctx.AddFailure(new ValidationFailure(nameof(Prestacion.CI), aviso) { Severity = Severity.Warning });
            });
        });
    }
}
