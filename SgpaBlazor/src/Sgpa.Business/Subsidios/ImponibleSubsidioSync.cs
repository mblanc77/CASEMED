using Sgpa.Data;

namespace Sgpa.Business.Subsidios;

/// <inheritdoc cref="IImponibleSubsidioSync"/>
public sealed class ImponibleSubsidioSync : IImponibleSubsidioSync
{
    // Empresa ficticia "SUBSIDIO POR ENF." (ver SubsidioLiquidacionService.DefaultCodCasemed).
    private const int CodCasemed = 900;
    private const string ConceptoNominal = "1"; // Concepto='1' = importe nominal (el '2' aguinaldo es legacy).
    private const int DiasFijos = 30;            // emp900 siempre se carga con 30 días.

    // Recalcula la(s) fila(s) Imponible emp900 del período en una sola sentencia atómica (MERGE):
    //  - Source: SUM(ImpNominal) por afiliado de los cabezales liquidados del período, anclado al registro
    //    Trabaja emp900 vigente del afiliado (1 por CI: prioriza sin baja y el ingreso más reciente) — su
    //    FechaIngCasemed/IdTrabaja son los que la liquidación futura usa para reencontrar el imponible.
    //  - MATCHED        → actualiza Importe/Días/IdTrabaja/AnioMes.
    //  - NOT MATCHED    → inserta la fila Concepto='1'.
    //  - NOT BY SOURCE  → borra (acotado al período/CI) las filas sin cabezal liquidado que las respalde.
    private const string MergeSql = @"
MERGE dbo.Imponible AS tgt
USING (
    SELECT sc.CI,
           t.FechaIngCasemed AS Fechaingreso,
           t.IdTrabaja,
           CAST(sc.Mes AS tinyint) AS Mes,
           sc.Anio,
           SUM(CAST(ISNULL(sc.ImpNominal, 0) AS float)) AS Importe
    FROM dbo.SubsidioCabezal sc
    INNER JOIN (
        SELECT CI, IdTrabaja, FechaIngCasemed,
               ROW_NUMBER() OVER (PARTITION BY CI
                   ORDER BY CASE WHEN FechaBaja IS NULL THEN 0 ELSE 1 END, FechaIngCasemed DESC) AS rn
        FROM dbo.Trabaja
        WHERE CodEmpresa = @codCasemed
    ) t ON t.CI = sc.CI AND t.rn = 1
    WHERE sc.Liquidar = 1 AND sc.Anio = @anio AND sc.Mes = @mes
      AND (@ci IS NULL OR sc.CI = @ci)
    GROUP BY sc.CI, t.FechaIngCasemed, t.IdTrabaja, sc.Mes, sc.Anio
) AS src
ON  tgt.CI = src.CI
AND tgt.CodEmpresa = @codCasemed
AND tgt.Fechaingreso = src.Fechaingreso
AND tgt.Mes = src.Mes
AND tgt.Anio = src.Anio
AND tgt.Concepto = @concepto
WHEN MATCHED THEN
    UPDATE SET Importe = src.Importe, DiasTrabajados = @dias, IdTrabaja = src.IdTrabaja,
               AnioMes = src.Anio * 100 + src.Mes, Usr = @usr, Ts = SYSDATETIME()
WHEN NOT MATCHED BY TARGET THEN
    INSERT (CI, CodEmpresa, Fechaingreso, Mes, Anio, Concepto, IdTrabaja, DiasTrabajados, Importe, AnioMes, Usr, Ts)
    VALUES (src.CI, @codCasemed, src.Fechaingreso, src.Mes, src.Anio, @concepto, src.IdTrabaja, @dias,
            src.Importe, src.Anio * 100 + src.Mes, @usr, SYSDATETIME())
WHEN NOT MATCHED BY SOURCE
     AND tgt.CodEmpresa = @codCasemed AND tgt.Concepto = @concepto
     AND tgt.Anio = @anio AND tgt.Mes = @mes
     AND (@ci IS NULL OR tgt.CI = @ci) THEN
    DELETE;";

    // Camino rápido para UN afiliado (handler de alta/baja/edición del cabezal): todo con seeks del índice
    // clusterizado (Imponible por CI, Trabaja por CI+CodEmpresa, SubsidioCabezal por IX_CI), sin la ventana
    // sobre las ~10k filas Trabaja900 ni el NOT MATCHED BY SOURCE sobre los ~1,9M de Imponible que hacía el
    // MERGE genérico (medido: ~2 s → ~ms). Borra la fila si no quedan cabezales liquidados; si no, upsert.
    private const string UpsertCiSql = @"
DECLARE @importe float, @hasCab bit;
SELECT @importe = SUM(CAST(ISNULL(ImpNominal, 0) AS float)),
       @hasCab  = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
FROM dbo.SubsidioCabezal WHERE CI = @ci AND Anio = @anio AND Mes = @mes AND Liquidar = 1;

IF @hasCab = 0
    DELETE FROM dbo.Imponible
    WHERE CI = @ci AND CodEmpresa = @codCasemed AND Concepto = @concepto AND Anio = @anio AND Mes = @mes;
ELSE
BEGIN
    DECLARE @fIng datetime, @idTrab int;
    SELECT TOP 1 @idTrab = IdTrabaja, @fIng = FechaIngCasemed
    FROM dbo.Trabaja WHERE CI = @ci AND CodEmpresa = @codCasemed
    ORDER BY CASE WHEN FechaBaja IS NULL THEN 0 ELSE 1 END, FechaIngCasemed DESC;

    IF @idTrab IS NOT NULL
    BEGIN
        UPDATE dbo.Imponible
        SET Importe = @importe, DiasTrabajados = @dias, IdTrabaja = @idTrab,
            AnioMes = @anio * 100 + @mes, Usr = @usr, Ts = SYSDATETIME()
        WHERE CI = @ci AND CodEmpresa = @codCasemed AND Fechaingreso = @fIng
          AND Mes = @mes AND Anio = @anio AND Concepto = @concepto;
        IF @@ROWCOUNT = 0
            INSERT INTO dbo.Imponible (CI, CodEmpresa, Fechaingreso, Mes, Anio, Concepto, IdTrabaja, DiasTrabajados, Importe, AnioMes, Usr, Ts)
            VALUES (@ci, @codCasemed, @fIng, @mes, @anio, @concepto, @idTrab, @dias, @importe, @anio * 100 + @mes, @usr, SYSDATETIME());
    END
END";

    public Task SincronizarPeriodoAsync(IDbExecutor db, int anio, int mes, long? ci, string usr,
        CancellationToken cancellationToken = default)
    {
        var p = new { anio, mes, ci, usr, codCasemed = CodCasemed, concepto = ConceptoNominal, dias = DiasFijos };
        // Un CI → vía rápida por seeks; período completo (ci=null, liquidación) → MERGE set-based.
        var sql = ci.HasValue ? UpsertCiSql : MergeSql;
        return db.ExecuteAsync(sql, p, cancellationToken: cancellationToken);
    }
}
