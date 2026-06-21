-- Fix 805_CertificadosActivos (Informe Estadístico "Afiliados activos certificados", IdRpt 10).
-- La función migrada contaba afiliados certificados SIN filtrar por afiliado activo, mientras 805_Activos
-- sí filtra activos => "Sin certificar" = Activos - Certificados quedaba NEGATIVO (Certificados podía superar
-- a Activos al incluir afiliados no activos). Se agrega el mismo criterio de actividad que 805_Activos
-- (CI presente en Trabaja con FechaBaja NULL). Idempotente (ALTER FUNCTION). Aplicado a NewSgpa2.
USE NewSgpa2;
GO
ALTER FUNCTION [dbo].[acc_sgpa_805_CertificadosActivos_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE Afiliado.CI In (Select CI From Trabaja Where FechaBaja Is Null) AND (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END)
)))
)
