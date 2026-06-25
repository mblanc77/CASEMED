-- Acceso de la app bajo IIS a NewSgpa3: la identidad del Application Pool "NewSgpa3"
-- (cuenta virtual IIS APPPOOL\NewSgpa3) necesita login + usuario + acceso total a la base.
-- Idempotente. Nota: el USER vive DENTRO de la base; si se vuelve a correr la migración
-- (drop+create de NewSgpa3) hay que re-ejecutar este script (el LOGIN a nivel servidor persiste).

-- 1) Login a nivel servidor (cuenta virtual de Windows del app pool).
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = N'IIS APPPOOL\NewSgpa3')
    CREATE LOGIN [IIS APPPOOL\NewSgpa3] FROM WINDOWS;
GO

USE [NewSgpa3];
GO

-- 2) Usuario en la base, mapeado al login.
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'IIS APPPOOL\NewSgpa3')
    CREATE USER [IIS APPPOOL\NewSgpa3] FOR LOGIN [IIS APPPOOL\NewSgpa3];
GO

-- 3) Acceso total a la base.
ALTER ROLE db_owner ADD MEMBER [IIS APPPOOL\NewSgpa3];
GO
