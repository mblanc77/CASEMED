-- FIX ValorJornal: el promedio por empresa debe dividirse por los días de la VENTANA COMPLETA
-- (meses de la ventana × 30), no por Sum(DiasTrabajados) de los meses con registros.
-- Coincide con la regla del negocio y con la query Casemed (Sum(Importe/180)).
-- El bug original (heredado de la query Access 300_AfiliadoDiasImporte) sólo daba el resultado
-- correcto cuando existían registros de imponible (incluso importe 0) para todos los meses.
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
           Sum(Imponible.Importe)
             / (((@pMesFin / 100 - @pMesIni / 100) * 12 + (@pMesFin % 100 - @pMesIni % 100) + 1) * 30.0) AS Promedio
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
