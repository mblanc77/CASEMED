using System;
using System.Collections.Generic;
using Sgpa.Domain.Security;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Unit tests (sin BD) de la lógica de reglas granulares de <see cref="UserSecurityContext"/>.</summary>
public class SecurityContextRulesTests
{
    private static UserSecurityContext Ctx(bool admin = false,
        Dictionary<string, ColumnRule>? columnas = null,
        Dictionary<(string, PermissionAction), RecordRule>? registros = null) => new()
    {
        Login = "u1",
        IsAdmin = admin,
        TablePermissions = new Dictionary<string, PermissionAction>(StringComparer.OrdinalIgnoreCase)
        {
            ["Afiliado"] = PermissionAction.All
        },
        ColumnRules = columnas ?? new(StringComparer.OrdinalIgnoreCase),
        RecordRules = registros ?? new()
    };

    [Fact]
    public void Admin_omite_todas_las_restricciones()
    {
        var ctx = Ctx(admin: true,
            columnas: new(StringComparer.OrdinalIgnoreCase) { [UserSecurityContext.Key("Afiliado", "Sueldo")] = new(false, false) },
            registros: new() { [("Afiliado", PermissionAction.Read)] = new(false, new[] { "x" }) });

        Assert.True(ctx.CanReadColumn("Afiliado", "Sueldo"));
        Assert.True(ctx.CanWriteColumn("Afiliado", "Sueldo"));
        Assert.Null(ctx.RecordFilter("Afiliado", PermissionAction.Read));
    }

    [Fact]
    public void Columna_sin_regla_es_legible_y_modificable()
    {
        var ctx = Ctx();
        Assert.True(ctx.CanReadColumn("Afiliado", "Nombre"));
        Assert.True(ctx.CanWriteColumn("Afiliado", "Nombre"));
    }

    [Fact]
    public void Columna_con_restriccion_se_respeta()
    {
        var ctx = Ctx(columnas: new(StringComparer.OrdinalIgnoreCase)
        {
            [UserSecurityContext.Key("Afiliado", "Sueldo")] = new(Read: false, Write: false),
            [UserSecurityContext.Key("Afiliado", "Telefono")] = new(Read: true, Write: false),
        });

        Assert.False(ctx.CanReadColumn("Afiliado", "Sueldo"));
        Assert.False(ctx.CanWriteColumn("Afiliado", "Sueldo"));
        Assert.True(ctx.CanReadColumn("Afiliado", "Telefono"));
        Assert.False(ctx.CanWriteColumn("Afiliado", "Telefono"));
    }

    [Fact]
    public void RecordFilter_devuelve_la_regla_de_la_accion()
    {
        var rule = new RecordRule(false, new[] { "[CodMutualista] = 5" });
        var ctx = Ctx(registros: new() { [("Afiliado", PermissionAction.Read)] = rule });

        Assert.Same(rule, ctx.RecordFilter("Afiliado", PermissionAction.Read));
        Assert.Null(ctx.RecordFilter("Afiliado", PermissionAction.Write));   // no configurado → sin restricción
        Assert.Null(ctx.RecordFilter("Afiliado", PermissionAction.Delete));
    }
}
