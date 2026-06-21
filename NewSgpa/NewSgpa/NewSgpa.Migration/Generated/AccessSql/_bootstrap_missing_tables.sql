-- Auto-generated bootstrap for missing Access tables in SQL Server
SET NOCOUNT ON;

IF OBJECT_ID('dbo.506_Rpt_Liquidos_Subsidios') IS NULL
BEGIN
    CREATE TABLE dbo.[506_Rpt_Liquidos_Subsidios] (
        [Id] int IDENTITY(1,1) NOT NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Afiliado_Certificado') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Afiliado_Certificado] (
        [CI] int NULL,
        [Nombres] nvarchar(50) NULL,
        [Apellido1] nvarchar(30) NULL,
        [Apellido2] nvarchar(30) NULL,
        [FechaNacimiento] datetime2(0) NULL,
        [Sexo] nvarchar(1) NULL,
        [CodMutualista] smallint NULL,
        [DescMutualista] nvarchar(50) NULL,
        [Especialidad] nvarchar(255) NULL,
        [Promedio] real NULL,
        [Empleos] nvarchar(255) NULL,
        [DiaProrroga] int NULL,
        [DiasUltPro] int NULL,
        [F_Ult_Prorroga] datetime2(0) NULL,
        [F_Ult_Certificacion] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Afiliado_Certificado_Afeccion') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Afiliado_Certificado_Afeccion] (
        [CI] int NULL,
        [CodAfeccionTipo] smallint NULL,
        [DescAfeccionTipo] nvarchar(255) NULL,
        [Cantidad] int NULL,
        [Dias] smallint NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Rpt_<125_Pct') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Rpt_<125_Pct] (
        [Grupo] nvarchar(50) NULL,
        [CI] int NULL,
        [Nombre] nvarchar(100) NULL,
        [Importe] int NULL,
        [Especialidad] nvarchar(50) NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Rpt_AfiliadoMutualista') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Rpt_AfiliadoMutualista] (
        [CI] nvarchar(255) NULL,
        [Nombres] nvarchar(50) NULL,
        [Apellido1] nvarchar(30) NULL,
        [Apellido2] nvarchar(30) NULL,
        [FechaNacimiento] datetime2(0) NULL,
        [Sexo] nvarchar(1) NULL,
        [Telefono] nvarchar(25) NULL,
        [EMail] nvarchar(25) NULL,
        [CodMutualista] smallint NULL,
        [DescMutualista] nvarchar(50) NULL,
        [FechaIngMutualista] datetime2(0) NULL,
        [NroSocioMutualista] nvarchar(12) NULL,
        [CodRegimenJubilatorio] tinyint NULL,
        [DescRegimenJubilatorio] nvarchar(50) NULL,
        [Usr] nvarchar(8) NULL,
        [Ts] datetime2(0) NULL,
        [DescAfiliado] nvarchar(255) NULL,
        [Cuota] float NULL,
        [Direccion] nvarchar(100) NULL,
        [PagaMutualista] bit NULL,
        [CodDepartamento] nvarchar(3) NULL,
        [CodEmpresa] smallint NULL,
        [DescEmpresa] nvarchar(50) NULL,
        [DescSituacionMutual] nvarchar(50) NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Rpt_BPS') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Rpt_BPS] (
        [Mes] smallint NULL,
        [Anio] int NULL,
        [Cantidad] int NULL,
        [Monto] int NULL,
        [TributoMonto] int NULL,
        [ImpRetPatronal] int NULL,
        [ImpRetObrero] int NULL,
        [TotImpRet] int NULL,
        [TotImpMut] int NULL,
        [TributoTotImpMut] int NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Rpt_CantidadDescrip') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Rpt_CantidadDescrip] (
        [Codigo] int NULL,
        [Cantidad] int NULL,
        [Descrip] nvarchar(50) NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Rpt_CantidadDescrip2') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Rpt_CantidadDescrip2] (
        [Codigo] int NULL,
        [Descrip] nvarchar(50) NULL,
        [Descrip2] nvarchar(50) NULL,
        [Cantidad] int NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Rpt_Cheque_Tmp') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Rpt_Cheque_Tmp] (
        [CI] int NULL,
        [Nombre] nvarchar(255) NULL,
        [Fecha] datetime2(0) NULL,
        [Importe] real NULL,
        [Letras] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_Rpt_Recibo') IS NULL
BEGIN
    CREATE TABLE dbo.[600_Rpt_Recibo] (
        [IdSubsidio] int NULL,
        [NroRecibo] nvarchar(50) NULL,
        [Mes] nvarchar(50) NULL,
        [Nombre] nvarchar(50) NULL,
        [CI] nvarchar(50) NULL,
        [Periodo] nvarchar(255) NULL,
        [Item] nvarchar(255) NULL,
        [Importe] real NULL,
        [Signo] smallint NULL,
        [bANCO] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_SubsidioFecha_Tmp') IS NULL
BEGIN
    CREATE TABLE dbo.[600_SubsidioFecha_Tmp] (
        [IDSubsidio] int NULL,
        [DescFecha] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.600_SubsidioResumen_Tmp') IS NULL
BEGIN
    CREATE TABLE dbo.[600_SubsidioResumen_Tmp] (
        [Anio] smallint NULL,
        [Mes] tinyint NULL,
        [CI] nvarchar(255) NULL,
        [DescAfiliado] nvarchar(255) NULL,
        [Dias] smallint NULL,
        [Nombres] nvarchar(50) NULL,
        [Apellido1] nvarchar(30) NULL,
        [Apellido2] nvarchar(30) NULL,
        [ImpNominal] float NULL,
        [ImpAguinaldo] float NULL,
        [ImpLiquido] float NULL,
        [Liquidar] bit NULL,
        [FechaNacimiento] datetime2(0) NULL,
        [DescFecha] nvarchar(255) NULL,
        [CIOrig] int NULL,
        [Baja] bit NULL
    );
END;
GO

IF OBJECT_ID('dbo.acc_sp_1030_FacturaFlujo_q') IS NULL
BEGIN
    CREATE TABLE dbo.[acc_sp_1030_FacturaFlujo_q] (
        [IdPrestamo] int NULL,
        [Mes] int NULL,
        [Importe] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.Atyro') IS NULL
BEGIN
    CREATE TABLE dbo.[Atyro] (
        [Id] int IDENTITY(1,1) NOT NULL
    );
END;
GO

IF OBJECT_ID('dbo.Bps2') IS NULL
BEGIN
    CREATE TABLE dbo.[Bps2] (
        [TipoReg] int NULL,
        [CI] nvarchar(255) NULL,
        [Apellido1] nvarchar(255) NULL,
        [Apellido2] nvarchar(255) NULL,
        [Nombres] nvarchar(255) NULL,
        [FechaNacimiento] datetime2(0) NULL,
        [Sexo] nvarchar(255) NULL,
        [Nacionalidad] nvarchar(255) NULL,
        [Reservado1] nvarchar(255) NULL,
        [Reservado2] nvarchar(255) NULL,
        [Reservado3] nvarchar(255) NULL,
        [Reservado4] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.Bps2_2') IS NULL
BEGIN
    CREATE TABLE dbo.[Bps2_2] (
        [TipoReg] int NULL,
        [Pais] int NULL,
        [TipoDocumento] nvarchar(255) NULL,
        [CI] nvarchar(255) NULL,
        [PrimerApellido] nvarchar(255) NULL,
        [SegundoApellido] nvarchar(255) NULL,
        [PrimerNombre] nvarchar(255) NULL,
        [SegundoNombre] nvarchar(255) NULL,
        [FechaNacimiento] datetime2(0) NULL,
        [Sexo] nvarchar(255) NULL,
        [Nacionalidad] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.Bps3') IS NULL
BEGIN
    CREATE TABLE dbo.[Bps3] (
        [TipoReg] int NULL,
        [CI] nvarchar(255) NULL,
        [AcumulacionLaboral] nvarchar(255) NULL,
        [FechaIngreso] datetime2(0) NULL,
        [SeguroSalud] nvarchar(255) NULL,
        [RemuneracionTipo] nvarchar(255) NULL,
        [HorasSemanales] nvarchar(255) NULL,
        [VinculoFuncional] nvarchar(255) NULL,
        [CodigoExoneracion] nvarchar(255) NULL,
        [ComputosEspeciales] nvarchar(255) NULL,
        [CausalBaja] nvarchar(255) NULL,
        [FechaBaja] datetime2(0) NULL,
        [LocalEmpresa] nvarchar(255) NULL,
        [DiasTrabajados] int NULL,
        [HorasTrabajadas] nvarchar(255) NULL,
        [Reservado1] nvarchar(255) NULL,
        [Reservado2] nvarchar(255) NULL,
        [Reservado3] nvarchar(255) NULL,
        [Reservado4] nvarchar(255) NULL,
        [Reservado5] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.Bps4') IS NULL
BEGIN
    CREATE TABLE dbo.[Bps4] (
        [TipoReg] int NULL,
        [CI] nvarchar(255) NULL,
        [AcumulacionLaboral] nvarchar(255) NULL,
        [Concepto] nvarchar(255) NULL,
        [Imponible] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.Bps4_2') IS NULL
BEGIN
    CREATE TABLE dbo.[Bps4_2] (
        [TipoReg] int NULL,
        [MesAño] int NULL,
        [Pais] int NULL,
        [TipoDocumento] nvarchar(255) NULL,
        [CI] nvarchar(255) NULL,
        [AcumulacionLaboral] int NULL,
        [Concepto] int NULL,
        [Imponible] float NULL,
        [Jornal] float NULL,
        [OtrosHaberes] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.BpsFormat') IS NULL
BEGIN
    CREATE TABLE dbo.[BpsFormat] (
        [Cedula] nvarchar(50) NULL,
        [Mutualista] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.BROU') IS NULL
BEGIN
    CREATE TABLE dbo.[BROU] (
        [CI] float NULL,
        [NOMBRE Y APELLIDO] nvarchar(255) NULL,
        [Nº CUENTA] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.CargaLiquidos') IS NULL
BEGIN
    CREATE TABLE dbo.[CargaLiquidos] (
        [vlbidn] float NULL,
        [apellido] nvarchar(255) NULL,
        [nombre] nvarchar(255) NULL,
        [cedula] float NULL,
        [chkdig] float NULL,
        [dd_egre] float NULL,
        [mm_egre] float NULL,
        [aa_egre] float NULL,
        [imphaberes] float NULL,
        [impdescuen] float NULL,
        [liquido] float NULL,
        [cargo] float NULL,
        [cantmov] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.CargarBancos') IS NULL
BEGIN
    CREATE TABLE dbo.[CargarBancos] (
        [Id] int NULL,
        [CI] float NULL,
        [Mes] float NULL,
        [Nombres] nvarchar(255) NULL,
        [Apellido1] nvarchar(255) NULL,
        [Reliquidación] float NULL,
        [Banco] nvarchar(255) NULL,
        [NroCuenta] nvarchar(255) NULL,
        [CodBanco] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.Casecasm') IS NULL
BEGIN
    CREATE TABLE dbo.[Casecasm] (
        [Campo1] nvarchar(255) NULL,
        [Campo2] nvarchar(255) NULL,
        [Campo3] nvarchar(255) NULL,
        [Campo4] nvarchar(255) NULL,
        [Campo5] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.Cristalin') IS NULL
BEGIN
    CREATE TABLE dbo.[Cristalin] (
        [DOCUMENTO] nvarchar(50) NULL,
        [1ER APELLIDO] nvarchar(255) NULL,
        [2DO APELLIDO] nvarchar(255) NULL,
        [1ER NOMBRE] nvarchar(255) NULL,
        [2DO NOMBRE] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.CtaCteRet') IS NULL
BEGIN
    CREATE TABLE dbo.[CtaCteRet] (
        [IDPrestamo] int NULL,
        [Importe] float NULL,
        [cobros] float NULL,
        [saldo] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.CTASBROU') IS NULL
BEGIN
    CREATE TABLE dbo.[CTASBROU] (
        [CI] int NULL,
        [Cta] nvarchar(50) NULL
    );
END;
GO

IF OBJECT_ID('dbo.DISCOUNT') IS NULL
BEGIN
    CREATE TABLE dbo.[DISCOUNT] (
        [CI] float NULL,
        [FICHA] float NULL,
        [NOMBRE Y APELLIDO] nvarchar(255) NULL,
        [Nº CUENTA] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.ENERO') IS NULL
BEGIN
    CREATE TABLE dbo.[ENERO] (
        [Nro#] float NULL,
        [C#I#] float NULL,
        [Nombres] nvarchar(255) NULL,
        [Apellido1] nvarchar(255) NULL,
        [importe] float NULL,
        [F6] float NULL,
        [F7] nvarchar(255) NULL,
        [F8] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.ExportedQueries') IS NULL
BEGIN
    CREATE TABLE dbo.[ExportedQueries] (
        [QueryName] nvarchar(255) NULL,
        [QueryType] nvarchar(50) NULL,
        [Parameters] nvarchar(max) NULL,
        [SqlText] nvarchar(max) NULL
    );
END;
GO

IF OBJECT_ID('dbo.FEBRERO') IS NULL
BEGIN
    CREATE TABLE dbo.[FEBRERO] (
        [Nro#] float NULL,
        [C#I#] float NULL,
        [Nombres] nvarchar(255) NULL,
        [Apellido1] nvarchar(255) NULL,
        [IMPORTE] float NULL,
        [F6] nvarchar(255) NULL,
        [F7] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.Grupo_Usuario') IS NULL
BEGIN
    CREATE TABLE dbo.[Grupo_Usuario] (
        [Cod_Grupo] nvarchar(8) NULL,
        [Login] nvarchar(8) NULL
    );
END;
GO

IF OBJECT_ID('dbo.IMP') IS NULL
BEGIN
    CREATE TABLE dbo.[IMP] (
        [CI] int NULL,
        [Importe] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.Imponible_ok') IS NULL
BEGIN
    CREATE TABLE dbo.[Imponible_ok] (
        [Id] int IDENTITY(1,1) NOT NULL
    );
END;
GO

IF OBJECT_ID('dbo.InteresDiciembre') IS NULL
BEGIN
    CREATE TABLE dbo.[InteresDiciembre] (
        [Id] int IDENTITY(1,1) NOT NULL
    );
END;
GO

IF OBJECT_ID('dbo.IRPControl') IS NULL
BEGIN
    CREATE TABLE dbo.[IRPControl] (
        [CodIRP] int NULL,
        [ImpFrjAnt] real NULL,
        [FranjaAnt] real NULL,
        [SMNAnt] real NULL
    );
END;
GO

IF OBJECT_ID('dbo.Liquidacion_BPS') IS NULL
BEGIN
    CREATE TABLE dbo.[Liquidacion_BPS] (
        [Id] int NULL,
        [CI] float NOT NULL,
        [NOMBRE] nvarchar(255) NULL,
        [APELLIDO] nvarchar(255) NULL,
        [MONTO_TOTAL] float NULL,
        [MES_DE_CARGO] int NOT NULL,
        [NOM_EMPRESA] nvarchar(255) NULL,
        [PCT_POR_EMPRESA] float NOT NULL,
        [FECHA_PER_DESDE] datetime2(0) NULL,
        [FECHA_PER_HASTA] datetime2(0) NULL,
        [N_ ENTREGA] int NULL,
        [FECHA_DE_ENTREGA] datetime2(0) NULL,
        [MES] smallint NULL,
        [ANIO] smallint NULL,
        [LIQUIDO] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.MaeFun') IS NULL
BEGIN
    CREATE TABLE dbo.[MaeFun] (
        [NroFun] int NULL,
        [NroCuenta] int NULL,
        [Apellido1] nvarchar(15) NOT NULL,
        [Apellido2] nvarchar(15) NULL,
        [Nombre1] nvarchar(15) NOT NULL,
        [Nombre2] nvarchar(15) NULL,
        [Cedula] int NULL,
        [Nacionalidad] smallint NULL,
        [FecNac] datetime2(0) NULL,
        [EstCivil] smallint NULL,
        [DirCalle] nvarchar(20) NULL,
        [DirPuerta] nvarchar(4) NULL,
        [DirBis] nvarchar(2) NULL,
        [DirPiso] nvarchar(2) NULL,
        [DirApto] nvarchar(4) NULL,
        [DirBloque] nvarchar(2) NULL,
        [DirLocal] nvarchar(30) NULL,
        [CodCargo] int NULL,
        [DesCargo] nvarchar(25) NULL,
        [Telefono] nvarchar(8) NULL,
        [FecIngreso] datetime2(0) NULL,
        [InfDbla] bit NULL,
        [FecInfDbla] datetime2(0) NULL,
        [AsigCuenta] smallint NULL,
        [FecAsigCta] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.Moneda') IS NULL
BEGIN
    CREATE TABLE dbo.[Moneda] (
        [Moneda] nvarchar(3) NULL
    );
END;
GO

IF OBJECT_ID('dbo.qCorregirFinSubsidio') IS NULL
BEGIN
    CREATE TABLE dbo.[qCorregirFinSubsidio] (
        [Id] int NULL,
        [CI] int NULL,
        [Nombre y apellido] nvarchar(255) NULL,
        [Fecha Inicio] datetime2(0) NULL,
        [Fecha Fin] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.Riesgo_Setiembre') IS NULL
BEGIN
    CREATE TABLE dbo.[Riesgo_Setiembre] (
        [Id] int NULL,
        [CI] int NULL,
        [Nombre] nvarchar(255) NULL,
        [Fecha Inicio] datetime2(0) NULL,
        [Fecha Fin] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.rptCheque_Tmp') IS NULL
BEGIN
    CREATE TABLE dbo.[rptCheque_Tmp] (
        [IDPrestamo] int NULL,
        [CI] int NULL,
        [Nombre] nvarchar(255) NULL,
        [Fecha] datetime2(0) NULL,
        [Importe] real NULL,
        [Letras] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.rptPlanes_Tmp') IS NULL
BEGIN
    CREATE TABLE dbo.[rptPlanes_Tmp] (
        [Cuotas] smallint NULL,
        [ValorCuota] real NULL,
        [Monto] real NULL
    );
END;
GO

IF OBJECT_ID('dbo.rptTIR_Tmp') IS NULL
BEGIN
    CREATE TABLE dbo.[rptTIR_Tmp] (
        [IDPrestamo] int NULL,
        [Mes] int NULL,
        [Importe] real NULL
    );
END;
GO

IF OBJECT_ID('dbo.rptVale_Tmp') IS NULL
BEGIN
    CREATE TABLE dbo.[rptVale_Tmp] (
        [CI] int NULL,
        [Nombres] nvarchar(255) NULL,
        [Apellido1] nvarchar(50) NULL,
        [Apellido2] nvarchar(50) NULL,
        [Direccion] nvarchar(255) NULL,
        [ImporteTotal] real NULL,
        [LetraImporte] nvarchar(255) NULL,
        [DescMoneda] nvarchar(50) NULL,
        [DescMonedaLargo] nvarchar(50) NULL,
        [Cuotas] smallint NULL,
        [ImporteCuota] real NULL,
        [FechaVencimiento] datetime2(0) NULL,
        [Tasa] real NULL
    );
END;
GO

IF OBJECT_ID('dbo.Rpt_Historia_Vandalismo_S') IS NULL
BEGIN
    CREATE TABLE dbo.[Rpt_Historia_Vandalismo_S] (
        [Id] int IDENTITY(1,1) NOT NULL
    );
END;
GO

IF OBJECT_ID('dbo.SP_Afiliado') IS NULL
BEGIN
    CREATE TABLE dbo.[SP_Afiliado] (
        [CI] int NOT NULL,
        [Nombres] nvarchar(50) NULL,
        [Apellido1] nvarchar(30) NULL,
        [Apellido2] nvarchar(30) NULL,
        [FechaNacimiento] datetime2(0) NULL,
        [Sexo] nvarchar(1) NULL,
        [Direccion] nvarchar(100) NULL,
        [Telefono] nvarchar(25) NULL,
        [EMail] nvarchar(100) NULL,
        [CodMutualista] smallint NULL,
        [FechaIngMutualista] datetime2(0) NULL,
        [FechaBajaMutualista] datetime2(0) NULL,
        [NroSocioMutualista] nvarchar(12) NULL,
        [CodRegimenJubilatorio] tinyint NULL,
        [CodDepartamento] nvarchar(3) NULL,
        [PagaMutualista] bit NULL,
        [CodSituacionMutual] nvarchar(2) NULL,
        [CodBanco] int NULL,
        [NroCuenta] nvarchar(50) NULL,
        [NroFunCuenta] nvarchar(50) NULL,
        [Movil] nvarchar(50) NULL,
        [Usr] nvarchar(8) NULL,
        [Ts] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.SP_CuadroAmortizacion_Tmp') IS NULL
BEGIN
    CREATE TABLE dbo.[SP_CuadroAmortizacion_Tmp] (
        [NroCuota] smallint NULL,
        [Monto] real NULL,
        [ImporteCuota] real NULL,
        [Interes] real NULL,
        [Amortizacion] real NULL,
        [Saldo] real NULL
    );
END;
GO

IF OBJECT_ID('dbo.SP_Empresa') IS NULL
BEGIN
    CREATE TABLE dbo.[SP_Empresa] (
        [CodEmpresa] smallint NULL,
        [Nombre] nvarchar(50) NULL,
        [Direccion] nvarchar(50) NULL,
        [Telefono] nvarchar(25) NULL,
        [Fax] nvarchar(25) NULL,
        [EMail] nvarchar(25) NULL,
        [AporteCasemed] real NULL,
        [AporteAguinaldo] int NULL,
        [PersonaContacto] nvarchar(50) NULL,
        [Autoridades] nvarchar(255) NULL,
        [CodRegimenAporte] smallint NULL,
        [CodSituacionPago] smallint NULL,
        [Liquidar] bit NULL,
        [Ficticia] bit NULL,
        [Usr] nvarchar(8) NULL,
        [Ts] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.SP_Trabaja') IS NULL
BEGIN
    CREATE TABLE dbo.[SP_Trabaja] (
        [CI] int NULL,
        [CodEmpresa] smallint NULL,
        [FechaIngreso] datetime2(0) NULL,
        [FechaBaja] datetime2(0) NULL,
        [CodBajaMotivo] int NULL,
        [NroFichaEmpresa] nvarchar(20) NULL,
        [IdTrabaja] int NULL,
        [FechaIngCasemed] datetime2(0) NULL,
        [Usr] nvarchar(8) NULL,
        [Ts] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.tmpCorrecciones') IS NULL
BEGIN
    CREATE TABLE dbo.[tmpCorrecciones] (
        [CI] float NULL,
        [Nombre] nvarchar(255) NULL,
        [Fecha Inicio] datetime2(0) NULL,
        [Fecha Fin] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.tmp_Anticipados') IS NULL
BEGIN
    CREATE TABLE dbo.[tmp_Anticipados] (
        [IDPrestamo] int NULL
    );
END;
GO

IF OBJECT_ID('dbo.tmp_Cantidad_Por_Puesto') IS NULL
BEGIN
    CREATE TABLE dbo.[tmp_Cantidad_Por_Puesto] (
        [CodEmpresa] smallint NULL,
        [Nombre] nvarchar(50) NULL,
        [Cantidad] int NULL,
        [CantidadNo0] int NULL
    );
END;
GO

IF OBJECT_ID('dbo.tmp_FacturasAnticipadas') IS NULL
BEGIN
    CREATE TABLE dbo.[tmp_FacturasAnticipadas] (
        [IdPrestamo] int NULL,
        [IDFactura] int NULL
    );
END;
GO

IF OBJECT_ID('dbo.tmp_Rectificativas') IS NULL
BEGIN
    CREATE TABLE dbo.[tmp_Rectificativas] (
        [Id] int NULL,
        [EMPRESA] float NULL,
        [CI] float NULL,
        [Concepto] float NULL,
        [Importe] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.tmp_ReporteBPS_Full') IS NULL
BEGIN
    CREATE TABLE dbo.[tmp_ReporteBPS_Full] (
        [CI] int NULL,
        [Dias] smallint NULL,
        [Nombres] nvarchar(50) NULL,
        [Apellido1] nvarchar(30) NULL,
        [Apellido2] nvarchar(30) NULL,
        [FechaNacimiento] datetime2(0) NULL,
        [IdSubsidio] int NULL,
        [NroRecibo] int NULL,
        [FechaIni] datetime2(0) NULL,
        [FechaFin] datetime2(0) NULL,
        [FechaIniSubsidio] datetime2(0) NULL,
        [FechaFinSubsidio] datetime2(0) NULL,
        [ImpNominal] float NULL,
        [ImpAguinaldo] float NULL,
        [ImpLiquido] float NULL,
        [Jornal70] float NULL,
        [Aguinaldo70] float NULL,
        [DiasBPS] int NULL,
        [LiquidoBPS] float NULL,
        [LiquidoPagar] float NULL,
        [Banco] nvarchar(50) NULL,
        [NroCuenta] nvarchar(50) NULL,
        [MONTO_TOTAL] float NULL,
        [MES_DE_CARGO] int NULL,
        [NOM_EMPRESA] nvarchar(255) NULL,
        [PCT_POR_EMPRESA] float NULL,
        [FECHA_PER_DESDE] datetime2(0) NULL,
        [FECHA_PER_HASTA] datetime2(0) NULL,
        [N_ ENTREGA] int NULL,
        [FECHA_DE_ENTREGA] datetime2(0) NULL
    );
END;
GO

IF OBJECT_ID('dbo.xLiq1') IS NULL
BEGIN
    CREATE TABLE dbo.[xLiq1] (
        [CI] int NULL
    );
END;
GO

IF OBJECT_ID('dbo.xLiq2') IS NULL
BEGIN
    CREATE TABLE dbo.[xLiq2] (
        [CI] int NULL
    );
END;
GO

IF OBJECT_ID('dbo.xUsrParam') IS NULL
BEGIN
    CREATE TABLE dbo.[xUsrParam] (
        [login] nvarchar(8) NULL,
        [clave] nvarchar(100) NULL,
        [orden] smallint NULL,
        [value1] nvarchar(255) NULL,
        [value2] nvarchar(255) NULL,
        [value3] nvarchar(255) NULL,
        [value4] nvarchar(255) NULL,
        [value5] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.xw_Suma_ValorJornal') IS NULL
BEGIN
    CREATE TABLE dbo.[xw_Suma_ValorJornal] (
        [IdSubsidio] int NULL,
        [SumaDeValorJornal] float NULL
    );
END;
GO

IF OBJECT_ID('dbo.zRs_AEsp') IS NULL
BEGIN
    CREATE TABLE dbo.[zRs_AEsp] (
        [CI] int NULL,
        [EspNom1] nvarchar(255) NULL,
        [EspNom2] nvarchar(255) NULL,
        [EspNom3] nvarchar(255) NULL
    );
END;
GO

IF OBJECT_ID('dbo.Casecasm') IS NOT NULL
BEGIN
    IF COL_LENGTH('dbo.Casecasm','Campo1') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo1] nvarchar(255) NULL;
    IF COL_LENGTH('dbo.Casecasm','Campo2') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo2] nvarchar(255) NULL;
    IF COL_LENGTH('dbo.Casecasm','Campo3') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo3] nvarchar(255) NULL;
    IF COL_LENGTH('dbo.Casecasm','Campo4') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo4] nvarchar(255) NULL;
    IF COL_LENGTH('dbo.Casecasm','Campo5') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo5] nvarchar(255) NULL;
END;
GO

