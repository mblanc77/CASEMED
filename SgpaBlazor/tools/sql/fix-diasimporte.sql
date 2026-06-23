-- FIX ValorJornal: el promedio por empresa se divide por los DÍAS REALMENTE TRABAJADOS de los meses
-- con aporte (Sum(DiasTrabajados)), NO por la ventana completa (meses × 30).
--
-- Es el port exacto de la query Access de producción 300_AfiliadoDiasImporte:
--     Iif(Dias > 0, Importe / Dias, 0)   con Dias = Sum(DiasTrabajados), Importe = Sum(Importe)
--
-- El divisor "ventana × 30" (= 180 para la ventana de 6 meses) sólo corresponde a la query CASEMED
-- (300_AfiliadoValorJornalCasemed = Sum(Importe/180)); el migrador lo generalizó por error a TODAS las
-- empresas. Eso daba el resultado correcto sólo cuando el afiliado tenía aportes en los 6 meses (Dias=180),
-- pero la MITAD del jornal cuando computaban menos meses (p. ej. 3 meses → Dias=90 → dividía por 180).
-- Verificado contra la liquidación VB6 05/2026 (CI 29119448: 3 meses, jornal 2867.558 = 258080.25/90).
-- La query Casemed sigue usando Importe/180 sobre la columna Importe (no Promedio), así que no se afecta.
ALTER FUNCTION [dbo].[acc_sgpa_300_AfiliadoDiasImporte_q]
    (@pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
    SELECT Imponible.CI,
           Sum(Imponible.DiasTrabajados) AS Dias,
           Imponible.CodEmpresa,
           Sum(Imponible.Importe) AS Importe,
           CASE WHEN Sum(Imponible.DiasTrabajados) > 0
                THEN CAST(Sum(Imponible.Importe) AS float) / Sum(Imponible.DiasTrabajados)
                ELSE 0 END AS Promedio
    FROM ((Imponible
          INNER JOIN [acc_sgpa_300_TrabajaActivo_q](@pMes) AS [300_TrabajaActivo]
              ON (Imponible.CI = [300_TrabajaActivo].CI)
             AND (Imponible.CodEmpresa = [300_TrabajaActivo].CodEmpresa)
             AND (Imponible.Fechaingreso = [300_TrabajaActivo].FechaIngreso))
          INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa)
          INNER JOIN [acc_sgpa_300_AfiliadoAporteOk_q](@pMesIniImp, @pMesFin) AS [300_AfiliadoAporteOk]
              ON ([300_TrabajaActivo].FechaIngreso = [300_AfiliadoAporteOk].Fechaingreso)
             AND ([300_TrabajaActivo].CodEmpresa = [300_AfiliadoAporteOk].CodEmpresa)
             AND ([300_TrabajaActivo].CI = [300_AfiliadoAporteOk].CI)
             AND (Empresa.CodEmpresa = [300_AfiliadoAporteOk].CodEmpresa)
    WHERE (((Imponible.Concepto) = '1')
       AND ((TRY_CONVERT(float, CONVERT(nvarchar(max), [Imponible].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2))) Between @pMesIni And @pMesFin)
       AND ((Imponible.CI) = @pCI)
       AND ((Empresa.Liquidar) = @pLiquidar)
       AND (([300_AfiliadoAporteOk].Cantidad) >= @pDias))
    GROUP BY Imponible.CI, Imponible.CodEmpresa
);
