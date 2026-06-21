-- POST-FIX (se aplica DESPUÉS de los artefactos generados): 805_CertificadosActivos (IdRpt 10
-- "Afiliados activos certificados"). La query Access original NO filtraba por afiliado activo, mientras
-- 805_Activos sí => "Sin certificar" = Activos - Certificados quedaba NEGATIVO (Certificados podía superar
-- a Activos al incluir no activos). Se agrega el mismo criterio de actividad que 805_Activos
-- (CI en Trabaja con FechaBaja NULL). Idempotente (ALTER FUNCTION). Sin USE: corre sobre la conexión configurada.
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
