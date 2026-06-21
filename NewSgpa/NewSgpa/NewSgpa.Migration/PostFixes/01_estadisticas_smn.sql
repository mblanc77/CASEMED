-- POST-FIX (se aplica DESPUÉS de los artefactos generados): overflow 1.25*@pSMN.
-- @pSMN se genera como NVARCHAR(MAX); al multiplicar por el literal numeric(3,2) 1.25, SQL Server intenta
-- convertir el nvarchar a numeric(3,2) y desborda. Se castea @pSMN a float. Afecta los Informes Estadísticos
-- IdRpt 2,3 (802_*) y 20,21 (811_*). Idempotente (ALTER FUNCTION). Sin USE: corre sobre la conexión configurada.
-- ===== DATA OBJECT FOR QUERY: 802_Ult6 =====
ALTER FUNCTION [dbo].[acc_sgpa_802_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>=(1.25*TRY_CONVERT(float,@pSMN))))
)
GO
-- ===== DATA OBJECT FOR QUERY: 802_UltMes =====
ALTER FUNCTION [dbo].[acc_sgpa_802_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))>=(1.25*TRY_CONVERT(float,@pSMN))))
)
GO
-- ===== DATA OBJECT FOR QUERY: 811_Afiliado<125_Pct_Ult6 =====
ALTER FUNCTION [dbo].[acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Importe, (CASE WHEN Avg([I].[Importe]) > 0 And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 10)/100) THEN 'Mayor que 0 hasta 10%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 10)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 20)/100) THEN 'Mayor que 10% hasta 20%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 20)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 30)/100) THEN 'Mayor que 20% hasta 30%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 30)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 40)/100) THEN 'Mayor que 30% hasta 40%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 40)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 50)/100) THEN 'Mayor que 40% hasta 50%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 50)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 60)/100) THEN 'Mayor que 50% hasta 60%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 60)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 70)/100) THEN 'Mayor que 60% hasta 70%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 70)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 80)/100) THEN 'Mayor que 70% hasta 80%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 80)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 90)/100) THEN 'Mayor que 80% hasta 90%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 90)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 100)/100) THEN 'Mayor que 90% hasta 100%' ELSE NULL END) AS Grupo
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
GROUP BY I.CI
HAVING Avg([I].[Importe])>0 And Avg([I].[Importe])<((1.25*TRY_CONVERT(float,@pSMN)))
)
GO
-- ===== DATA OBJECT FOR QUERY: 811_Afiliado<125_Pct_UltMes =====
ALTER FUNCTION [dbo].[acc_sgpa_811_Afiliado_125_Pct_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Importe, (CASE WHEN Avg([I].[Importe]) > 0 And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 10)/100) THEN 'Mayor que 0 hasta 10%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 10)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 20)/100) THEN 'Mayor que 10% hasta 20%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 20)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 30)/100) THEN 'Mayor que 20% hasta 30%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 30)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 40)/100) THEN 'Mayor que 30% hasta 40%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 40)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 50)/100) THEN 'Mayor que 40% hasta 50%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 50)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 60)/100) THEN 'Mayor que 50% hasta 60%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 60)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 70)/100) THEN 'Mayor que 60% hasta 70%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 70)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 80)/100) THEN 'Mayor que 70% hasta 80%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 80)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 90)/100) THEN 'Mayor que 80% hasta 90%' WHEN Avg([I].[Importe]) > ((1.25*TRY_CONVERT(float,@pSMN) * 90)/100) And Avg([I].[Importe]) <= ((1.25*TRY_CONVERT(float,@pSMN) * 100)/100) THEN 'Mayor que 90% hasta 100%' ELSE NULL END) AS Grupo
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING Avg([I].[Importe])>0 And Avg([I].[Importe])<((1.25*TRY_CONVERT(float,@pSMN)))
)
