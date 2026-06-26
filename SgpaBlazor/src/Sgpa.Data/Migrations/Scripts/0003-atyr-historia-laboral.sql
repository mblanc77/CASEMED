-- Carga de Historia Laboral (BPS) — landing del archivo de nómina ATYR formato V2 (versión 3.x).
-- Port de frmCarHL (AtyroV2_2_Bps + GrabarImponibles), pero acotado al formato V2 y guardando el
-- contenido del archivo en tablas dedicadas Atyr_* (una por tipo de registro de la nómina): 1=Empresa,
-- 4=Cabezal, 5=Persona, 6=Actividad, 7=Remuneración. La carga se identifica por (CodEmpresa, Anio, Mes):
-- la empresa la elige el operador y el período sale del cabezal (tipo 4). Recargar la misma empresa/mes
-- sobrescribe (DELETE por ese scope + INSERT). La derivación a dbo.Imponible la hace el servicio.
-- Run-once (DbUp, journalizado en dbo.SchemaVersions). Guards IF OBJECT_ID → re-aplicable sin daño.

-- REGISTRO TIPO 1 — EMPRESA (datos del contribuyente declarante). Cabecera de la declaración.
IF OBJECT_ID('dbo.Atyr_Empresa', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Atyr_Empresa
    (
        Id               int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Atyr_Empresa PRIMARY KEY,
        CodEmpresa       int           NOT NULL,
        Anio             int           NOT NULL,
        Mes              int           NOT NULL,
        TipoDeclaracion  nvarchar(1)   NULL,   -- N (nómina) | R (rectificativa) | D (deducción)
        Version          nvarchar(10)  NULL,
        Aplicacion       nvarchar(20)  NULL,
        NroEmpresa       nvarchar(14)  NULL,   -- N° empresa BPS (no es el CodEmpresa interno)
        NroContribuyente nvarchar(14)  NULL,
        TipoAportacion   int           NULL,
        Denominacion     nvarchar(40)  NULL,
        Domicilio        nvarchar(80)  NULL,
        Telefono         nvarchar(15)  NULL,
        Usr              nvarchar(50)  NULL,
        Ts               datetime2(0)  NULL
    );
    CREATE INDEX IX_Atyr_Empresa_Scope ON dbo.Atyr_Empresa (CodEmpresa, Anio, Mes);
END
GO

-- REGISTRO TIPO 4 — CABEZAL DE NÓMINA (período y totales de la declaración).
IF OBJECT_ID('dbo.Atyr_Cabezal', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Atyr_Cabezal
    (
        Id                   int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Atyr_Cabezal PRIMARY KEY,
        CodEmpresa           int          NOT NULL,
        Anio                 int          NOT NULL,
        Mes                  int          NOT NULL,
        MesCargo             nvarchar(6)  NULL,   -- mmaaaa, tal cual viene en el archivo
        TipoContribuyente    int          NULL,
        Monto                float        NULL,   -- monto total de la nómina
        FormaRealizacionObra int          NULL,
        ActividadPrincipal   int          NULL,
        Usr                  nvarchar(50) NULL,
        Ts                   datetime2(0) NULL
    );
    CREATE INDEX IX_Atyr_Cabezal_Scope ON dbo.Atyr_Cabezal (CodEmpresa, Anio, Mes);
END
GO

-- REGISTRO TIPO 5 — PERSONAS (identificación de cada persona declarada).
IF OBJECT_ID('dbo.Atyr_Persona', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Atyr_Persona
    (
        Id              int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Atyr_Persona PRIMARY KEY,
        CodEmpresa      int          NOT NULL,
        Anio            int          NOT NULL,
        Mes             int          NOT NULL,
        PaisDocumento   int          NULL,
        TipoDocumento   nvarchar(2)  NULL,   -- DO | FR | PA
        NroDocumento    nvarchar(16) NULL,   -- documento con dígito verificador (sin puntos/guiones)
        CI              bigint       NULL,    -- NroDocumento numérico (cédula uruguaya); NULL si pasaporte/extranjero
        PrimerApellido  nvarchar(30) NULL,
        SegundoApellido nvarchar(30) NULL,
        PrimerNombre    nvarchar(30) NULL,
        SegundoNombre   nvarchar(30) NULL,
        FechaNacimiento date         NULL,
        Sexo            int          NULL,    -- 1 masculino | 2 femenino
        Nacionalidad    int          NULL,    -- 1 uruguayo | 2 legalizado | 3 extranjero
        Usr             nvarchar(50) NULL,
        Ts              datetime2(0) NULL
    );
    CREATE INDEX IX_Atyr_Persona_Scope ON dbo.Atyr_Persona (CodEmpresa, Anio, Mes);
END
GO

-- REGISTRO TIPO 6 — ACTIVIDAD (relación empresa/persona del período).
IF OBJECT_ID('dbo.Atyr_Actividad', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Atyr_Actividad
    (
        Id                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Atyr_Actividad PRIMARY KEY,
        CodEmpresa         int          NOT NULL,
        Anio               int          NOT NULL,
        Mes                int          NOT NULL,
        PaisDocumento      int          NULL,
        TipoDocumento      nvarchar(2)  NULL,
        NroDocumento       nvarchar(16) NULL,
        CI                 bigint       NULL,
        AcumulacionLaboral int          NULL,
        FechaIngreso       date         NULL,
        TipoRemuneracion   int          NULL,
        HorasSemanales     int          NULL,
        VinculoFuncional   int          NULL,
        CodExoneracion     int          NULL,
        ComputosEspeciales int          NULL,
        Categoria          int          NULL,
        CajaActividad      int          NULL,
        AsignacionFamiliar nvarchar(1)  NULL,   -- S | N
        DiasTrabajados     int          NULL,
        HorasTrabajadas    int          NULL,
        SeguroSalud        int          NULL,
        CausalEgreso       int          NULL,
        FechaEgreso        date         NULL,
        Usr                nvarchar(50) NULL,
        Ts                 datetime2(0) NULL
    );
    CREATE INDEX IX_Atyr_Actividad_Scope ON dbo.Atyr_Actividad (CodEmpresa, Anio, Mes);
END
GO

-- REGISTRO TIPO 7 — REMUNERACIÓN (conceptos y montos imponibles por persona).
IF OBJECT_ID('dbo.Atyr_Remuneracion', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Atyr_Remuneracion
    (
        Id                 int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Atyr_Remuneracion PRIMARY KEY,
        CodEmpresa         int          NOT NULL,
        Anio               int          NOT NULL,
        Mes                int          NOT NULL,
        PaisDocumento      int          NULL,
        TipoDocumento      nvarchar(2)  NULL,
        NroDocumento       nvarchar(16) NULL,
        CI                 bigint       NULL,
        AcumulacionLaboral int          NULL,
        Concepto           int          NULL,    -- 1=imponible mensual, 2=aguinaldo, 3=compl. laudo, 5/6=IRPF, 41-46…
        Remuneracion       float        NULL,    -- monto imponible
        Jornal             float        NULL,    -- sólo construcción
        OtrosHaberes       float        NULL,    -- sólo construcción
        Usr                nvarchar(50) NULL,
        Ts                 datetime2(0) NULL
    );
    CREATE INDEX IX_Atyr_Remuneracion_Scope ON dbo.Atyr_Remuneracion (CodEmpresa, Anio, Mes);
END
GO
