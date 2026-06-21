-- Filtros de sistema precargados por entidad (EsSistema=1), estilo VB6 'Seleccion'.
-- Idempotente. Criteria = sintaxis DevExpress CriteriaOperator. Re-aplicar tras regen total.
SET NOCOUNT ON;
DECLARE @f TABLE (Entity nvarchar(128), Nombre nvarchar(128), Criteria nvarchar(max));
INSERT INTO @f VALUES
 ('Afiliado','Sin fecha de nacimiento','IsNull([FechaNacimiento])'),
 ('Afiliado','Paga mutualista','[PagaMutualista] = True'),
 ('Afiliado','Afiliado por cédula (demo)','[CI] = 13010559'),
 ('Empresa','Liquida','[Liquidar] = True'),
 ('Empresa','No liquida','[Liquidar] = False'),
 ('Empresa','Ficticias','[Ficticia] = True'),
 ('Certificacion','Efectivas','[Efectiva] = True'),
 ('Certificacion','No efectivas','[Efectiva] = False'),
 ('Prestacion','Con boleta','[Boleta] = True'),
 ('SP_Prestamo','Sin estado','IsNullOrEmpty([CodPrestamoEstado])');

MERGE dbo.SgpaFiltro AS t
USING @f AS s ON t.Entity = s.Entity AND t.Nombre = s.Nombre AND t.EsSistema = 1
WHEN NOT MATCHED THEN
  INSERT (Entity, Nombre, Criteria, EsSistema, Usr, Ts)
  VALUES (s.Entity, s.Nombre, s.Criteria, 1, 'sistema', SYSDATETIME());
