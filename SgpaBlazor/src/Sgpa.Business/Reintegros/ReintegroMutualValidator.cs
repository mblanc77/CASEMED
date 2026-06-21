using FluentValidation;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Reintegros;

/// <summary>
/// Reglas de negocio del reintegro mutual (port de la parte síncrona de AbmReint.DatosOk): mes y año válidos.
/// El aviso de "no llega a 1,25 SMN" es NO bloqueante (en el VB6 era "¿desea ingresar igual?") y se informa
/// vía <see cref="ReintegroService.GetAvisosAsync"/>.
/// </summary>
public sealed class ReintegroMutualValidator : AbstractValidator<ReintegroMutual>
{
    public ReintegroMutualValidator()
    {
        RuleFor(r => r.Mes).InclusiveBetween(1, 12).WithMessage("Ingresá un mes válido (1 a 12).");
        RuleFor(r => r.Anio).GreaterThan(0).WithMessage("Ingresá un año válido.");
    }
}
