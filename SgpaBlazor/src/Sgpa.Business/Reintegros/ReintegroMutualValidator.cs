using FluentValidation;
using FluentValidation.Results;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Reintegros;

/// <summary>
/// Reglas de negocio del reintegro mutual (port de AbmReint.DatosOk): mes y año válidos (Severity.Error,
/// bloquean) y el aviso de elegibilidad "no llega a 1,25 SMN" como <b>soft validation</b>
/// (<see cref="Severity.Warning"/>, en el VB6 era "¿desea ingresar igual?"). Las advertencias viven en el
/// ruleset "Avisos" (asíncrono, consulta a base) para que la validación síncrona por campo no las dispare; el
/// CRUD genérico las corre en el gate de guardado (IncludeAllRuleSets) y las muestra en el popup de continuar.
/// </summary>
public sealed class ReintegroMutualValidator : AbstractValidator<ReintegroMutual>
{
    public ReintegroMutualValidator(ReintegroService service)
    {
        RuleFor(r => r.Mes).InclusiveBetween(1, 12).WithMessage("Ingresá un mes válido (1 a 12).");
        RuleFor(r => r.Anio).GreaterThan(0).WithMessage("Ingresá un año válido.");

        RuleSet("Avisos", () =>
        {
            RuleFor(r => r).CustomAsync(async (r, ctx, ct) =>
            {
                foreach (var aviso in await service.GetAvisosAsync(r, ct))
                    ctx.AddFailure(new ValidationFailure(nameof(ReintegroMutual.CI), aviso) { Severity = Severity.Warning });
            });
        });
    }
}
