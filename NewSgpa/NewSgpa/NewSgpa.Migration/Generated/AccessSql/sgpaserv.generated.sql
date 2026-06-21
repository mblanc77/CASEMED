-- Auto-generated from sgpaserv.mdb-specs.txt
-- Query count: 7
-- Ordered by dependencies (nested queries first)

-- ===== DATA OBJECT FOR QUERY: Consulta5 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpaserv_Consulta5_q]
AS
SELECT Imponible.CodEmpresa, Imponible.Concepto, Sum(Imponible.Importe) AS SumaDeImporte
FROM Imponible
WHERE (((Imponible.Mes)=1) AND ((Imponible.Anio)=2023))
GROUP BY Imponible.CodEmpresa, Imponible.Concepto;
GO

-- ===== DATA OBJECT FOR QUERY: qCerti05 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpaserv_qCerti05_q]
AS
SELECT SubsidioCabezal.IdSubsidio, SubsidioCabezal.CI, SubsidioEnfermedad.FechaIni, SubsidioEnfermedad.FechaFin, SubsidioEnfermedad.FechaIniSubsidio, SubsidioEnfermedad.FechaFinSubsidio
FROM SubsidioCabezal INNER JOIN SubsidioEnfermedad ON SubsidioCabezal.IdSubsidio = SubsidioEnfermedad.IdSubsidio
WHERE (((SubsidioCabezal.Mes)=5) AND ((SubsidioCabezal.Anio)=2020));
GO

-- ===== DATA OBJECT FOR QUERY: qryNoEn2 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpaserv_qryNoEn2_q]
AS
SELECT xLiq1.*
FROM xLiq2 RIGHT JOIN xLiq1 ON xLiq2.CI = xLiq1.CI
WHERE (((xLiq2.CI) Is Null));
GO

-- ===== DATA OBJECT FOR QUERY: qry_rect_tot =====
CREATE OR ALTER VIEW [dbo].[acc_sgpaserv_qry_rect_tot_q]
AS
SELECT tmp_Rectificativas.EMPRESA, tmp_Rectificativas.CI, tmp_Rectificativas.Concepto, Sum(tmp_Rectificativas.Importe) AS SumaDeImporte
FROM tmp_Rectificativas
GROUP BY tmp_Rectificativas.EMPRESA, tmp_Rectificativas.CI, tmp_Rectificativas.Concepto;
GO

-- ===== DATA OBJECT FOR QUERY: tmp_qry_rect_emp =====
CREATE OR ALTER VIEW [dbo].[acc_sgpaserv_tmp_qry_rect_emp_q]
AS
SELECT tmp_Rectificativas.EMPRESA
FROM tmp_Rectificativas
GROUP BY tmp_Rectificativas.EMPRESA;
GO

-- ===== DATA OBJECT FOR QUERY: tmp_ult_empleo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpaserv_tmp_ult_empleo_q]
AS
SELECT *
FROM Trabaja AS t1
WHERE NOT exists (select * from Trabaja as t2 where t1.CI=t2.CI and t1.CodEmpresa=t2.CodEmpresa and t1.FechaIngreso < t2.FechaIngreso);
GO

