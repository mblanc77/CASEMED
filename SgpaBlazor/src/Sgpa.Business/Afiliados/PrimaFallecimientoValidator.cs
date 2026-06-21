using FluentValidation;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Afiliados;

/// <summary>
/// Regla de grabado de la prima por fallecimiento (port de AbmAfili.cmdGrabarPrima_Click): hay que ingresar
/// al menos una de las dos fechas (firma o fallecimiento). El error se publica bajo la propiedad "Fechas"
/// (no es de un campo único). La exigencia de fecha de fallecimiento para LIQUIDAR es una regla de esa
/// acción y se valida aparte en el panel.
/// </summary>
public sealed class PrimaFallecimientoValidator : AbstractValidator<PrimaFallecimiento>
{
    public PrimaFallecimientoValidator()
    {
        RuleFor(p => p.FechaFallecimiento)
            .Must((p, _) => p.FechaFirma.HasValue || p.FechaFallecimiento.HasValue)
            .OverridePropertyName("Fechas")
            .WithMessage("Debe ingresar la fecha de la firma o la fecha de fallecimiento.");
    }
}
