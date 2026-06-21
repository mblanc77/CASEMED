using FluentValidation;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Certificaciones;

/// <summary>
/// Reglas síncronas de la certificación (port de AbmCerti.DatosOk): si es EFECTIVA, requiere fecha de
/// certificación, inicio y fin, y que el inicio no sea posterior al fin. La superposición con otra
/// certificación (consulta a base) y los avisos por días siguen en <see cref="CertificacionService"/>.
/// </summary>
public sealed class CertificacionValidator : AbstractValidator<Certificacion>
{
    public CertificacionValidator()
    {
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
    }
}
