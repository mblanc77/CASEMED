using FluentValidation;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Afiliados;

/// <summary>
/// Reglas de negocio del afiliado (FluentValidation), bloqueantes y por campo. Es el ejemplo de cómo
/// extender la validación: agregar un <c>AbstractValidator&lt;T&gt;</c> y queda enganchado solo en el CRUD.
///
/// El dígito verificador de la cédula NO bloquea acá: en el VB6 (AbmAfili.DatosOk → ChkCedula) era un aviso
/// "el dígito puede estar mal, ¿desea corregirlo?" que permitía guardar igual. Se informa como aviso no
/// bloqueante en <see cref="AfiliadoService.GetAvisosAsync"/>.
/// </summary>
public sealed class AfiliadoValidator : AbstractValidator<Afiliado>
{
    public AfiliadoValidator()
    {
        RuleFor(a => a.CI)
            .NotEmpty().WithMessage("La cédula es obligatoria.")
            .InclusiveBetween(1_000_000L, 999_999_999L)
            .WithMessage("La cédula debe tener entre 7 y 9 dígitos.");

        RuleFor(a => a.Nombres)
            .NotEmpty().WithMessage("El nombre es obligatorio.");

        RuleFor(a => a.Apellido1)
            .NotEmpty().WithMessage("El primer apellido es obligatorio.");
    }
}
