using FluentValidation;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Imponibles;

/// <summary>
/// Reglas síncronas del imponible. Las columnas obligatorias (clave compuesta + Concepto) ya las exige la
/// metadata (Required) vía el DetailView genérico; acá se valida el rango del período: mes entre 1 y 12, y
/// año entre 1900 y el año corriente + 10. Se auto-registra (AddValidatorsFromAssembly) y el SgpaMetadataValidator
/// lo engancha solo: campo en rojo + gate de guardado, tanto en la grilla anidada como en el ABM de /imponibles.
/// </summary>
public sealed class ImponibleValidator : AbstractValidator<Imponible>
{
    public ImponibleValidator()
    {
        // Identificadores de la clave: deben ser positivos (impide el 0 del alta sin completar). En edición la
        // clave es de solo lectura y ya es válida, así que esto efectivamente sólo gatea el alta.
        RuleFor(i => i.CI)
            .GreaterThan(0L)
            .WithMessage("La cédula (CI) es obligatoria.");

        RuleFor(i => i.CodEmpresa)
            .GreaterThan(0)
            .WithMessage("La empresa es obligatoria.");

        RuleFor(i => i.Mes)
            .InclusiveBetween((byte)1, (byte)12)
            .WithMessage("El mes debe estar entre 1 y 12.");

        RuleFor(i => i.Anio)
            .InclusiveBetween(1900, DateTime.Today.Year + 10)
            .WithMessage(_ => $"El año debe estar entre 1900 y {DateTime.Today.Year + 10}.");
    }
}
