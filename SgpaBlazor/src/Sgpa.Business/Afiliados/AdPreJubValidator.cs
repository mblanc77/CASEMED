using FluentValidation;
using FluentValidation.Results;
using Sgpa.Domain.Entities;

namespace Sgpa.Business.Afiliados;

/// <summary>
/// Reglas del adelanto prejubilatorio (port de AbmAfili.AdPreJubOk). Lo estructural bloquea (Error): cédula de
/// un afiliado existente y fecha de presentación. Los requisitos de elegibilidad del afiliado (régimen
/// jubilatorio cargado, no estar activo, haber tenido subsidio por enfermedad) en el VB6 bloqueaban en el alta;
/// acá se informan como avisos (Warning) para no impedir editar un adelanto ya existente si luego cambia la
/// situación del afiliado. Las reglas async viven en el ruleset "Avisos" (gate de guardado).
/// </summary>
public sealed class AdPreJubValidator : AbstractValidator<AdPreJub>
{
    public AdPreJubValidator(AfiliadoService afiliados, AdPreJubService adelanto)
    {
        RuleFor(a => a.CI).GreaterThan(0).WithMessage("La cédula del afiliado es obligatoria.");
        RuleFor(a => a.FechaPresentacion).NotNull().WithMessage("Debe ingresar la fecha de presentación.");

        RuleSet("Avisos", () =>
        {
            RuleFor(a => a).CustomAsync(async (a, ctx, ct) =>
            {
                if (a.CI <= 0) return;
                if (!await afiliados.ExisteAsync(a.CI, ct))
                {
                    ctx.AddFailure(new ValidationFailure(nameof(AdPreJub.CI),
                        "No existe la cédula ingresada en los afiliados.") { Severity = Severity.Error });
                    return;
                }

                if (!await adelanto.TieneRegimenJubilatorioAsync(a.CI, ct))
                    ctx.AddFailure(new ValidationFailure(nameof(AdPreJub.CI),
                        "El afiliado no tiene cargado el régimen jubilatorio.") { Severity = Severity.Warning });

                if (await adelanto.TieneTrabajoActivoAsync(a.CI, ct))
                    ctx.AddFailure(new ValidationFailure(nameof(AdPreJub.CI),
                        "El afiliado está activo (tiene empleo vigente), por lo tanto no debería generar el adelanto.")
                        { Severity = Severity.Warning });

                if (!await adelanto.TuvoSubsidioEnfermedadAsync(a.CI, ct))
                    ctx.AddFailure(new ValidationFailure(nameof(AdPreJub.CI),
                        "El afiliado no tuvo ningún subsidio por enfermedad.") { Severity = Severity.Warning });
            });
        });
    }
}
