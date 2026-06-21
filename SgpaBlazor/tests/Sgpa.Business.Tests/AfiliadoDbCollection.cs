using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Colección que serializa las pruebas que cuentan o mutan dbo.Afiliado (PagingTests asume un total
/// canónico; AfiliadoAltaTests inserta/borra una fila). Sin esto correrían en paralelo y el conteo fallaría.
/// </summary>
[CollectionDefinition("AfiliadoDb")]
public sealed class AfiliadoDbCollection { }
