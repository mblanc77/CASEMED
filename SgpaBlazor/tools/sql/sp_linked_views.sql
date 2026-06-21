-- Tablas SP_* que en la app SP eran LINKEADAS a la base SGPA (sgpaserv) y al consolidar quedaron VACÍAS.
-- Se reemplazan por VISTAS sobre sus tablas SGPA para restaurar la paridad sin tocar las queries migradas.
-- Todas sin FKs entrantes/salientes (drop seguro). Reemplaza a sp_trabaja_view.sql.

IF OBJECT_ID('dbo.SP_Trabaja', 'U') IS NOT NULL DROP TABLE dbo.SP_Trabaja;
IF OBJECT_ID('dbo.SP_Trabaja', 'V') IS NOT NULL DROP VIEW dbo.SP_Trabaja;
GO
CREATE VIEW dbo.SP_Trabaja AS
SELECT CI, CodEmpresa, FechaIngreso, FechaBaja, CodBajaMotivo, NroFichaEmpresa, IdTrabaja, FechaIngCasemed, Usr, Ts
FROM dbo.Trabaja;
GO

IF OBJECT_ID('dbo.SP_Afiliado', 'U') IS NOT NULL DROP TABLE dbo.SP_Afiliado;
IF OBJECT_ID('dbo.SP_Afiliado', 'V') IS NOT NULL DROP VIEW dbo.SP_Afiliado;
GO
CREATE VIEW dbo.SP_Afiliado AS
SELECT CI, Nombres, Apellido1, Apellido2, FechaNacimiento, Sexo, Direccion, Telefono, EMail, CodMutualista,
       FechaIngMutualista, FechaBajaMutualista, NroSocioMutualista, CodRegimenJubilatorio, CodDepartamento,
       PagaMutualista, CodSituacionMutual, CodBanco, NroCuenta, NroFunCuenta, Movil, Usr, Ts
FROM dbo.Afiliado;
GO

IF OBJECT_ID('dbo.SP_Empresa', 'U') IS NOT NULL DROP TABLE dbo.SP_Empresa;
IF OBJECT_ID('dbo.SP_Empresa', 'V') IS NOT NULL DROP VIEW dbo.SP_Empresa;
GO
CREATE VIEW dbo.SP_Empresa AS
SELECT CodEmpresa, Nombre, Direccion, Telefono, Fax, EMail, AporteCasemed, AporteAguinaldo, PersonaContacto,
       Autoridades, CodRegimenAporte, CodSituacionPago, Liquidar, Ficticia, Usr, Ts
FROM dbo.Empresa;
GO
