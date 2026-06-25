using FluentValidation;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Afiliados;

/// <summary>
/// Reglas de negocio del afiliado (FluentValidation), por campo. Es el ejemplo de cómo extender la validación:
/// agregar un <c>AbstractValidator&lt;T&gt;</c> y queda enganchado solo en el CRUD.
///
/// El dígito verificador de la cédula es una <b>soft validation</b> (<see cref="Severity.Warning"/>): en el VB6
/// (AbmAfili.DatosOk → ChkCedula) era un aviso "el dígito puede estar mal, ¿desea corregirlo?" que permitía
/// guardar igual. Como Warning, el CRUD genérico la muestra en un popup "continuar / cancelar" (estilo XAF) en
/// vez de bloquear; además sigue informándose como aviso en vivo vía <see cref="AfiliadoService.GetAvisosAsync"/>.
/// </summary>
public sealed class AfiliadoValidator : AbstractValidator<Afiliado>
{
    public AfiliadoValidator()
    {
        RuleFor(a => a.CI)
            .NotEmpty().WithMessage("La cédula es obligatoria.")
            .InclusiveBetween(1_000_000L, 999_999_999L)
            .WithMessage("La cédula debe tener entre 7 y 9 dígitos.");

        // Dígito verificador: NO bloquea (paridad VB6) — soft validation en el ruleset "Avisos" para que la
        // validación síncrona por campo no la dispare; el CRUD la corre en el gate de guardado (IncludeAllRuleSets)
        // y la muestra en el popup de continuar. Es el mismo patrón que Reintegro/Prestación/Certificación.
        RuleSet("Avisos", () =>
        {
            RuleFor(a => a.CI)
                .Must(ci => ci <= 0 || CedulaValidator.EsValida(ci))
                .WithSeverity(Severity.Warning)
                .WithMessage("El dígito verificador de la cédula no es correcto; verificá que el número esté bien ingresado.");
        });

        RuleFor(a => a.Nombres)
            .NotEmpty().WithMessage("El nombre es obligatorio.");

        RuleFor(a => a.Apellido1)
            .NotEmpty().WithMessage("El primer apellido es obligatorio.");
    }
}