-- ===== COMPAT OBJECT FOR QUERY: Consulta5 =====
IF OBJECT_ID('dbo.Consulta5') IS NULL EXEC('CREATE VIEW [dbo].[Consulta5] AS SELECT * FROM [dbo].[acc_sgpaserv_Consulta5_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: qCerti05 =====
IF OBJECT_ID('dbo.qCerti05') IS NULL EXEC('CREATE VIEW [dbo].[qCerti05] AS SELECT * FROM [dbo].[acc_sgpaserv_qCerti05_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: qryNoEn2 =====
IF OBJECT_ID('dbo.qryNoEn2') IS NULL EXEC('CREATE VIEW [dbo].[qryNoEn2] AS SELECT * FROM [dbo].[acc_sgpaserv_qryNoEn2_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: qry_rect_tot =====
IF OBJECT_ID('dbo.qry_rect_tot') IS NULL EXEC('CREATE VIEW [dbo].[qry_rect_tot] AS SELECT * FROM [dbo].[acc_sgpaserv_qry_rect_tot_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: tmp_qry_rect_emp =====
IF OBJECT_ID('dbo.tmp_qry_rect_emp') IS NULL EXEC('CREATE VIEW [dbo].[tmp_qry_rect_emp] AS SELECT * FROM [dbo].[acc_sgpaserv_tmp_qry_rect_emp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: tmp_ult_empleo =====
IF OBJECT_ID('dbo.tmp_ult_empleo') IS NULL EXEC('CREATE VIEW [dbo].[tmp_ult_empleo] AS SELECT * FROM [dbo].[acc_sgpaserv_tmp_ult_empleo_q];')
GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Consulta1 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpaserv_Consulta1]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SubsidioEnfermedad SET SubsidioEnfermedad.FechaFin = [Certificacion].[FechaFin] FROM tExcel INNER JOIN (SubsidioCabezal INNER JOIN (SubsidioEnfermedad INNER JOIN Certificacion ON SubsidioEnfermedad.FechaIni = Certificacion.FechaIni) ON (SubsidioCabezal.IdSubsidio = SubsidioEnfermedad.IdSubsidio) AND (SubsidioCabezal.CI = Certificacion.CI)) ON tExcel.CI = Certificacion.CI
    WHERE (((SubsidioEnfermedad.FechaFin)<>[Certificacion].[FechaFin]) AND ((SubsidioCabezal.Anio)=2020) AND ((SubsidioCabezal.Mes)=7));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Consulta5 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpaserv_Consulta5]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CodEmpresa, Imponible.Concepto, Sum(Imponible.Importe) AS SumaDeImporte
    FROM Imponible
    WHERE (((Imponible.Mes)=1) AND ((Imponible.Anio)=2023))
    GROUP BY Imponible.CodEmpresa, Imponible.Concepto;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: qCerti05 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpaserv_qCerti05]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.IdSubsidio, SubsidioCabezal.CI, SubsidioEnfermedad.FechaIni, SubsidioEnfermedad.FechaFin, SubsidioEnfermedad.FechaIniSubsidio, SubsidioEnfermedad.FechaFinSubsidio
    FROM SubsidioCabezal INNER JOIN SubsidioEnfermedad ON SubsidioCabezal.IdSubsidio = SubsidioEnfermedad.IdSubsidio
    WHERE (((SubsidioCabezal.Mes)=5) AND ((SubsidioCabezal.Anio)=2020));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: qryNoEn2 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpaserv_qryNoEn2]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT xLiq1.*
    FROM xLiq2 RIGHT JOIN xLiq1 ON xLiq2.CI = xLiq1.CI
    WHERE (((xLiq2.CI) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: qry_rect_tot =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpaserv_qry_rect_tot]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT tmp_Rectificativas.EMPRESA, tmp_Rectificativas.CI, tmp_Rectificativas.Concepto, Sum(tmp_Rectificativas.Importe) AS SumaDeImporte
    FROM tmp_Rectificativas
    GROUP BY tmp_Rectificativas.EMPRESA, tmp_Rectificativas.CI, tmp_Rectificativas.Concepto;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: tmp_qry_rect_emp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpaserv_tmp_qry_rect_emp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT tmp_Rectificativas.EMPRESA
    FROM tmp_Rectificativas
    GROUP BY tmp_Rectificativas.EMPRESA;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: tmp_ult_empleo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpaserv_tmp_ult_empleo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Trabaja AS t1
    WHERE NOT exists (select * from Trabaja as t2 where t1.CI=t2.CI and t1.CodEmpresa=t2.CodEmpresa and t1.FechaIngreso < t2.FechaIngreso);
END;

GO

