using Sgpa.Data.Errors;
using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>Mensajes amigables por número de error de SQL Server (port del manejo de errores del CRUD).</summary>
public class SqlErrorMessagesTests
{
    [Theory]
    [InlineData(2627, "duplicado")]   // clave primaria/única
    [InlineData(2601, "duplicado")]   // índice único
    [InlineData(547, "relacionado")]  // FK
    [InlineData(515, "obligatorio")]  // NOT NULL
    [InlineData(8152, "largo")]       // truncamiento
    [InlineData(245, "formato")]      // conversión
    [InlineData(99999, "base de datos")] // desconocido → genérico
    public void PorNumero_da_un_mensaje_acorde(int numero, string fragmento)
        => Assert.Contains(fragmento, SqlErrorMessages.PorNumero(numero));

    [Fact]
    public void Amigable_de_una_excepcion_no_sql_es_generica()
        => Assert.Equal("No se pudo completar la operación.", SqlErrorMessages.Amigable(new System.InvalidOperationException("x")));
}
