-- Campos calculados de Certificación que exponen la descripción de los FK (vía subconsulta correlacionada),
-- para poder mostrarlos/ordenarlos/filtrarlos por el nombre y no por el código. Idempotente.
-- Mantener en sintonía con NewSgpa.Migration/Program.cs (CreateInfrastructureTablesAsync).
MERGE dbo.CampoCalculado AS t
USING (VALUES
    (N'Certificacion', N'CertificadorDesc', N'Certificador',     N'[Certificador.Descrip]', N'string'),
    (N'Certificacion', N'AfeccionDesc',     N'Tipo de afección', N'[AfeccionTipo.Descrip]', N'string'),
    (N'Certificacion', N'SalidaDesc',       N'Tipo de salida',   N'[SalidaTipo.Descrip]',   N'string')
) AS s(Tabla, Nombre, Caption, Expr, TipoResultado)
ON t.Tabla = s.Tabla AND t.Nombre = s.Nombre
WHEN NOT MATCHED THEN
    INSERT (Tabla, Nombre, Caption, Expr, TipoResultado, Activo)
    VALUES (s.Tabla, s.Nombre, s.Caption, s.Expr, s.TipoResultado, 1);
