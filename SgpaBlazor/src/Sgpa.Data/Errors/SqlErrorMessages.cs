using Microsoft.Data.SqlClient;

namespace Sgpa.Data.Errors;

/// <summary>Traduce excepciones de base de datos a mensajes amigables en español para mostrar al usuario.</summary>
public static class SqlErrorMessages
{
    /// <summary>Mensaje amigable para el usuario ante una excepción de guardado/borrado (el detalle se loguea aparte).</summary>
    public static string Amigable(Exception ex)
    {
        var sql = Buscar<SqlException>(ex);
        return sql is not null ? PorNumero(sql.Number) : "No se pudo completar la operación.";
    }

    /// <summary>Mensaje amigable según el número de error de SQL Server (separado para poder testearlo).</summary>
    public static string PorNumero(int sqlNumber) => sqlNumber switch
    {
        2627 or 2601 => "Ya existe un registro con esa clave (valor duplicado).",
        547 => "No se puede completar: el registro está relacionado con otros datos, o una referencia no existe.",
        515 => "Falta completar un campo obligatorio.",
        2628 or 8152 => "Un valor es demasiado largo para el campo.",
        245 => "Un valor tiene un formato inválido.",
        _ => "No se pudo completar la operación en la base de datos."
    };

    private static T? Buscar<T>(Exception? ex) where T : Exception
    {
        for (; ex is not null; ex = ex.InnerException)
            if (ex is T t) return t;
        return null;
    }
}
