-- Extiende dbo.SP_Prestamo (NewSgpa2) con las columnas que la migración recortó respecto del
-- SP.mdb original (ver Migration/Access/sp.mdb-specs.txt). El workbench de préstamos (port de
-- frmPrestamo.frm) las necesita. Idempotente: re-ejecutable sin error.
-- Tipos: Single→real, Long/Integer→int, Text→nvarchar, Date→datetime2 (convención NewSgpa2).

IF COL_LENGTH('dbo.SP_Prestamo','NroCta')         IS NULL ALTER TABLE dbo.SP_Prestamo ADD NroCta nvarchar(50) NULL;
IF COL_LENGTH('dbo.SP_Prestamo','Banco')          IS NULL ALTER TABLE dbo.SP_Prestamo ADD Banco nvarchar(50) NULL;
IF COL_LENGTH('dbo.SP_Prestamo','Sucursal')       IS NULL ALTER TABLE dbo.SP_Prestamo ADD Sucursal nvarchar(50) NULL;
IF COL_LENGTH('dbo.SP_Prestamo','Tasa')           IS NULL ALTER TABLE dbo.SP_Prestamo ADD Tasa real NULL;
IF COL_LENGTH('dbo.SP_Prestamo','CuotasPagas')    IS NULL ALTER TABLE dbo.SP_Prestamo ADD CuotasPagas int NULL;
IF COL_LENGTH('dbo.SP_Prestamo','Saldo')          IS NULL ALTER TABLE dbo.SP_Prestamo ADD Saldo real NULL;
IF COL_LENGTH('dbo.SP_Prestamo','FechaCobro')     IS NULL ALTER TABLE dbo.SP_Prestamo ADD FechaCobro datetime2 NULL;
IF COL_LENGTH('dbo.SP_Prestamo','NroSerieCheque') IS NULL ALTER TABLE dbo.SP_Prestamo ADD NroSerieCheque int NULL;
IF COL_LENGTH('dbo.SP_Prestamo','NroCheque')      IS NULL ALTER TABLE dbo.SP_Prestamo ADD NroCheque int NULL;
IF COL_LENGTH('dbo.SP_Prestamo','TasaCambio')     IS NULL ALTER TABLE dbo.SP_Prestamo ADD TasaCambio real NULL;
IF COL_LENGTH('dbo.SP_Prestamo','Promedio')       IS NULL ALTER TABLE dbo.SP_Prestamo ADD Promedio real NULL;
IF COL_LENGTH('dbo.SP_Prestamo','IDPrestamoRef')  IS NULL ALTER TABLE dbo.SP_Prestamo ADD IDPrestamoRef int NULL;
IF COL_LENGTH('dbo.SP_Prestamo','CodPrestamoTipo') IS NULL ALTER TABLE dbo.SP_Prestamo ADD CodPrestamoTipo nvarchar(3) NULL;
