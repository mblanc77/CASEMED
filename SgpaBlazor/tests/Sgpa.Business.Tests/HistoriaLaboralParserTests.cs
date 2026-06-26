using System;
using System.Collections.Generic;
using System.Linq;
using Sgpa.Business.Imponibles;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Tests del parser ATYR V2 (puros, sin base). Cubren el mapeo de campos por tipo de registro, el merge de
/// una declaración spliteada en varios archivos y las validaciones (solapamiento de tipos, período del cabezal).
/// </summary>
public class HistoriaLaboralParserTests
{
    private static readonly HistoriaLaboralParser Parser = new();

    private const string Empresa = "1|N|3.1|MiApp|123456789012|987654321098|11|RAZON SOCIAL SA|18 DE JULIO 1234|24001234";
    private const string Cabezal = "4|032024|3|150000.50|1|1234";
    private const string Persona = "5|1|DO|12345678|PEREZ|GOMEZ|JUAN|CARLOS|31121980|1|1";
    private const string Actividad = "6||1|DO|12345678|1|01032020|10|44|100|0|0|0|0|S|30|0|110|0|";
    private const string RemSueldo = "7||1|DO|12345678|1|1|45000.00||";
    private const string RemAguinaldo = "7||1|DO|12345678|1|2|3750.00||";

    private static AtyrArchivo File(string name, params string[] lineas) => new(name, lineas);

    [Fact]
    public void Parsea_archivo_unico_con_todos_los_tipos()
    {
        var p = Parser.Parse(new[] { File("N_1.bps", Empresa, Cabezal, Persona, Actividad, RemSueldo, RemAguinaldo) });

        Assert.Equal(3, p.Mes);
        Assert.Equal(2024, p.Anio);
        Assert.Single(p.Empresas);
        Assert.Single(p.Cabezales);
        Assert.Single(p.Personas);
        Assert.Single(p.Actividades);
        Assert.Equal(2, p.Remuneraciones.Count);

        var per = p.Personas[0];
        Assert.Equal("DO", per.TipoDocumento);
        Assert.Equal(12345678L, per.CI);
        Assert.Equal("PEREZ", per.PrimerApellido);
        Assert.Equal("GOMEZ", per.SegundoApellido);
        Assert.Equal("JUAN", per.PrimerNombre);
        Assert.Equal(new DateTime(1980, 12, 31), per.FechaNacimiento);
        Assert.Equal(1, per.Sexo);
        Assert.Equal(1, per.Nacionalidad);

        var rem = p.Remuneraciones[0];
        Assert.Equal(12345678L, rem.CI);
        Assert.Equal(1, rem.AcumulacionLaboral);
        Assert.Equal(1, rem.Concepto);
        Assert.Equal(45000.00, rem.Remuneracion);
        Assert.Null(rem.Jornal);

        var act = p.Actividades[0];
        Assert.Equal(new DateTime(2020, 3, 1), act.FechaIngreso);
        Assert.Equal(30, act.DiasTrabajados);
        Assert.Equal("S", act.AsignacionFamiliar);
    }

    [Fact]
    public void Mergea_declaracion_spliteada_por_tipo_de_registro()
    {
        // Archivo A: cabecera + personas. Archivo B: actividad + remuneración. Sin solapamiento de tipos.
        var a = File("N_1a.bps", Empresa, Cabezal, Persona);
        var b = File("N_1b.bps", Actividad, RemSueldo, RemAguinaldo);

        var p = Parser.Parse(new[] { a, b });

        Assert.Equal(3, p.Mes);
        Assert.Single(p.Personas);
        Assert.Single(p.Actividades);
        Assert.Equal(2, p.Remuneraciones.Count);
    }

    [Fact]
    public void Falla_si_un_tipo_de_datos_aparece_en_mas_de_un_archivo()
    {
        var a = File("N_1a.bps", Empresa, Cabezal, RemSueldo);
        var b = File("N_1b.bps", RemAguinaldo);   // tipo 7 también acá → solapamiento

        var ex = Assert.Throws<InvalidOperationException>(() => Parser.Parse(new[] { a, b }));
        Assert.Contains("tipo 7", ex.Message);
    }

    [Fact]
    public void Falla_si_los_cabezales_tienen_periodos_distintos()
    {
        var a = File("N_1a.bps", Empresa, "4|032024|3|100|1|1", Persona);
        var b = File("N_1b.bps", "4|042024|3|100|1|1", RemSueldo);

        Assert.Throws<InvalidOperationException>(() => Parser.Parse(new[] { a, b }));
    }

    [Fact]
    public void Falla_si_no_hay_cabezal()
    {
        var a = File("N_1.bps", Empresa, Persona, RemSueldo);
        var ex = Assert.Throws<InvalidOperationException>(() => Parser.Parse(new[] { a }));
        Assert.Contains("cabezal", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Falla_si_los_archivos_son_de_empresas_distintas()
    {
        var a = File("N_1a.bps", "1|N|3.1|App|111111111111|1|11|EMPRESA A|x|1", Cabezal, Persona);
        var b = File("N_1b.bps", "1|N|3.1|App|222222222222|2|11|EMPRESA B|y|2", RemSueldo);

        Assert.Throws<InvalidOperationException>(() => Parser.Parse(new[] { a, b }));
    }

    [Fact]
    public void Lista_vacia_de_archivos_falla()
        => Assert.Throws<InvalidOperationException>(() => Parser.Parse(Array.Empty<AtyrArchivo>()));

    [Theory]
    [InlineData("032024", 3, 2024)]
    [InlineData("122030", 12, 2030)]
    public void ParseMesCargo_ok(string mc, int mes, int anio)
    {
        var (m, a) = HistoriaLaboralParser.ParseMesCargo(mc);
        Assert.Equal(mes, m);
        Assert.Equal(anio, a);
    }

    [Theory]
    [InlineData("13" + "2024")]   // mes 13 inválido
    [InlineData("2024")]          // largo incorrecto
    [InlineData("")]
    public void ParseMesCargo_invalido_falla(string mc)
        => Assert.Throws<InvalidOperationException>(() => HistoriaLaboralParser.ParseMesCargo(mc));

    [Fact]
    public void Documento_no_numerico_deja_CI_nula()
    {
        var p = Parser.Parse(new[] { File("N.bps", Empresa, Cabezal, "5|1|PA|AB123456|SMITH||JOHN||01011990|1|3") });
        Assert.Null(p.Personas[0].CI);
        Assert.Equal("AB123456", p.Personas[0].NroDocumento);
    }
}
