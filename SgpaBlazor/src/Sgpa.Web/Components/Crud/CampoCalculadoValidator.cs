using DevExpress.Data.Filtering;
using FluentValidation;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Sgpa.Domain.Metadata;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Validación de un <see cref="CampoCalculado"/> al darlo de alta/editar en el CRUD genérico. Vive en Web (no en
/// Sgpa.Business) porque necesita DevExpress para parsear la expresión. Verifica: tabla existente, nombre identificador
/// que no choque con una columna real, tipo de resultado válido, y que la expresión parsee y referencie sólo columnas
/// existentes de la tabla.
/// </summary>
public sealed class CampoCalculadoValidator : AbstractValidator<CampoCalculado>
{
    private static readonly string[] Tipos = { "int", "decimal", "datetime", "bool", "string" };

    public CampoCalculadoValidator()
    {
        RuleFor(c => c.Tabla)
            .NotEmpty().WithMessage("Indicá la tabla.")
            .Must(t => EntityCatalog.TryGet(t) is not null).WithMessage("La tabla no existe en el catálogo.");

        RuleFor(c => c.Nombre)
            .NotEmpty().WithMessage("Indicá el nombre del campo.")
            .Must(EsIdentificador).WithMessage("El nombre debe ser un identificador (letras, dígitos o '_', sin espacios).");

        RuleFor(c => c.TipoResultado)
            .Must(t => t is not null && Tipos.Contains(t.Trim().ToLowerInvariant()))
            .WithMessage($"Tipo de resultado inválido. Use: {string.Join(", ", Tipos)}.");

        // Nombre no debe pisar una columna real de la tabla.
        RuleFor(c => c)
            .Must(c => EntityCatalog.TryGet(c.Tabla) is not { } m
                       || !m.Columns.Any(col => col.Name.Equals(c.Nombre, StringComparison.OrdinalIgnoreCase)))
            .WithMessage("Ya existe una columna real con ese nombre en la tabla.")
            .WithName(nameof(CampoCalculado.Nombre));

        // La expresión debe parsear y referenciar sólo columnas existentes de la tabla.
        RuleFor(c => c.Expr)
            .NotEmpty().WithMessage("Indicá la expresión.")
            .Custom((expr, ctx) =>
            {
                var tabla = ctx.InstanceToValidate.Tabla;
                var meta = EntityCatalog.TryGet(tabla);
                if (meta is null || string.IsNullOrWhiteSpace(expr)) return;
                ScalarNode node;
                try { node = SgpaScalarTranslator.Translate(CriteriaOperator.Parse(expr)); }
                catch (Exception ex) { ctx.AddFailure($"Expresión inválida: {ex.Message}"); return; }

                foreach (var colName in ColumnasDe(node))
                    if (!meta.Columns.Any(col => col.Name.Equals(colName, StringComparison.OrdinalIgnoreCase)
                                              || col.Property.Name.Equals(colName, StringComparison.OrdinalIgnoreCase)))
                        ctx.AddFailure($"La columna '{colName}' no existe en {tabla}.");
            });
    }

    private static bool EsIdentificador(string? s)
        => !string.IsNullOrWhiteSpace(s) && s.All(ch => char.IsLetterOrDigit(ch) || ch == '_') && !char.IsDigit(s[0]);

    // Recolecta los nombres de columna referenciados por la expresión.
    private static IEnumerable<string> ColumnasDe(ScalarNode node) => node switch
    {
        ScalarColumn c => new[] { c.Name },
        ScalarConst => Array.Empty<string>(),
        ScalarNegate u => ColumnasDe(u.Operand),
        ScalarBinary b => ColumnasDe(b.Left).Concat(ColumnasDe(b.Right)),
        ScalarCondition cn => ColumnasDe(cn.Left).Concat(ColumnasDe(cn.Right)),
        ScalarFunc f => f.Args.SelectMany(ColumnasDe),
        _ => Array.Empty<string>()
    };
}
