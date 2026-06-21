using Xunit;

namespace Sgpa.Business.Tests;

/// <summary>
/// Colección que serializa los tests que insertan en SP_Prestamo (el próximo IDPrestamo es MAX+1, así
/// que correrlos en paralelo colisiona en la PK). xUnit no paraleliza clases de una misma colección.
/// </summary>
[CollectionDefinition("PrestamosDb", DisableParallelization = true)]
public sealed class PrestamosDbCollection { }
