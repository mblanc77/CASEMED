using System.Linq;
using System.Threading.Tasks;
using Sgpa.Data;
using Sgpa.Data.Connection;
using Sgpa.Data.Crud;
using Sgpa.Domain.Entities;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Integración contra NewSgpa2: filtrado por columna del CRUD genérico (master-detail).
/// El afiliado 13010559 tiene empleos/especialidades/apuntes conocidos.
/// </summary>
public class CrudFilterTests
{
    private const string ConnectionString =
        "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

    private const long Ci = 13010559;

    private static DapperCrudService<T> Svc<T>() where T : class =>
        TestCrud.Create<T>(new DbExecutor(new SqlDbConnectionFactory(ConnectionString)), new FakeCurrentUser());

    [Fact]
    public async Task GetByColumn_filtra_empleos_del_afiliado()
    {
        var empleos = await Svc<Trabaja>().GetByColumnAsync("CI", Ci);

        Assert.NotEmpty(empleos);
        Assert.All(empleos, e => Assert.Equal(Ci, e.CI)); // sólo filas del padre
    }

    [Fact]
    public async Task GetByColumn_filtra_especialidades_y_apuntes()
    {
        var esp = await Svc<AfiliadoEspecialidad>().GetByColumnAsync("CI", Ci);
        var apuntes = await Svc<AfiliadoApunte>().GetByColumnAsync("CI", Ci);

        Assert.All(esp, e => Assert.Equal(Ci, e.CI));
        Assert.All(apuntes, a => Assert.Equal(Ci, a.CI));
    }

    [Fact]
    public async Task GetByColumn_columna_inexistente_lanza()
    {
        await Assert.ThrowsAsync<System.ArgumentException>(
            () => Svc<Trabaja>().GetByColumnAsync("NoExiste", Ci));
    }

