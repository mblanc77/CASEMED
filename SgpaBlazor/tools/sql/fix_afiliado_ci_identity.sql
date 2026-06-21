-- Quita el IDENTITY de Afiliado.CI (la cédula la ingresa el usuario, no es autoincremental).
-- Preserva datos y recrea PK + las 17 FKs con su ON DELETE original.
-- Batches separados por GO (resolución diferida de CI_new / CI renombrada). La transacción la maneja el runner.

ALTER TABLE dbo.[AdPreJub] DROP CONSTRAINT [FK_AdPreJub_Afiliado_CI];
ALTER TABLE dbo.[AdPreJubPago] DROP CONSTRAINT [FK_AdPreJubPago_Afiliado_CI];
ALTER TABLE dbo.[AfiliadoApunte] DROP CONSTRAINT [FK_AfiliadoApunte_Afiliado_CI];
ALTER TABLE dbo.[AfiliadoEspecialidad] DROP CONSTRAINT [FK_AfiliadoEspecialidad_Afiliado_CI];
ALTER TABLE dbo.[Certificacion] DROP CONSTRAINT [FK_Certificacion_Afiliado_CI];
ALTER TABLE dbo.[CertificacionProrroga] DROP CONSTRAINT [FK_CertificacionProrroga_Afiliado_CI];
ALTER TABLE dbo.[Cuenta] DROP CONSTRAINT [FK_Cuenta_Afiliado_CI];
ALTER TABLE dbo.[Imponible] DROP CONSTRAINT [FK_Imponible_Afiliado_CI];
ALTER TABLE dbo.[Prestacion] DROP CONSTRAINT [FK_Prestacion_Afiliado_CI];
ALTER TABLE dbo.[PrimaFallecimiento] DROP CONSTRAINT [FK_PrimaFallecimiento_Afiliado_CI];
ALTER TABLE dbo.[Receta] DROP CONSTRAINT [FK_Receta_Afiliado_CI];
ALTER TABLE dbo.[ReintegroMutual] DROP CONSTRAINT [FK_ReintegroMutual_Afiliado_CI];
ALTER TABLE dbo.[SP_AfiliadoComentario] DROP CONSTRAINT [FK_SP_AfiliadoComentario_Afiliado_CI];
ALTER TABLE dbo.[SP_ImpLiquido] DROP CONSTRAINT [FK_SP_ImpLiquido_Afiliado_CI];
ALTER TABLE dbo.[SubsidioCabezal] DROP CONSTRAINT [FK_SubsidioCabezal_Afiliado_CI];
ALTER TABLE dbo.[SubsidioItemCod_Afiliado] DROP CONSTRAINT [FK_SubsidioItemCod_Afiliado_Afiliado_CI];
ALTER TABLE dbo.[Trabaja] DROP CONSTRAINT [FK_Trabaja_Afiliado_CI];
ALTER TABLE dbo.Afiliado DROP CONSTRAINT PK_Afiliado;
ALTER TABLE dbo.Afiliado ADD CI_new bigint NULL;
GO
UPDATE dbo.Afiliado SET CI_new = CI;
ALTER TABLE dbo.Afiliado DROP COLUMN CI;
EXEC sp_rename 'dbo.Afiliado.CI_new', 'CI', 'COLUMN';
GO
ALTER TABLE dbo.Afiliado ALTER COLUMN CI bigint NOT NULL;
GO
ALTER TABLE dbo.Afiliado ADD CONSTRAINT PK_Afiliado PRIMARY KEY ([CI]);
ALTER TABLE dbo.[AdPreJub] ADD CONSTRAINT [FK_AdPreJub_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[AdPreJubPago] ADD CONSTRAINT [FK_AdPreJubPago_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[AfiliadoApunte] ADD CONSTRAINT [FK_AfiliadoApunte_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[AfiliadoEspecialidad] ADD CONSTRAINT [FK_AfiliadoEspecialidad_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[Certificacion] ADD CONSTRAINT [FK_Certificacion_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]);
ALTER TABLE dbo.[CertificacionProrroga] ADD CONSTRAINT [FK_CertificacionProrroga_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[Cuenta] ADD CONSTRAINT [FK_Cuenta_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]);
ALTER TABLE dbo.[Imponible] ADD CONSTRAINT [FK_Imponible_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[Prestacion] ADD CONSTRAINT [FK_Prestacion_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[PrimaFallecimiento] ADD CONSTRAINT [FK_PrimaFallecimiento_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[Receta] ADD CONSTRAINT [FK_Receta_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[ReintegroMutual] ADD CONSTRAINT [FK_ReintegroMutual_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[SP_AfiliadoComentario] ADD CONSTRAINT [FK_SP_AfiliadoComentario_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[SP_ImpLiquido] ADD CONSTRAINT [FK_SP_ImpLiquido_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
ALTER TABLE dbo.[SubsidioCabezal] ADD CONSTRAINT [FK_SubsidioCabezal_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]);
ALTER TABLE dbo.[SubsidioItemCod_Afiliado] ADD CONSTRAINT [FK_SubsidioItemCod_Afiliado_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]);
ALTER TABLE dbo.[Trabaja] ADD CONSTRAINT [FK_Trabaja_Afiliado_CI] FOREIGN KEY ([CI]) REFERENCES dbo.Afiliado([CI]) ON DELETE CASCADE;
GO