    // Filtro EXISTS sobre la relación 1-N Afiliado→Empleos (Trabaja): "afiliados con algún empleo en la empresa X".
    [Fact]
    public async Task FilterExists_afiliados_con_empleo_en_empresa()
    {
        var empleos = await Svc<Trabaja>().GetByColumnAsync("CI", Ci);
        Assert.NotEmpty(empleos);
        var emp = empleos[0].CodEmpresa;

        var trabaja = Sgpa.Domain.Metadata.EntityMetadata.For<Trabaja>();

        // Afiliado Ci AND EXISTS(empleo con CodEmpresa = emp) → lo encuentra (1).
        var conEmpresa = new FilterGroup(true, new FilterNode[]
        {
            new FilterCompare("CI", FilterOp.Equal, Ci),
            new FilterExists(trabaja, "CI", "CI", new FilterCompare("CodEmpresa", FilterOp.Equal, emp)),
        });
        Assert.Equal(1, (await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 1, Filter: conEmpresa))).TotalCount);

        // Mismo afiliado AND EXISTS(empleo con CodEmpresa imposible) → 0.
        var sinEmpresa = new FilterGroup(true, new FilterNode[]
        {
            new FilterCompare("CI", FilterOp.Equal, Ci),
            new FilterExists(trabaja, "CI", "CI", new FilterCompare("CodEmpresa", FilterOp.Equal, -999999)),
        });
        Assert.Equal(0, (await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 1, Filter: sinEmpresa))).TotalCount);

        // NOT EXISTS: el afiliado NO tiene empleo en esa empresa imposible → lo encuentra (1).
        var negate = new FilterGroup(true, new FilterNode[]
        {
            new FilterCompare("CI", FilterOp.Equal, Ci),
            new FilterExists(trabaja, "CI", "CI", new FilterCompare("CodEmpresa", FilterOp.Equal, -999999), Negate: true),
        });
        Assert.Equal(1, (await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 1, Filter: negate))).TotalCount);
    }

    // Filtro EXISTS sobre una relación N-1 (referencia/FK): "afiliados cuyo Banco se llama …".
    // N-1 se modela igual que 1-N: hija.PK (Banco.CodBanco) = padre.FK (Afiliado.CodBanco).
    [Fact]
    public async Task FilterExists_afiliados_por_descripcion_de_banco()
    {
        // Un afiliado con banco asignado y la descripción de ese banco.
        var afi = (await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 200))).Items.FirstOrDefault(a => a.CodBanco != null);
        Assert.NotNull(afi);
        var banco = (await Svc<Banco>().GetByColumnAsync("CodBanco", afi!.CodBanco)).Single();

        var bancoMeta = Sgpa.Domain.Metadata.EntityMetadata.For<Banco>();

        // EXISTS(Banco.CodBanco = Afiliado.CodBanco AND Banco.Descripcion = <la del banco>) → al menos ese afiliado.
        var conBanco = new FilterExists(bancoMeta, "CodBanco", "CodBanco",
            new FilterCompare("Descripcion", FilterOp.Equal, banco.Descripcion));
        Assert.True((await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 1, Filter: conBanco))).TotalCount >= 1);

        // Descripción imposible → 0.
        var bogus = new FilterExists(bancoMeta, "CodBanco", "CodBanco",
            new FilterCompare("Descripcion", FilterOp.Equal, "___NO_EXISTE_BANCO___"));
        Assert.Equal(0, (await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 1, Filter: bogus))).TotalCount);
    }

    // Filtro por CAMPO CALCULADO en el CRUD tipado (server-side): duración de la certificación = FechaFin-FechaIni+1.
    [Fact]
    public async Task Filtra_por_campo_calculado_duracion()
    {
        var duracion = new CalculatedField("Certificacion", "Duracion", "Duración",
            new ScalarBinary(ScalarBinOp.Add,
                new ScalarFunc(ScalarFn.DateDiffDay, new ScalarNode[] { new ScalarColumn("FechaIni"), new ScalarColumn("FechaFin") }),
                new ScalarConst(1)),
            typeof(int), null);

        var conDuracion = new PageQuery(0, 1, Filter: new FilterCompare("Duracion", FilterOp.GreaterOrEqual, 1)) { Calc = new[] { duracion } };
        Assert.True((await Svc<Certificacion>().GetPageAsync(conDuracion)).TotalCount > 0);

        var imposible = new PageQuery(0, 1, Filter: new FilterCompare("Duracion", FilterOp.Greater, 100000)) { Calc = new[] { duracion } };
        Assert.Equal(0, (await Svc<Certificacion>().GetPageAsync(imposible)).TotalCount);
    }

    private static CalculatedField DuracionCalc() => new(
        "Certificacion", "Duracion", "Duración",
        new ScalarBinary(ScalarBinOp.Add,
            new ScalarFunc(ScalarFn.DateDiffDay, new ScalarNode[] { new ScalarColumn("FechaIni"), new ScalarColumn("FechaFin") }),
            new ScalarConst(1)),
        typeof(int), null);

    // 4b: ordenar / agrupar / totalizar POR un campo calculado en el CRUD tipado (server-side).
    [Fact]
    public async Task Ordena_agrupa_totaliza_por_campo_calculado()
    {
        var calc = new[] { DuracionCalc() };
        var ctx = new PageQuery(0, 1) { Calc = calc };

        var ordenado = await Svc<Certificacion>().GetPageAsync(
            new PageQuery(0, 5, Sort: new[] { new SortColumn("Duracion", true) }) { Calc = calc });
        Assert.NotEmpty(ordenado.Items);

        var grupos = await Svc<Certificacion>().GetGroupsAsync("Duracion", false, ctx, System.Array.Empty<SummarySpec>());
        Assert.NotEmpty(grupos);

        var totales = await Svc<Certificacion>().GetTotalSummaryAsync(new[] { new SummarySpec("Duracion", AggKind.Sum) }, ctx);
        Assert.Single(totales);
    }

    // Side-fetch de valores de un campo calculado por PK (para mostrar la columna en el ListView tipado).
    [Fact]
    public async Task GetCalcValues_resuelve_duracion_por_clave()
    {
        var duracion = new CalculatedField("Certificacion", "Duracion", "Duración",
            new ScalarBinary(ScalarBinOp.Add,
                new ScalarFunc(ScalarFn.DateDiffDay, new ScalarNode[] { new ScalarColumn("FechaIni"), new ScalarColumn("FechaFin") }),
                new ScalarConst(1)),
            typeof(int), null);

        var page = await Svc<Certificacion>().GetPageAsync(new PageQuery(0, 5));
        var keys = page.Items.Select(c => (object)c.NroLlamado).ToList();
        Assert.NotEmpty(keys);

        var map = await Svc<Certificacion>().GetCalcValuesAsync(keys, new[] { duracion });

        Assert.NotEmpty(map);
        Assert.All(map.Values, v => Assert.True(v.ContainsKey("Duracion")));
    }

    // Filtro de AGREGADO sobre colección: "afiliados con N certificaciones" (Certificaciones.Count()).
    [Fact]
    public async Task FilterAggregate_afiliados_por_cantidad_de_certificaciones()
    {
        var cert = Sgpa.Domain.Metadata.EntityMetadata.For<Certificacion>();

        // Hay afiliados con más de una certificación.
        var masDeUna = new FilterAggregate(cert, "CI", "CI", AggKind.Count, null, null, FilterOp.Greater, 1);
        Assert.True((await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 1, Filter: masDeUna))).TotalCount > 0);

        // Conteo exacto del afiliado conocido: Count >= n → lo encuentra; Count > n → no.
        var n = (await Svc<Certificacion>().GetByColumnAsync("CI", Ci)).Count;
        if (n > 0)
        {
            var alMenosN = new FilterGroup(true, new FilterNode[]
            {
                new FilterCompare("CI", FilterOp.Equal, Ci),
                new FilterAggregate(cert, "CI", "CI", AggKind.Count, null, null, FilterOp.GreaterOrEqual, n),
            });
            Assert.Equal(1, (await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 1, Filter: alMenosN))).TotalCount);

            var masDeN = new FilterGroup(true, new FilterNode[]
            {
                new FilterCompare("CI", FilterOp.Equal, Ci),
                new FilterAggregate(cert, "CI", "CI", AggKind.Count, null, null, FilterOp.Greater, n),
            });
            Assert.Equal(0, (await Svc<Afiliado>().GetPageAsync(new PageQuery(0, 1, Filter: masDeN))).TotalCount);
        }
    }
}
