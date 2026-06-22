using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using NewSgpa.Migration.AccessQueries;
using NewSgpa.Module.BusinessObjects;
using NewSgpa.Module.BusinessObjects.Sgpa;
using NewSgpa.Module.BusinessObjects.Prestamos;
using NewSgpa.Module.BusinessObjects.Shared;

namespace NewSgpa.Migration;

class Program
{
    // Configurables por argumento (--clave=valor) o variable de entorno; estos son los valores por defecto.
    // Ver ConfigurarDesde(args). Dejaron de ser const para poder sobreescribirlos en tiempo de ejecución.
    static string SgpaServ2k3Mdb = @"C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb";
    static string SpServ2k3Mdb   = @"C:\Personal\Gestion\CASEMED\VB6\sp\Data\spserv2k3.mdb";
    static string SqlConn        = "Data Source=localhost;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Command Timeout=300;Initial Catalog=NewSgpa2";
    static string OleDbProvider  = "Microsoft.ACE.OLEDB.12.0";
    static int TotalRowsImported;

    static string OleDbConn(string mdb) =>
        $"Provider={OleDbProvider};Data Source={mdb};";

    /// <summary>
    /// Resuelve la configuración (rutas de los .mdb, cadena/nombre de la base SQL, proveedor OLEDB) desde
    /// argumentos de línea de comando o variables de entorno; si no se especifican, usa los valores por defecto.
    /// Precedencia: argumento --clave=valor &gt; variable de entorno &gt; default.
    ///   --sgpa-mdb / NEWSGPA_SGPA_MDB     ruta del backend SGPA (sgpaserv2k3.mdb)
    ///   --sp-mdb   / NEWSGPA_SP_MDB       ruta del backend SP (spserv2k3.mdb)
    ///   --sql      / NEWSGPA_SQL          cadena de conexión SQL completa
    ///   --db       / NEWSGPA_DB           atajo: cambia sólo el catálogo (Initial Catalog) de la cadena
    ///   --oledb-provider / NEWSGPA_OLEDB_PROVIDER   proveedor OLEDB (ACE 12.0 por defecto; Jet 4.0 para .mdb viejos)
    /// </summary>
    static void ConfigurarDesde(string[] args)
    {
        SgpaServ2k3Mdb = Resolver(args, "--sgpa-mdb", "NEWSGPA_SGPA_MDB", SgpaServ2k3Mdb);
        SpServ2k3Mdb   = Resolver(args, "--sp-mdb",   "NEWSGPA_SP_MDB",   SpServ2k3Mdb);
        SqlConn        = Resolver(args, "--sql",      "NEWSGPA_SQL",      SqlConn);
        OleDbProvider  = Resolver(args, "--oledb-provider", "NEWSGPA_OLEDB_PROVIDER", OleDbProvider);

        var dbName = Resolver(args, "--db", "NEWSGPA_DB", "");
        if (!string.IsNullOrWhiteSpace(dbName))
            SqlConn = Regex.Replace(SqlConn, @"(?i)(Initial Catalog|Database)\s*=\s*[^;]*", "Initial Catalog=" + dbName);

        Console.WriteLine("Configuración:");
        Console.WriteLine($"  SGPA mdb : {SgpaServ2k3Mdb}");
        Console.WriteLine($"  SP mdb   : {SpServ2k3Mdb}");
        Console.WriteLine($"  OLEDB    : {OleDbProvider}");
        Console.WriteLine($"  SQL      : {Regex.Replace(SqlConn, @"(?i)(Password|Pwd)\s*=\s*[^;]*", "$1=***")}\n");
    }

    static string Resolver(string[] args, string argKey, string envKey, string def)
    {
        var prefix = argKey + "=";
        var hit = args.FirstOrDefault(a => a.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        if (hit != null) return hit[prefix.Length..].Trim().Trim('"');
        var env = Environment.GetEnvironmentVariable(envKey);
        return string.IsNullOrWhiteSpace(env) ? def : env;
    }

    static void EnsureSourceReadable(string mdb)
    {
        var cs = OleDbConn(mdb);
        try
        {
            using var cn = new OleDbConnection(cs);
            cn.Open();
            using var cm = new OleDbCommand("SELECT 1", cn);
            _ = cm.ExecuteScalar();
        }
        catch (OleDbException ex)
        {
            throw new InvalidOperationException($"Cannot open source database '{mdb}'. {OleDbError(ex)}", ex);
        }
    }

    static string PrepareSource(string mdb)
    {
        EnsureSourceReadable(mdb);
        return mdb;
    }

    static string OleDbError(OleDbException ex)
    {
        if (ex.Errors.Count > 0)
        {
            var e = ex.Errors[0];
            return $"{e.Message} (NativeError={e.NativeError}, SQLState={e.SQLState})";
        }

        return ex.Message;
    }

    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== NewSgpa Migration Tool ===\n");

            ConfigurarDesde(args);

            var onlyGenerateAccessSql = args.Any(a => a.Equals("--generate-access-sql-only", StringComparison.OrdinalIgnoreCase));

            GenerateAccessQueriesSqlArtifacts();
            if (onlyGenerateAccessSql)
                return;

            var skipSgpa = args.Any(a => a.Equals("--skip-sgpa", StringComparison.OrdinalIgnoreCase));
            var skipSp = args.Any(a => a.Equals("--skip-sp", StringComparison.OrdinalIgnoreCase));
            // Sólo rellenar (verbatim) las tablas que la migración tipada no carga, sin dropear la base.
            var backfillOnly = args.Any(a => a.Equals("--backfill-only", StringComparison.OrdinalIgnoreCase));
            var scriptsOnlyRun = skipSgpa && skipSp;
            var partialRun = skipSgpa || skipSp;

            Console.WriteLine($"Options: skip-sgpa={skipSgpa}, skip-sp={skipSp}\n");
            
            var opt = new DbContextOptionsBuilder<MigrationDbContext>();
            opt.UseSqlServer(SqlConn);
            
            using var db = new MigrationDbContext(opt.Options);

            if (backfillOnly)
            {
                Console.WriteLine("Backfill-only: rellenando tablas vacías sin dropear la base.\n");
                int copied = 0;
                if (File.Exists(SgpaServ2k3Mdb)) copied += await BackfillEmptyTablesVerbatimAsync(OleDbConn(PrepareSource(SgpaServ2k3Mdb)), "SGPA");
                if (File.Exists(SpServ2k3Mdb)) copied += await BackfillEmptyTablesVerbatimAsync(OleDbConn(PrepareSource(SpServ2k3Mdb)), "SP");
                Console.WriteLine($"\n=== Backfill done === filas copiadas: {copied}");
                return;
            }

            if (partialRun)
            {
                Console.WriteLine("Partial run detected (--skip-sgpa/--skip-sp): preserving target database.");
                Console.Write("Ensuring schema... ");
                await db.Database.EnsureCreatedAsync();
                Console.WriteLine("OK\n");
            }
            else
            {
                Console.Write("Dropping old database... ");
                await db.Database.EnsureDeletedAsync();
                Console.WriteLine("OK");
                Console.Write("Creating schema... ");
                await db.Database.EnsureCreatedAsync();
                Console.WriteLine("OK\n");
            }

            Console.Write("Applying generated SQL artifacts... ");
            var artifactsResult = await ApplyGeneratedAccessSqlArtifactsAsync(db);
            Console.WriteLine($"OK (applied: {artifactsResult.AppliedBatches}, failed: {artifactsResult.FailedBatches})\n");
            if (!string.IsNullOrWhiteSpace(artifactsResult.FailuresReportPath))
                Console.WriteLine($"SQL artifact failures report: {artifactsResult.FailuresReportPath}\n");

            // Parches post-generación: arreglos a los objetos acc_sgpa_* generados que NO se pueden expresar
            // en la traducción Access→SQL (overflow 1.25*@pSMN, filtro de activo en 805). Idempotentes (ALTER).
            Console.Write("Applying post-generation fixes (PostFixes/*.sql)... ");
            await ApplyPostGenerationFixesAsync(db);

            await ReplaceLinkedSpTablesWithViewsAsync(db);

            await CreateInfrastructureTablesAsync(db);

            if (scriptsOnlyRun)
            {
                Console.WriteLine("Both --skip-sgpa and --skip-sp were specified. SQL artifacts were applied; data migration was skipped.");
                return;
            }

            if (!skipSgpa)
            {
                if (!File.Exists(SgpaServ2k3Mdb))
                    throw new FileNotFoundException($"SGPA source not found: {SgpaServ2k3Mdb}");

                Console.WriteLine($"Using SGPA source: {SgpaServ2k3Mdb}");
                var sgSource = PrepareSource(SgpaServ2k3Mdb);
                var sg = OleDbConn(sgSource);

                // Temporary: run Shared first to fail early on Seleccion issues.
                await Shared(sg, db);
                Console.WriteLine($"Seleccion rows in SQL after SGPA Shared: {await db.Set<Seleccion>().CountAsync()}");

                await Lookups(sg, db);

                // Retry lookups that depend on other lookup tables loaded later in the first pass
                await R<Empresa>(sg, db, "Empresa", r => new()
                {
                    CodEmpresa = I(r, "CodEmpresa"),
                    Nombre = S(r, "Nombre"),
                    Direccion = S(r, "Direccion"),
                    Telefono = S(r, "Telefono"),
                    Fax = S(r, "Fax"),
                    EMail = S(r, "EMail"),
                    AporteCasemed = Fl(r, "AporteCasemed"),
                    AporteAguinaldo = IN(r, "AporteAguinaldo"),
                    PersonaContacto = S(r, "PersonaContacto"),
                    Autoridades = S(r, "Autoridades"),
                    CodRegimenAporte = IN(r, "CodRegimenAporte"),
                    CodSituacionPago = IN(r, "CodSituacionPago"),
                    Liquidar = B(r, "Liquidar"),
                    Ficticia = B(r, "Ficticia"),
                    Usr = S(r, "Usr"),
                    Ts = Dt(r, "Ts")
                });

                await Afiliados(sg, db);
                await Transactional(sg, db);
                await Subsidios(sg, db);
                TotalRowsImported += await BackfillEmptyTablesVerbatimAsync(sg, "SGPA");
            }

            if (!skipSp)
            {
                if (!File.Exists(SpServ2k3Mdb))
                    throw new FileNotFoundException($"SP source not found: {SpServ2k3Mdb}");

                Console.WriteLine($"Using SP source: {SpServ2k3Mdb}");
                var spSource = PrepareSource(SpServ2k3Mdb);
                var sp = OleDbConn(spSource);
                await Prestamos(sp, db);
                TotalRowsImported += await BackfillEmptyTablesVerbatimAsync(sp, "SP");
            }

            if (TotalRowsImported == 0)
                throw new InvalidOperationException("Migration finished without importing rows. Verify source MDB format/provider and table mappings.");

            Console.WriteLine($"\n=== Done === Rows imported: {TotalRowsImported}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFATAL ERROR: {ex.GetType().Name}");
            Console.WriteLine($"Message: {ex.Message}");
            if (ex.InnerException != null)
                Console.WriteLine($"Inner: {ex.InnerException.Message}");
            Environment.Exit(1);
        }
    }

    static async Task Lookups(string c, MigrationDbContext d) { Hdr("Lookups"); await R<Mutualista>(c,d,"Mutualista",r=>new(){CodMutualista=I(r,"CodMutualista"),Descrip=S(r,"Nombre"),Direccion=S(r,"Direccion"),Telefono=S(r,"Telefono"),Fax=S(r,"Fax"),EMail=S(r,"EMail"),Ficticia=B(r,"Ficticia"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Patologia>(c,d,"Patologia",r=>new(){CodPatologia=I(r,"CodPatologia"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<AfeccionGrupo>(c,d,"AfeccionGrupo",r=>new(){CodAfeccionGrupo=I(r,"CodAfeccionGrupo"),Descrip=S(r,"Descrip"),CodPatologia=IN(r,"CodPatologia"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<AfeccionTipo>(c,d,"AfeccionTipo",r=>new(){CodAfeccionTipo=I(r,"CodAfeccionTipo"),Descrip=S(r,"Descrip"),CodAfeccionGrupo=IN(r,"CodAfeccionGrupo"),CodDiameg=IN(r,"CodDiameg"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Certificador>(c,d,"Certificador",r=>new(){CodCertificador=I(r,"CodCertificador"),Descrip=S(r,"Descrip"),Direccion=S(r,"Direccion"),Telefono=S(r,"Telefono"),Fax=S(r,"Fax"),Bip=S(r,"Bip"),CobraLlamado=B(r,"CobraLlamado"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<SalidaTipo>(c,d,"SalidaTipo",r=>new(){CodSalidaTipo=I(r,"CodSalidaTipo"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<BajaMotivo>(c,d,"BajaMotivo",r=>new(){CodBajaMotivo=I(r,"CodBajaMotivo"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Banco>(c,d,"Banco",r=>new(){CodBanco=I(r,"CodBanco"),Descripcion=S(r,"Descripcion"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<FormaPago>(c,d,"FormaPago",r=>new(){CodFormaPago=I(r,"CodFormaPago"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<RegimenAporte>(c,d,"RegimenAporte",r=>new(){CodRegimenAporte=I(r,"CodRegimenAporte"),Descrip=S(r,"Descrip"),Porcentaje=IN(r,"Porcentaje"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<RegimenJubilatorio>(c,d,"RegimenJubilatorio",r=>new(){CodRegimenJubilatorio=By(r,"CodRegimenJubilatorio"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<AporteTipo>(c,d,"AporteTipo",r=>new(){CodAporteTipo=S(r,"CodAporteTipo"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<SituacionPago>(c,d,"SituacionPago",r=>new(){CodSituacionPago=I(r,"CodSituacionPago"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<SituacionMutual>(c,d,"SituacionMutual",r=>new(){CodSituacionMutual=S(r,"CodSituacionMutual"),Descrip=S(r,"Descrip"),Pagar=B(r,"Pagar"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Departamento>(c,d,"Departamento",r=>new(){CodDepartamento=S(r,"CodDepartamento"),Descrip=S(r,"Descrip")}); await R<Especialidad>(c,d,"Especialidad",r=>new(){CodEspecialidad=I(r,"CodEspecialidad"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<PrestacionTipo>(c,d,"PrestacionTipo",r=>new(){CodPrestacionTipo=I(r,"CodPrestacionTipo"),Descrip=S(r,"Descrip"),Importe=Fl(r,"Importe"),CodMoneda=S(r,"CodMoneda"),FechaVigencia=Dt(r,"FechaVigencia"),ImporteTopeDISSE=Db(r,"ImporteTopeDISSE"),ImporteTopeCASEMED=Db(r,"ImporteTopeCASEMED"),PeriodoRenovacion=IN(r,"PeriodoRenovacion"),Receta=B(r,"Receta"),Obs=S(r,"Obs")}); await R<RecetaDistancia>(c,d,"RecetaDistancia",r=>new(){CodRecetaDistancia=S(r,"CodRecetaDistancia"),Descrip=S(r,"Descrip")}); await R<Ims>(c,d,"IMS",r=>new(){Mes=IN(r,"Mes"),Anio=IN(r,"Anio"),Importe=Fl(r,"Importe"),AnioMes=IN(r,"AnioMes"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await RNoKey(c,d,"FranjaIRPF"); await RNoKey(c,d,"Parametros"); await R<SubsidioItemCod>(c,d,"SubsidioItemCod",r=>new(){CodSubsidioItemCod=I(r,"CodSubsidioItemCod"),Descrip=S(r,"Descrip"),Orden=IN(r,"Orden"),Signo=IN(r,"Signo"),Operador=S(r,"Operador"),ValorMin=Db(r,"ValorMin"),ValorMax=Db(r,"ValorMax"),Procesar=B(r,"Procesar"),FechaVigencia=Dt(r,"FechaVigencia"),FechaBaja=Dt(r,"FechaBaja"),AperturaXEmpresa=B(r,"AperturaXEmpresa"),ModificaNominal=B(r,"ModificaNominal")}); await R<InformeEstadistico>(c,d,"InformeEstadistico",r=>new(){IdRpt=I(r,"IdRpt"),Grupo=S(r,"Grupo"),Orden=IN(r,"Orden"),TituloPantalla=S(r,"TituloPantalla"),TituloRpt=S(r,"TituloRpt"),MesAnio=B(r,"MesAnio"),Periodo=B(r,"Periodo"),Empresa=B(r,"Empresa"),Fecha=B(r,"Fecha"),GrupoEtario=B(r,"GrupoEtario"),Patologia=B(r,"Patologia"),Comentario=S(r,"Comentario")}); }

    static async Task Afiliados(string c, MigrationDbContext d) { Hdr("Afiliados"); await R<Afiliado>(c,d,"Afiliado",r=>new(){CI=L(r,"CI"),Nombres=S(r,"Nombres"),Apellido1=S(r,"Apellido1"),Apellido2=S(r,"Apellido2"),FechaNacimiento=Dt(r,"FechaNacimiento"),Sexo=S(r,"Sexo"),Direccion=S(r,"Direccion"),Telefono=S(r,"Telefono"),EMail=S(r,"EMail"),Movil=S(r,"Movil"),CodMutualista=IN(r,"CodMutualista"),FechaIngMutualista=Dt(r,"FechaIngMutualista"),FechaBajaMutualista=Dt(r,"FechaBajaMutualista"),NroSocioMutualista=S(r,"NroSocioMutualista"),CodRegimenJubilatorio=ByN(r,"CodRegimenJubilatorio"),CodDepartamento=S(r,"CodDepartamento"),PagaMutualista=B(r,"PagaMutualista"),CodSituacionMutual=S(r,"CodSituacionMutual"),CodBanco=IN(r,"CodBanco"),NroCuenta=S(r,"NroCuenta"),NroFunCuenta=S(r,"NroFunCuenta"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<AfiliadoApunte>(c,d,"AfiliadoApunte",r=>new(){CI=LN(r,"CI"),Fecha=Dt(r,"Fecha"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<AfiliadoEspecialidad>(c,d,"AfiliadoEspecialidad",r=>new(){CI=LN(r,"CI"),CodEspecialidad=IN(r,"CodEspecialidad"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Trabaja>(c,d,"Trabaja",r=>new(){IdTrabaja=IN(r,"IdTrabaja"),CI=LN(r,"CI"),CodEmpresa=IN(r,"CodEmpresa"),FechaIngreso=Dt(r,"FechaIngreso"),FechaBaja=Dt(r,"FechaBaja"),CodBajaMotivo=IN(r,"CodBajaMotivo"),NroFichaEmpresa=S(r,"NroFichaEmpresa"),FechaIngCasemed=Dt(r,"FechaIngCasemed"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Cuenta>(c,d,"Cuenta",r=>new(){CI=LN(r,"CI"),CodBanco=IN(r,"CodBanco"),NroCuenta=S(r,"NroCuenta")}); await R<AdPreJub>(c,d,"AdPreJub",r=>new(){CI=LN(r,"CI"),FechaPresentacion=Dt(r,"FechaPresentacion"),ImporteMensual=IN(r,"ImporteMensual"),FechaJubilacion=Dt(r,"FechaJubilacion"),Observaciones=S(r,"Observaciones"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<AdPreJubPago>(c,d,"AdPreJubPago",r=>new(){CI=LN(r,"CI"),Mes=IN(r,"Mes"),Anio=IN(r,"Anio"),Fecha=Dt(r,"Fecha"),Importe=Fl(r,"Importe"),Observaciones=S(r,"Observaciones"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<PrimaFallecimiento>(c,d,"PrimaFallecimiento",r=>new(){CI=LN(r,"CI"),FechaFirma=Dt(r,"FechaFirma"),FechaFallecimiento=Dt(r,"FechaFallecimiento"),Importe=Db(r,"Importe"),FechaPago=Dt(r,"FechaPago"),Observaciones=S(r,"Observaciones"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); }

    static async Task Transactional(string c, MigrationDbContext d) { Hdr("Transactional"); await R<Certificacion>(c,d,"Certificacion",r=>new(){NroLlamado=IN(r,"NroLlamado"),CI=LN(r,"CI"),NroRecibo=IN(r,"NroRecibo"),FechaRecibido=Dt(r,"FechaRecibido"),FechaCertificacion=Dt(r,"FechaCertificacion"),FechaIni=Dt(r,"FechaIni"),FechaFin=Dt(r,"FechaFin"),CodAfeccionTipo=IN(r,"CodAfeccionTipo"),CodCertificador=IN(r,"CodCertificador"),CodSalidaTipo=IN(r,"CodSalidaTipo"),Efectiva=B(r,"Efectiva"),Indicaciones=S(r,"Indicaciones"),ImporteDeducible=Db(r,"ImporteDeducible"),TrabajaDuranteCertificacion=B(r,"Trabaja"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<CertificacionProrroga>(c,d,"CertificacionProrroga",r=>new(){CI=LN(r,"CI"),Fecha=Dt(r,"Fecha"),Dias=IN(r,"Dias"),Observaciones=S(r,"Observaciones"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Prestacion>(c,d,"Prestacion",r=>new(){CI=LN(r,"CI"),Fecha=Dt(r,"Fecha"),CodPrestacionTipo=IN(r,"CodPrestacionTipo"),Moneda=S(r,"Moneda"),Importe=Db(r,"Importe"),Boleta=B(r,"Boleta"),Observaciones=S(r,"Observaciones"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Receta>(c,d,"Receta",r=>new(){CI=LN(r,"CI"),Fecha=Dt(r,"Fecha"),CodPrestacionTipo=IN(r,"CodPrestacionTipo"),CodRecetaDistancia=S(r,"CodRecetaDistancia"),Esf_I=Fl(r,"Esf_I"),Esf_D=Fl(r,"Esf_D"),Cil_I=Fl(r,"Cil_I"),Cil_D=Fl(r,"Cil_D"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<Imponible>(c,d,"Imponible",r=>new(){CI=LN(r,"CI"),CodEmpresa=IN(r,"CodEmpresa"),Fechaingreso=Dt(r,"Fechaingreso"),Mes=ByN(r,"Mes"),Anio=IN(r,"Anio"),Concepto=S(r,"Concepto"),IdTrabaja=IN(r,"IdTrabaja"),DiasTrabajados=IN(r,"DiasTrabajados"),Importe=Db(r,"Importe"),AnioMes=IN(r,"AnioMes"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")},2000); await R<EmpresaPago>(c,d,"EmpresaPago",r=>new(){CodEmpresa=IN(r,"CodEmpresa"),Mes=IN(r,"Mes"),Anio=IN(r,"Anio"),Importe=IN(r,"Importe"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<ReintegroMutual>(c,d,"ReintegroMutual",r=>new(){CI=LN(r,"CI"),Mes=IN(r,"Mes"),Anio=IN(r,"Anio"),Fecha=Dt(r,"Fecha"),CodMutualista=IN(r,"CodMutualista"),Importe=Fl(r,"Importe"),Observaciones=S(r,"Observaciones"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); }

    static async Task Subsidios(string c, MigrationDbContext d) { Hdr("Subsidios"); await R<SubsidioCabezal>(c,d,"SubsidioCabezal",r=>new(){IdSubsidio=I(r,"IdSubsidio"),Mes=ByN(r,"Mes"),Anio=IN(r,"Anio"),CI=LN(r,"CI"),Liquidar=B(r,"Liquidar"),ValorJornal=Fl(r,"ValorJornal"),Dias=IN(r,"Dias"),ImpNominal=Db(r,"ImpNominal"),ImpAguinaldo=Db(r,"ImpAguinaldo"),ImpLiquido=Db(r,"ImpLiquido"),NroRecibo=IN(r,"NroRecibo"),FechaPago=Dt(r,"FechaPago"),CodBanco=IN(r,"CodBanco"),NroCuenta=S(r,"NroCuenta"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")},2000); await R<SubsidioEnfermedad>(c,d,"SubsidioEnfermedad",r=>new(){IdSubsidio=IN(r,"IdSubsidio"),FechaIni=Dt(r,"FechaIni"),FechaFin=Dt(r,"FechaFin"),FechaIniSubsidio=Dt(r,"FechaIniSubsidio"),FechaFinSubsidio=Dt(r,"FechaFinSubsidio"),NroLlamado=IN(r,"NroLlamado"),Dias=ByN(r,"Dias"),Importe=Db(r,"Importe")}); await R<SubsidioItem>(c,d,"SubsidioItem",r=>new(){IdSubsidio=IN(r,"IdSubsidio"),CodSubsidioItemCod=IN(r,"CodSubsidioItemCod"),Importe=Fl(r,"Importe"),AbiEmp=B(r,"AbiEmp")},2000); await R<SubsidioCabezalEmpresa>(c,d,"SubsidioCabezalEmpresa",r=>new(){IdSubsidio=IN(r,"IdSubsidio"),CodEmpresa=IN(r,"CodEmpresa"),ValorJornal=Fl(r,"ValorJornal"),Dias=IN(r,"Dias"),ImpNominal=Db(r,"ImpNominal"),ImpAguinaldo=Db(r,"ImpAguinaldo"),ImpLiquido=Db(r,"ImpLiquido"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<SubsidioCabezalBps>(c,d,"SubsidioCabezal_BPS",r=>new(){IdSubsidio=IN(r,"IdSubsidio"),DiasBPS=IN(r,"DiasBPS"),LiquidoBPS=Db(r,"LiquidoBPS"),AguinaldoBPS=Db(r,"AguinaldoBPS"),LiquidoPagar=Db(r,"LiquidoPagar")}); await R<SubsidioItemCodAfiliado>(c,d,"SubsidioItemCod_Afiliado",r=>new(){SubItmCodAfiId=IN(r,"SubItmCodAfiId"),CodSubsidioItemCod=IN(r,"CodSubsidioItemCod"),CI=LN(r,"CI"),Valor=Db(r,"Valor"),Vigencia=Dt(r,"Vigencia"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await RSubsidioItemEmpresa(c,d); }

    static async Task RSubsidioItemEmpresa(string cs, MigrationDbContext db)
    {
        Console.Write("  SubsidioItemEmpresa... ");
        int n = 0;
        try
        {
            using var cn = new OleDbConnection(cs);
            cn.Open();
            using var cm = new OleDbCommand("SELECT * FROM [SubsidioItemEmpresa]", cn);
            using var rd = cm.ExecuteReader();
            if (rd == null)
            {
                Console.WriteLine("0 OK");
                return;
            }

            const string sql = @"INSERT INTO [SubsidioItemEmpresa] ([IdSubsidio],[CodSubsidioItemCod],[CodEmpresa],[Importe],[Usr],[Ts])
                                 SELECT {0},{1},{2},{3},{4},{5}
                                 WHERE {0} IS NULL OR EXISTS (SELECT 1 FROM [SubsidioCabezal] s WHERE s.[IdSubsidio] = {0})";

            using var tx = await db.Database.BeginTransactionAsync();
            while (rd.Read())
            {
                var idSubsidio = IN(rd, "IdSubsidio");
                var codSubsidioItemCod = IN(rd, "CodSubsidioItemCod");
                var codEmpresa = IN(rd, "CodEmpresa");
                var importe = Fl(rd, "Importe");
                var usr = S(rd, "Usr");
                var ts = Dt(rd, "Ts");

                var inserted = await db.Database.ExecuteSqlRawAsync(sql,
                    idSubsidio ?? (object)DBNull.Value,
                    codSubsidioItemCod ?? (object)DBNull.Value,
                    codEmpresa ?? (object)DBNull.Value,
                    importe ?? (object)DBNull.Value,
                    usr ?? (object)DBNull.Value,
                    ts ?? (object)DBNull.Value);

                if (inserted > 0)
                    n++;
            }

            await tx.CommitAsync();
            TotalRowsImported += n;
            Console.WriteLine($"{n} OK");
        }
        catch (OleDbException ex) { Console.WriteLine($"ERR({n}): {OleDbError(ex)}"); throw; }
        catch (DbUpdateException dbEx) { var inner = dbEx.InnerException?.Message ?? dbEx.Message; Console.WriteLine($"ERR({n}): " + inner); throw; }
        catch (Exception ex) { Console.WriteLine($"ERR({n}): {ex.Message}"); throw; }
    }

    static async Task Shared(string c, MigrationDbContext d) { Hdr("Shared"); await R<Seleccion>(c,d,"Seleccion",r=>new(){IdSeleccion=IN(r,"IdSeleccion") ?? IN(r,"Id"),Form=S(r,"Form"),Nombre=S(r,"Nombre"),Txt=S(r,"Txt"),System=B(r,"System"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); }

    static async Task Prestamos(string c, MigrationDbContext d)
    {
        Hdr("Préstamos");
        await R<SpMoneda>(c,d,"SP_Moneda",r=>new(){CodMoneda=S(r,"CodMoneda"),Descrip=S(r,"Descrip"),Tasa=Fl(r,"Tasa"),TasaMora=Fl(r,"TasaMora"),TasaCambio=Fl(r,"TasaCambio"),CodAbitab=S(r,"CodAbitab"),DescripLarga=S(r,"DescripLarga"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpPrestamoEstado>(c,d,"SP_PrestamoEstado",r=>new(){CodPrestamoEstado=S(r,"CodPrestamoEstado"),Descrip=S(r,"Descrip"),Fin=B(r,"Fin")});
        await R<SpItemPago>(c,d,"SP_ItemPago",r=>new(){CodItemPago=S(r,"CodItemPago"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpFacturaEstado>(c,d,"SP_FacturaEstado",r=>new(){CodFacturaEstado=S(r,"CodFacturaEstado"),Descrip=S(r,"Descrip"),Anulada=B(r,"Anulada")});
        await R<SpCuotaEstado>(c,d,"SP_CuotaEstado",r=>new(){CodCuotaEstado=S(r,"CodCuotaEstado"),Descrip=S(r,"Descrip")});
        await R<SpFacturaTipo>(c,d,"SP_FacturaTipo",r=>new(){CodFacturaTipo=S(r,"CodFacturaTipo"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpPagoOrigen>(c,d,"SP_PagoOrigen",r=>new(){CodPagoOrigen=S(r,"CodPagoOrigen"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpRetencionItemCod>(c,d,"SP_RetencionItemCod",r=>new(){CodRetencionItemCod=S(r,"CodRetencionItemCod"),Descrip=S(r,"Descrip"),TopeaImporte=S(r,"TopeaImporte"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpCtrlPrestamoEstado>(c,d,"SP_CtrlPrestamoEstado",r=>new(){PrestamoEstadoAnt=S(r,"PrestamoEstadoAnt"),PrestamoEstadoSig=S(r,"PrestamoEstadoSig"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpParametro>(c,d,"SP_Parametro",r=>new(){NroEmpresa=S(r,"NroEmpresa"),UR=Fl(r,"UR"),Dolar=Fl(r,"Dolar"),Redondeo=ByN(r,"Redondeo"),TopeUR=Fl(r,"TopeUR"),PctPrestamo=Fl(r,"PctPrestamo"),MesesCalculo=SHN(r,"MesesCalculo"),MaxCuotas=SHN(r,"MaxCuotas"),DiasTolerancia=SHN(r,"DiasTolerancia"),TopeSueldos=Db(r,"TopeSueldos"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpPrestamoTipo>(c,d,"SP_PrestamoTipo",r=>new(){CodPrestamoTipo=S(r,"CodPrestamoTipo"),Descrip=S(r,"Descrip"),TopeaImporte=B(r,"TopeaImporte"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<MapeoAbitab>(c,d,"MapeoAbitab",r=>new(){Inicio=IN(r,"Inicio"),Largo=IN(r,"Largo"),Campo=S(r,"Campo"),CodigoBarra=B(r,"CodigoBarra")});
        await R<ErrCargaAbitab>(c,d,"ErrCargaAbitab",r=>new(){Fecha=Dt(r,"Fecha"),NroReng=IN(r,"NroReng"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpPrestamo>(c,d,"SP_Prestamo",r=>new(){IDPrestamo=I(r,"IDPrestamo"),CI=LN(r,"CI"),Fecha=Dt(r,"Fecha"),CodEmpresa=IN(r,"CodEmpresa"),CodMoneda=S(r,"CodMoneda"),Importe=Fl(r,"Importe"),Cuotas=IN(r,"Cuotas"),ImporteCuota=Fl(r,"ImporteCuota"),CodPrestamoEstado=S(r,"CodPrestamoEstado"),Observaciones=S(r,"Observaciones"),CodPrestamoTipo=S(r,"CodPrestamoTipo"),Tasa=Fl(r,"Tasa"),Saldo=Fl(r,"Saldo"),CuotasPagas=IN(r,"CuotasPagas"),FechaCobro=Dt(r,"FechaCobro"),NroSerieCheque=IN(r,"NroSerieCheque"),NroCheque=IN(r,"NroCheque"),TasaCambio=Fl(r,"TasaCambio"),Promedio=Fl(r,"Promedio"),Banco=S(r,"Banco"),Sucursal=S(r,"Sucursal"),NroCta=S(r,"NroCta"),IDPrestamoRef=IN(r,"IDPrestamoRef"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpFactura>(c,d,"SP_Factura",r=>new(){IDFactura=I(r,"IDFactura"),NroFactura=IN(r,"NroFactura"),NroEmpresa=S(r,"NroEmpresa"),IdPrestamo=IN(r,"IdPrestamo"),FechaEmitida=Dt(r,"FechaEmitida"),FechaVencimiento=Dt(r,"FechaVencimiento"),FechaPago=Dt(r,"FechaPago"),CodMoneda=S(r,"CodMoneda"),Importe=Fl(r,"Importe"),CodFacturaEstado=S(r,"CodFacturaEstado"),TasaCambio=Fl(r,"TasaCambio"),CodigoBarra=S(r,"CodigoBarra"),Impresiones=IN(r,"Impresiones"),ImpAmortizable=Fl(r,"ImpAmortizable"),ImpInteres=Fl(r,"ImpInteres"),CodFacturaTipo=S(r,"CodFacturaTipo"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpFacturaDetalle>(c,d,"SP_FacturaDetalle",r=>new(){IdFactura=IN(r,"IdFactura"),NroReng=IN(r,"NroReng"),CodItemPago=S(r,"CodItemPago"),Descrip=S(r,"Descrip"),NroCuota=IN(r,"NroCuota"),Importe=Fl(r,"Importe"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpCuota>(c,d,"SP_Cuota",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),Nro=IN(r,"Nro"),FechaVencimiento=Dt(r,"FechaVencimiento"),FechaPago=Dt(r,"FechaPago"),CodItemPago=S(r,"CodItemPago"),Importe=Fl(r,"Importe"),CodMoneda=S(r,"CodMoneda"),CodCuotaEstado=S(r,"CodCuotaEstado"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpCuadroAmortizacion>(c,d,"SP_CuadroAmortizacion",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),NroCuota=IN(r,"NroCuota"),Monto=Fl(r,"Monto"),ImporteCuota=Fl(r,"ImporteCuota"),Interes=Fl(r,"Interes"),Amortizacion=Fl(r,"Amortizacion"),Saldo=Fl(r,"Saldo"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpPago>(c,d,"SP_Pago",r=>new(){IDFactura=IN(r,"IDFactura"),Fecha=Dt(r,"Fecha"),Importe=Fl(r,"Importe"),CodSucursal=S(r,"CodSucursal"),TasaCambio=Fl(r,"TasaCambio"),CodPagoOrigen=S(r,"CodPagoOrigen"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpPagoItemPago>(c,d,"SP_Pago_ItemPago",r=>new(){IDFactura=IN(r,"IDFactura"),CodItemPago=S(r,"CodItemPago"),NroCuota=IN(r,"NroCuota"),Importe=Fl(r,"Importe"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpImpLiquido>(c,d,"SP_ImpLiquido",r=>new(){CI=LN(r,"CI"),CodEmpresa=IN(r,"CodEmpresa"),Fechaingreso=Dt(r,"Fechaingreso"),Mes=ByN(r,"Mes"),Anio=IN(r,"Anio"),IdTrabaja=IN(r,"IdTrabaja"),Importe=Db(r,"Importe"),AnioMes=IN(r,"AnioMes"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpRetencionPrestamo>(c,d,"SP_RetencionPrestamo",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),CI=LN(r,"CI"),Fecha=Dt(r,"Fecha"),CodEmpresa=IN(r,"CodEmpresa"),CodMoneda=S(r,"CodMoneda"),Importe=Fl(r,"Importe"),Saldo=Fl(r,"Saldo"),ImpPago=Fl(r,"ImpPago"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpRetencion>(c,d,"SP_Retencion",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),Fecha=Dt(r,"Fecha"),TipoCambio=Fl(r,"TipoCambio"),Importe=Fl(r,"Importe"),Observaciones=S(r,"Observaciones"),Directa=B(r,"Directa"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpRetencionAviso>(c,d,"SP_RetencionAviso",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),Fecha=Dt(r,"Fecha"),Comentario=S(r,"Comentario"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpRetencionItem>(c,d,"SP_RetencionItem",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),Fecha=Dt(r,"Fecha"),IDFactura=IN(r,"IDFactura"),CodRetencionItemCod=S(r,"CodRetencionItemCod"),Importe=Fl(r,"Importe"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpRetencionPago>(c,d,"SP_RetencionPago",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),Fecha=Dt(r,"Fecha"),Mes=IN(r,"Mes"),Anio=IN(r,"Anio"),Importe=Fl(r,"Importe"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpPagoError>(c,d,"SP_PagoError",r=>new(){IDFactura=IN(r,"IDFactura"),Fecha=Dt(r,"Fecha"),Importe=Fl(r,"Importe"),CodSucursal=S(r,"CodSucursal"),TasaCambio=Fl(r,"TasaCambio"),CodFacturaEstado=S(r,"CodFacturaEstado"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpPagoParcial>(c,d,"SP_PagoParcial",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),Fecha=Dt(r,"Fecha"),Importe=Fl(r,"Importe"),TasaCambio=Fl(r,"TasaCambio"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
        await R<SpAfiliadoComentario>(c,d,"SP_AfiliadoComentario",r=>new(){CI=LN(r,"CI"),Fecha=Dt(r,"Fecha"),Observaciones=S(r,"Observaciones"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")});
    }

    static async Task PrestamosLegacy(string c, MigrationDbContext d) { Hdr("Préstamos Legacy"); await R<MapeoAbitab>(c,d,"MapeoAbitab",r=>new(){Inicio=IN(r,"Inicio"),Largo=IN(r,"Largo"),Campo=S(r,"Campo"),CodigoBarra=B(r,"CodigoBarra")}); await R<ErrCargaAbitab>(c,d,"ErrCargaAbitab",r=>new(){Fecha=Dt(r,"Fecha"),NroReng=IN(r,"NroReng"),Descrip=S(r,"Descrip"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<SpPagoError>(c,d,"SP_PagoError",r=>new(){IDFactura=IN(r,"IDFactura"),Fecha=Dt(r,"Fecha"),Importe=Fl(r,"Importe"),CodSucursal=S(r,"CodSucursal"),TasaCambio=Fl(r,"TasaCambio"),CodFacturaEstado=S(r,"CodFacturaEstado"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<SpPagoParcial>(c,d,"SP_PagoParcial",r=>new(){IDPrestamo=IN(r,"IDPrestamo"),Fecha=Dt(r,"Fecha"),Importe=Fl(r,"Importe"),TasaCambio=Fl(r,"TasaCambio"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<SpParametro>(c,d,"SP_Parametro",r=>new(){NroEmpresa=S(r,"NroEmpresa"),UR=Fl(r,"UR"),Dolar=Fl(r,"Dolar"),Redondeo=ByN(r,"Redondeo"),TopeUR=Fl(r,"TopeUR"),PctPrestamo=Fl(r,"PctPrestamo"),MesesCalculo=SHN(r,"MesesCalculo"),MaxCuotas=SHN(r,"MaxCuotas"),DiasTolerancia=SHN(r,"DiasTolerancia"),TopeSueldos=Db(r,"TopeSueldos"),Usr=S(r,"Usr"),Ts=Dt(r,"Ts")}); await R<SpPrestamoTipo>(c,d,"SP_PrestamoTipo",r=>new(){CodPrestamoTipo=S(r,"CodPrestamoTipo"),Descrip=S(r,"Descrip"),TopeaImporte=B(r,"TopeaImporte")}); }

    static void Hdr(string s) => Console.WriteLine($"\n--- {s} ---");

    // Tablas SP_* que en la app SP eran LINKEADAS a la base SGPA (sgpaserv): al consolidar quedan vacías y
    // rompen las queries migradas que las usan (aportes, promedio, etc.). Se reemplazan por VISTAS sobre sus
    // tablas SGPA homónimas para restaurar la paridad sin tocar esas queries. Todas sin FKs (drop seguro).
    static async Task ReplaceLinkedSpTablesWithViewsAsync(MigrationDbContext db)
    {
        Console.Write("Replacing linked SP_* tables with views over SGPA... ");
        await CreateViewOverAsync(db, "SP_Trabaja", "Trabaja",
            "CI, CodEmpresa, FechaIngreso, FechaBaja, CodBajaMotivo, NroFichaEmpresa, IdTrabaja, FechaIngCasemed, Usr, Ts");
        await CreateViewOverAsync(db, "SP_Afiliado", "Afiliado",
            "CI, Nombres, Apellido1, Apellido2, FechaNacimiento, Sexo, Direccion, Telefono, EMail, CodMutualista, " +
            "FechaIngMutualista, FechaBajaMutualista, NroSocioMutualista, CodRegimenJubilatorio, CodDepartamento, " +
            "PagaMutualista, CodSituacionMutual, CodBanco, NroCuenta, NroFunCuenta, Movil, Usr, Ts");
        await CreateViewOverAsync(db, "SP_Empresa", "Empresa",
            "CodEmpresa, Nombre, Direccion, Telefono, Fax, EMail, AporteCasemed, AporteAguinaldo, PersonaContacto, " +
            "Autoridades, CodRegimenAporte, CodSituacionPago, Liquidar, Ficticia, Usr, Ts");
        Console.WriteLine("OK");
    }

    // Tablas de infraestructura de la app Blazor (no provienen de los Access; las usa el front).
    // Idempotente. Hoy: PreferenciaVista (personalización de pantallas por usuario, estilo XAF).
    static async Task CreateInfrastructureTablesAsync(MigrationDbContext db)
    {
        Console.Write("Creating infrastructure tables (PreferenciaVista, TablaConfig, AuditCambio, SgpaFiltro, Reporte, ReporteDinamico, Z_ErrorLog, CampoCalculado + alias)... ");
        await db.Database.ExecuteSqlRawAsync(
            """
            IF OBJECT_ID('dbo.PreferenciaVista','U') IS NULL
            CREATE TABLE dbo.PreferenciaVista
            (
                Login    nvarchar(50)  NOT NULL,
                Vista    nvarchar(200) NOT NULL,
                Json     nvarchar(max) NOT NULL,
                FechaMod datetime2     NOT NULL CONSTRAINT DF_PreferenciaVista_FechaMod DEFAULT SYSDATETIME(),
                CONSTRAINT PK_PreferenciaVista PRIMARY KEY (Login, Vista)
            );
            """);
        await db.Database.ExecuteSqlRawAsync(
            """
            IF OBJECT_ID('dbo.TablaConfig','U') IS NULL
            BEGIN
                CREATE TABLE dbo.TablaConfig
                (
                    Tabla            nvarchar(128) NOT NULL CONSTRAINT PK_TablaConfig PRIMARY KEY,
                    EdicionInline    bit NOT NULL CONSTRAINT DF_TablaConfig_Inline DEFAULT 0,
                    ConfirmarBorrado bit NOT NULL CONSTRAINT DF_TablaConfig_Conf   DEFAULT 1,
                    Auditar          bit NOT NULL CONSTRAINT DF_TablaConfig_Aud    DEFAULT 0,
                    Alias            nvarchar(200) NULL
                );
                INSERT INTO dbo.TablaConfig (Tabla, EdicionInline)
                SELECT v, 1 FROM (VALUES
                    ('Mutualista'),('Banco'),('Certificador'),('Patologia'),('AfeccionGrupo'),('AfeccionTipo'),
                    ('SalidaTipo'),('BajaMotivo'),('FormaPago'),('AporteTipo'),('RegimenAporte'),('RegimenJubilatorio'),
                    ('SituacionPago'),('SituacionMutual'),('Departamento'),('Especialidad'),('PrestacionTipo'),
                    ('RecetaDistancia'),('IMS')
                ) t(v);
            END
            """);
        await db.Database.ExecuteSqlRawAsync(
            """
            IF OBJECT_ID('dbo.AuditCambio','U') IS NULL
            BEGIN
                CREATE TABLE dbo.AuditCambio
                (
                    Id bigint IDENTITY(1,1) NOT NULL CONSTRAINT PK_AuditCambio PRIMARY KEY,
                    Fecha datetime2 NOT NULL CONSTRAINT DF_AuditCambio_Fecha DEFAULT SYSDATETIME(),
                    Login nvarchar(50) NULL, Tabla nvarchar(128) NOT NULL, Clave nvarchar(400) NULL,
                    Operacion char(1) NOT NULL, Campo nvarchar(128) NULL,
                    ValorAnterior nvarchar(max) NULL, ValorNuevo nvarchar(max) NULL
                );
                CREATE INDEX IX_AuditCambio_Tabla_Clave ON dbo.AuditCambio (Tabla, Clave);
                CREATE INDEX IX_AuditCambio_Fecha ON dbo.AuditCambio (Fecha DESC);
            END
            """);
        // Columna DisponibleReportes (la usa la app; idempotente para TablaConfig recién creada).
        await db.Database.ExecuteSqlRawAsync(
            "IF COL_LENGTH('dbo.TablaConfig','DisponibleReportes') IS NULL ALTER TABLE dbo.TablaConfig ADD DisponibleReportes bit NULL;");
        // Filtros guardados de los ListView (+ Parametros = JSON de parámetros pedidos al ejecutar). Idempotente.
        await db.Database.ExecuteSqlRawAsync(
            """
            IF OBJECT_ID('dbo.SgpaFiltro','U') IS NULL
            BEGIN
                CREATE TABLE dbo.SgpaFiltro (
                    Id        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SgpaFiltro PRIMARY KEY,
                    Entity    nvarchar(128) NOT NULL,
                    Nombre    nvarchar(128) NOT NULL,
                    Criteria  nvarchar(max) NOT NULL,
                    EsSistema bit NOT NULL CONSTRAINT DF_SgpaFiltro_Sys DEFAULT(0),
                    Usr       nvarchar(16) NULL,
                    Ts        datetime2 NULL,
                    Parametros nvarchar(max) NULL
                );
                CREATE INDEX IX_SgpaFiltro_Entity ON dbo.SgpaFiltro(Entity);
            END
            IF COL_LENGTH('dbo.SgpaFiltro','Parametros') IS NULL ALTER TABLE dbo.SgpaFiltro ADD Parametros nvarchar(max) NULL;
            """);
        // Reportes dinámicos creados por el administrador (raíz + campos N-1 + filtro con parámetros).
        // Mantener en sintonía con SgpaBlazor/tools/sql/reporte-dinamico.sql. Idempotente.
        await db.Database.ExecuteSqlRawAsync(
            """
            IF OBJECT_ID('dbo.ReporteDinamico','U') IS NULL
            BEGIN
                CREATE TABLE dbo.ReporteDinamico (
                    Id        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ReporteDinamico PRIMARY KEY,
                    Nombre    nvarchar(200) NOT NULL,
                    RootTable nvarchar(128) NOT NULL,
                    DefJson   nvarchar(max) NOT NULL,
                    Activo    bit NOT NULL CONSTRAINT DF_ReporteDinamico_Activo DEFAULT(1),
                    Login     nvarchar(50) NULL,
                    Fecha     datetime2 NOT NULL CONSTRAINT DF_ReporteDinamico_Fecha DEFAULT SYSDATETIME()
                );
                CREATE INDEX IX_ReporteDinamico_RootTable ON dbo.ReporteDinamico(RootTable);
            END
            """);
        // Log de errores no controlados (sink de Serilog + IErrorLog, best-effort). La app sólo INSERTA;
        // si la tabla falta, el logging a base se pierde silenciosamente. Se crea acá para que sea reproducible.
        await db.Database.ExecuteSqlRawAsync(
            """
            IF OBJECT_ID('dbo.Z_ErrorLog','U') IS NULL
            CREATE TABLE dbo.Z_ErrorLog (
                Id      int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Z_ErrorLog PRIMARY KEY,
                Fecha   datetime2 NOT NULL CONSTRAINT DF_Z_ErrorLog_Fecha DEFAULT SYSDATETIME(),
                Login   nvarchar(100) NULL,
                Origen  nvarchar(400) NULL,
                Mensaje nvarchar(2000) NULL,
                Detalle nvarchar(max) NULL
            );
            """);
        // Campos calculados por tabla (expresión CriteriaOperator reutilizable en reportes/filtros/ListViews).
        // Mantener en sintonía con SgpaBlazor/tools/sql/campo-calculado.sql. Idempotente.
        await db.Database.ExecuteSqlRawAsync(
            """
            IF OBJECT_ID('dbo.CampoCalculado','U') IS NULL
            BEGIN
                CREATE TABLE dbo.CampoCalculado (
                    Id            int IDENTITY(1,1) NOT NULL CONSTRAINT PK_CampoCalculado PRIMARY KEY,
                    Tabla         nvarchar(128) NOT NULL,
                    Nombre        nvarchar(128) NOT NULL,
                    Caption       nvarchar(200) NULL,
                    Expr          nvarchar(max) NOT NULL,
                    TipoResultado nvarchar(20) NOT NULL CONSTRAINT DF_CampoCalculado_Tipo DEFAULT('decimal'),
                    DisplayFormat nvarchar(50) NULL,
                    Activo        bit NOT NULL CONSTRAINT DF_CampoCalculado_Activo DEFAULT(1),
                    Usr           nvarchar(16) NULL,
                    Ts            datetime2 NULL
                );
                CREATE UNIQUE INDEX UX_CampoCalculado_Tabla_Nombre ON dbo.CampoCalculado(Tabla, Nombre);
            END
            """);
        // Reportes ad-hoc de DevExpress (REPX, End-User Designer). La app los lee/escribe vía SgpaReportStorage;
        // si la tabla falta, el catálogo de reportes y el menú fallan. Mantener en sintonía con tools/sql/reporte.sql.
        await db.Database.ExecuteSqlRawAsync(
            """
            IF OBJECT_ID('dbo.Reporte','U') IS NULL
            BEGIN
                CREATE TABLE dbo.Reporte (
                    Id        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Reporte PRIMARY KEY,
                    Nombre    nvarchar(200)  NOT NULL,
                    TablaRoot nvarchar(128)  NULL,
                    Layout    varbinary(max) NOT NULL,
                    Fecha     datetime2      NOT NULL CONSTRAINT DF_Reporte_Fecha DEFAULT SYSDATETIME(),
                    Login     nvarchar(50)   NULL
                );
                CREATE INDEX IX_Reporte_TablaRoot ON dbo.Reporte (TablaRoot);
            END
            """);
        // Alias (nombres amigables) por tabla. Mantener en sintonía con SgpaBlazor/tools/sql/tabla-config-alias-seed.sql.
        await db.Database.ExecuteSqlRawAsync(
            """
            MERGE dbo.TablaConfig AS t
            USING (VALUES
                (N'AdPreJub', N'Adelantos pre-jubilatorios'), (N'AdPreJubPago', N'Pagos de adelantos pre-jubilatorios'),
                (N'AfeccionGrupo', N'Grupos de afección'), (N'AfeccionTipo', N'Tipos de afección'),
                (N'Afiliado', N'Afiliados'), (N'AfiliadoApunte', N'Apuntes del afiliado'),
                (N'AfiliadoEspecialidad', N'Especialidades del afiliado'), (N'AporteTipo', N'Tipos de aporte'),
                (N'BajaMotivo', N'Motivos de baja'), (N'Banco', N'Bancos'), (N'Certificacion', N'Certificaciones'),
                (N'CertificacionProrroga', N'Prórrogas de certificación'), (N'Certificador', N'Certificadores'),
                (N'Departamento', N'Departamentos'), (N'Empresa', N'Empresas'), (N'EmpresaPago', N'Pagos de empresa'),
                (N'ErrCargaAbitab', N'Errores de carga Abitab'), (N'Especialidad', N'Especialidades'),
                (N'FormaPago', N'Formas de pago'), (N'FranjaIRPF', N'Franjas de IRPF'), (N'GrupoEtario', N'Grupos etarios'),
                (N'Imponible', N'Imponibles'), (N'InformeEstadistico', N'Informes estadísticos'),
                (N'MapeoAbitab', N'Mapeo de Abitab'), (N'Mutualista', N'Mutualistas'), (N'NoCargadoHL', N'No cargados (HL)'),
                (N'Patologia', N'Patologías'), (N'Prestacion', N'Prestaciones'), (N'PrestacionTipo', N'Tipos de prestación'),
                (N'PrimaFallecimiento', N'Primas por fallecimiento'), (N'Receta', N'Recetas'),
                (N'RecetaDistancia', N'Distancias de receta'), (N'RegimenAporte', N'Regímenes de aporte'),
                (N'RegimenJubilatorio', N'Regímenes jubilatorios'), (N'ReintegroMutual', N'Reintegros mutuales'),
                (N'Reporte', N'Reportes'), (N'ReporteDinamico', N'Reportes dinámicos'),
                (N'CampoCalculado', N'Campos calculados'),
                (N'SalidaTipo', N'Tipos de salida'), (N'SituacionMutual', N'Situaciones mutuales'),
                (N'SituacionPago', N'Situaciones de pago'), (N'SP_AfiliadoComentario', N'Comentarios del afiliado'),
                (N'SP_CtrlPrestamoEstado', N'Control de estados de préstamo'), (N'SP_CuadroAmortizacion', N'Cuadros de amortización'),
                (N'SP_Cuota', N'Cuotas'), (N'SP_CuotaEstado', N'Estados de cuota'), (N'SP_Factura', N'Facturas'),
                (N'SP_FacturaDetalle', N'Detalle de facturas'), (N'SP_FacturaEstado', N'Estados de factura'),
                (N'SP_FacturaTipo', N'Tipos de factura'), (N'SP_ImpLiquido', N'Importes líquidos'), (N'SP_Moneda', N'Monedas'),
                (N'SP_Pago', N'Pagos'), (N'SP_PagoError', N'Errores de pago'), (N'SP_PagoOrigen', N'Orígenes de pago'),
                (N'SP_PagoParcial', N'Pagos parciales'), (N'SP_Prestamo', N'Préstamos'), (N'SP_PrestamoEstado', N'Estados de préstamo'),
                (N'SP_PrestamoTipo', N'Tipos de préstamo'), (N'SP_Retencion', N'Retenciones'),
                (N'SP_RetencionAviso', N'Avisos de retención'), (N'SP_RetencionItem', N'Ítems de retención'),
                (N'SP_RetencionItemCod', N'Códigos de ítem de retención'), (N'SP_RetencionPago', N'Pagos de retención'),
                (N'SP_RetencionPrestamo', N'Retenciones de préstamos'), (N'SubsidioCabezal', N'Cabezales de subsidio'),
                (N'SubsidioCabezal_BPS', N'Cabezales de subsidio (BPS)'), (N'SubsidioCabezalEmpresa', N'Cabezales de subsidio por empresa'),
                (N'SubsidioEnfermedad', N'Subsidios por enfermedad'), (N'SubsidioItem', N'Ítems de subsidio'),
                (N'SubsidioItemCod', N'Códigos de ítem de subsidio'), (N'SubsidioItemCod_Afiliado', N'Códigos de ítem de subsidio por afiliado'),
                (N'SubsidioItemEmpresa', N'Ítems de subsidio por empresa'), (N'Trabaja', N'Empleos')
            ) AS s(Tabla, Alias)
            ON t.Tabla = s.Tabla
            WHEN MATCHED THEN UPDATE SET Alias = s.Alias
            WHEN NOT MATCHED THEN INSERT (Tabla, Alias) VALUES (s.Tabla, s.Alias);
            """);
        Console.WriteLine("OK");
    }

    static async Task CreateViewOverAsync(MigrationDbContext db, string spTable, string sgpaTable, string columns)
    {
        await db.Database.ExecuteSqlRawAsync($"IF OBJECT_ID('dbo.{spTable}','U') IS NOT NULL DROP TABLE dbo.{spTable};");
        await db.Database.ExecuteSqlRawAsync($"IF OBJECT_ID('dbo.{spTable}','V') IS NOT NULL DROP VIEW dbo.{spTable};");
        await db.Database.ExecuteSqlRawAsync($"CREATE VIEW dbo.{spTable} AS SELECT {columns} FROM dbo.{sgpaTable};");
    }

    // Copia VERBATIM toda tabla de Access (no-basura) cuya tabla SQL homónima exista y esté VACÍA,
    // mapeando columnas por nombre (intersección). Garantiza paridad de datos para las tablas que la
    // migración tipada no carga (config, snapshots, staging): xUsrParam, SubsidioImponible, etc.
    // Idempotente: nunca toca tablas ya cargadas (count>0).
    static async Task<int> BackfillEmptyTablesVerbatimAsync(string accessConnStr, string label)
    {
        Hdr($"Backfill verbatim ({label})");
        int totalCopied = 0;

        using var acn = new OleDbConnection(accessConnStr);
        acn.Open();
        var schema = acn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object?[] { null, null, null, "TABLE" });
        var accessTables = schema!.Rows.Cast<DataRow>()
            .Select(r => (string)r["TABLE_NAME"])
            .Where(n => !n.StartsWith("MSys", StringComparison.OrdinalIgnoreCase)
                     && !n.StartsWith("Errores de", StringComparison.OrdinalIgnoreCase)
                     && !n.StartsWith("~", StringComparison.Ordinal))
            .OrderBy(n => n, StringComparer.OrdinalIgnoreCase)
            .ToList();

        using var scn = new SqlConnection(SqlConn);
        scn.Open();

        foreach (var t in accessTables)
        {
            var sqlCols = GetSqlColumns(scn, t);
            if (sqlCols is null) continue; // la tabla no existe en SQL → no aplica

            long sqlCount;
            using (var cc = scn.CreateCommand()) { cc.CommandText = $"SELECT COUNT_BIG(*) FROM [{t}]"; sqlCount = (long)cc.ExecuteScalar(); }
            if (sqlCount > 0) continue; // sólo se backfillean tablas vacías (idempotente)

            var dt = new DataTable();
            using (var da = new OleDbDataAdapter($"SELECT * FROM [{t}]", acn)) da.Fill(dt);
            if (dt.Rows.Count == 0) continue;

            var mapCols = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)
                .Where(c => sqlCols.Contains(c)).ToList();
            if (mapCols.Count == 0) { Console.WriteLine($"  {t}: sin columnas en común, SKIP"); continue; }

            try
            {
                using var bulk = new SqlBulkCopy(scn) { DestinationTableName = $"[{t}]", BulkCopyTimeout = 600, BatchSize = 4000 };
                foreach (var c in mapCols) bulk.ColumnMappings.Add(c, c);
                await bulk.WriteToServerAsync(dt);
                Console.WriteLine($"  {t}: {dt.Rows.Count} filas ({mapCols.Count}/{dt.Columns.Count} cols)");
                totalCopied += dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  {t}: ERROR {ex.Message}");
            }
        }
        Console.WriteLine($"  Total backfill {label}: {totalCopied} filas");
        return totalCopied;
    }

    static HashSet<string>? GetSqlColumns(SqlConnection scn, string table)
    {
        var cols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        using var cmd = scn.CreateCommand();
        cmd.CommandText = "SELECT c.name FROM sys.columns c JOIN sys.tables t ON t.object_id=c.object_id WHERE SCHEMA_NAME(t.schema_id)='dbo' AND t.name=@n";
        cmd.Parameters.AddWithValue("@n", table);
        using var rd = cmd.ExecuteReader();
        while (rd.Read()) cols.Add((string)rd["name"]);
        return cols.Count == 0 ? null : cols;
    }

    sealed class SqlBatchInfo
    {
        public required int Number { get; init; }
        public required int StartLine { get; init; }
        public required string Sql { get; init; }
        public required string Preview { get; init; }
    }

    static async Task<(int AppliedBatches, int FailedBatches, string? FailuresReportPath)> ApplyGeneratedAccessSqlArtifactsAsync(MigrationDbContext db)
    {
        ArgumentNullException.ThrowIfNull(db);

        var projectDir = FindProjectDirectory();
        var outputDir = Path.Combine(projectDir, "Generated", "AccessSql");
        if (!Directory.Exists(outputDir))
            throw new DirectoryNotFoundException($"Generated SQL directory not found: {outputDir}");

        var scriptFiles = new List<string>();
        var bootstrapScriptPath = Path.Combine(outputDir, "_bootstrap_missing_tables.sql");
        if (File.Exists(bootstrapScriptPath))
            scriptFiles.Add(bootstrapScriptPath);

        scriptFiles.AddRange(Directory.GetFiles(outputDir, "*.generated.sql")
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase));

        var appliedBatches = 0;
        var failedBatches = 0;
        var failureDetails = new List<string>();

        foreach (var scriptFilePath in scriptFiles)
        {
            var scriptText = await File.ReadAllTextAsync(scriptFilePath);
            var batches = SplitSqlBatches(scriptText);
            for (var i = 0; i < batches.Count; i++)
            {
                var batchInfo = batches[i];
                var batch = batchInfo.Sql.Trim();
                if (string.IsNullOrWhiteSpace(batch))
                    continue;

                try
                {
                    await db.Database.ExecuteSqlRawAsync(batch);
                    appliedBatches++;
                }
                catch (Exception ex)
                {
                    failedBatches++;
                    var message = ex.InnerException?.Message ?? ex.Message;
                    var normalizedMessage = message.Replace("\r", " ").Replace("\n", " ");
                    failureDetails.Add($"{Path.GetFileName(scriptFilePath)} | batch={batchInfo.Number} | startLine={batchInfo.StartLine} | preview={batchInfo.Preview} | {normalizedMessage}");
                    Console.WriteLine($"\n  WARN SQL artifact batch failed: {Path.GetFileName(scriptFilePath)} #{batchInfo.Number} (line {batchInfo.StartLine}) - {message}");
                }
            }
        }

        string? failuresReportPath = null;
        if (failureDetails.Count > 0)
        {
            failuresReportPath = Path.Combine(outputDir, "_artifact_apply_failures.txt");
            var reportLines = new List<string>
            {
                $"GeneratedAtUtc={DateTime.UtcNow:O}",
                $"AppliedBatches={appliedBatches}",
                $"FailedBatches={failedBatches}",
                string.Empty,
                "Failures:"
            };
            reportLines.AddRange(failureDetails);
            await File.WriteAllLinesAsync(failuresReportPath, reportLines);
        }

        return (appliedBatches, failedBatches, failuresReportPath);
    }

    // Aplica los parches SQL de PostFixes/*.sql (orden alfabético) DESPUÉS de los artefactos generados.
    // Son ALTER idempotentes sobre objetos acc_sgpa_* ya creados. Corre sobre la conexión configurada (sin USE).
    static async Task ApplyPostGenerationFixesAsync(MigrationDbContext db)
    {
        var dir = Path.Combine(FindProjectDirectory(), "PostFixes");
        if (!Directory.Exists(dir))
        {
            Console.WriteLine("OK (no hay PostFixes/)");
            return;
        }

        var files = Directory.GetFiles(dir, "*.sql").OrderBy(p => p, StringComparer.OrdinalIgnoreCase).ToList();
        int applied = 0, failed = 0;
        foreach (var f in files)
        {
            foreach (var batch in SplitSqlBatches(await File.ReadAllTextAsync(f)))
            {
                var sql = batch.Sql.Trim();
                if (string.IsNullOrWhiteSpace(sql))
                    continue;
                try
                {
                    await db.Database.ExecuteSqlRawAsync(sql);
                    applied++;
                }
                catch (Exception ex)
                {
                    failed++;
                    Console.WriteLine($"\n  WARN post-fix {Path.GetFileName(f)} #{batch.Number} (line {batch.StartLine}): {ex.InnerException?.Message ?? ex.Message}");
                }
            }
        }
        Console.WriteLine($"OK ({files.Count} archivo(s), batches applied: {applied}, failed: {failed})\n");
    }

    static List<SqlBatchInfo> SplitSqlBatches(string scriptText)
    {
        var result = new List<SqlBatchInfo>();
        using var reader = new StringReader(scriptText ?? string.Empty);
        var sb = new System.Text.StringBuilder();
        var lineNumber = 0;
        var batchStartLine = 1;
        var batchNumber = 1;

        while (true)
        {
            var line = reader.ReadLine();
            if (line is null)
                break;

            lineNumber++;
            if (Regex.IsMatch(line, @"^\s*GO\s*(?:--.*)?$", RegexOptions.IgnoreCase))
            {
                var sql = sb.ToString();
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    result.Add(new SqlBatchInfo
                    {
                        Number = batchNumber++,
                        StartLine = batchStartLine,
                        Sql = sql,
                        Preview = BuildBatchPreview(sql)
                    });
                }

                sb.Clear();
                batchStartLine = lineNumber + 1;
                continue;
            }

            sb.AppendLine(line);
        }

        var remaining = sb.ToString();
        if (!string.IsNullOrWhiteSpace(remaining))
        {
            result.Add(new SqlBatchInfo
            {
                Number = batchNumber,
                StartLine = batchStartLine,
                Sql = remaining,
                Preview = BuildBatchPreview(remaining)
            });
        }

        return result;
    }

    static string BuildBatchPreview(string sql)
    {
        var firstLine = (sql ?? string.Empty)
            .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x))
            ?? string.Empty;

        if (firstLine.Length > 140)
            firstLine = firstLine[..140] + "...";

        return firstLine;
    }

    static async Task R<T>(string cs, MigrationDbContext db, string tbl, Func<OleDbDataReader,T> map, int bs=500) where T : class
    {
        Console.Write($"  {tbl}... ");
        var entityType = db.Model.FindEntityType(typeof(T));
        if (entityType?.FindPrimaryKey() == null)
        {
            await RNoKey(cs, db, tbl);
            return;
        }

        var ds = db.Set<T>();
        if (typeof(T) == typeof(Seleccion))
        {
            await db.Database.ExecuteSqlRawAsync("DELETE FROM [Seleccion]");
        }
        else if (await ds.AnyAsync())
        {
            Console.WriteLine("SKIP");
            return;
        }
        const int maxWarningDetails = 20;
        var warningDetailsPrinted = 0;
        var warningSuppressed = 0;
        var keyProperties = entityType?.FindPrimaryKey()?.Properties;
        var useIdentityInsert = keyProperties?.Any(p => p.ValueGenerated == ValueGenerated.OnAdd) == true;
        var targetTableName = entityType?.GetTableName() ?? tbl;
        var targetSchema = entityType?.GetSchema();
        var targetTableSql = string.IsNullOrWhiteSpace(targetSchema)
            ? $"[{targetTableName}]"
            : $"[{targetSchema}].[{targetTableName}]";

        HashSet<string>? validSituacionMutual = null;
        HashSet<string>? validDepartamento = null;
        HashSet<int>? validMutualista = null;
        HashSet<byte>? validRegimenJub = null;
        HashSet<int>? validBanco = null;
        HashSet<int>? validSubsidioCabezal = null;
        HashSet<int>? existingSeleccionIds = null;
        HashSet<long>? validAfiliadoCi = null;
        HashSet<int>? validSpFacturaIds = null;

        string FlattenException(Exception ex)
        {
            var messages = new List<string>();
            Exception? current = ex;
            while (current != null)
            {
                if (!string.IsNullOrWhiteSpace(current.Message))
                    messages.Add(current.Message.Replace("\r", " ").Replace("\n", " "));
                current = current.InnerException;
            }
            return string.Join(" | ", messages.Distinct());
        }

        void LogWarn(int rowNumber, string message)
        {
            Console.Write($" W{rowNumber}");
            if (warningDetailsPrinted < maxWarningDetails)
            {
                warningDetailsPrinted++;
                Console.WriteLine($"\n    WARN {tbl} row {rowNumber}: {message}");
            }
            else
            {
                warningSuppressed++;
            }
        }

        async Task SaveBufferAsync(List<T> buffer)
        {
            if (typeof(T) == typeof(Seleccion))
            {
                existingSeleccionIds ??= db.Set<Seleccion>()
                    .Where(x => x.IdSeleccion.HasValue)
                    .Select(x => x.IdSeleccion!.Value)
                    .ToHashSet();

                var cleaned = buffer.Cast<Seleccion>()
                    .Where(x => x.IdSeleccion.HasValue)
                    .GroupBy(x => x.IdSeleccion!.Value)
                    .Select(g => g.First())
                    .Where(x => !existingSeleccionIds.Contains(x.IdSeleccion!.Value))
                    .ToList();

                buffer.Clear();
                buffer.AddRange(cleaned.Cast<T>());

                foreach (var row in cleaned)
                    existingSeleccionIds.Add(row.IdSeleccion!.Value);
            }

            if (buffer.Count == 0)
                return;

            if (typeof(T) == typeof(Afiliado))
            {
                validSituacionMutual ??= db.Set<SituacionMutual>()
                    .Where(x => x.CodSituacionMutual != null)
                    .Select(x => x.CodSituacionMutual!)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);
                validDepartamento ??= db.Set<Departamento>()
                    .Where(x => x.CodDepartamento != null)
                    .Select(x => x.CodDepartamento!)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);
                validMutualista ??= db.Set<Mutualista>().Select(x => x.CodMutualista).ToHashSet();
                validRegimenJub ??= db.Set<RegimenJubilatorio>().Select(x => x.CodRegimenJubilatorio).ToHashSet();
                validBanco ??= db.Set<Banco>().Select(x => x.CodBanco).ToHashSet();

                foreach (var item in buffer.Cast<Afiliado>())
                {
                    item.CodSituacionMutual = string.IsNullOrWhiteSpace(item.CodSituacionMutual)
                        ? null
                        : item.CodSituacionMutual.Trim();
                    if (item.CodSituacionMutual != null && !validSituacionMutual.Contains(item.CodSituacionMutual))
                        item.CodSituacionMutual = null;

                    item.CodDepartamento = string.IsNullOrWhiteSpace(item.CodDepartamento)
                        ? null
                        : item.CodDepartamento.Trim();
                    if (item.CodDepartamento != null && !validDepartamento.Contains(item.CodDepartamento))
                        item.CodDepartamento = null;

                    if (item.CodMutualista.HasValue && !validMutualista.Contains(item.CodMutualista.Value))
                        item.CodMutualista = null;
                    if (item.CodRegimenJubilatorio.HasValue && !validRegimenJub.Contains(item.CodRegimenJubilatorio.Value))
                        item.CodRegimenJubilatorio = null;
                    if (item.CodBanco.HasValue && !validBanco.Contains(item.CodBanco.Value))
                        item.CodBanco = null;
                }
            }

            if (typeof(T) == typeof(SpImpLiquido))
            {
                validAfiliadoCi ??= db.Set<Afiliado>().Select(a => a.CI).ToHashSet();

                var cleaned = buffer.Cast<SpImpLiquido>()
                    .Where(x => !x.CI.HasValue || validAfiliadoCi.Contains(x.CI.Value))
                    .ToList();

                buffer.Clear();
                buffer.AddRange(cleaned.Cast<T>());
            }

            if (typeof(T) == typeof(SpRetencionItem))
            {
                validSpFacturaIds ??= db.Set<SpFactura>().Select(f => f.IDFactura).ToHashSet();

                var cleaned = buffer.Cast<SpRetencionItem>()
                    .Where(x => !x.IDFactura.HasValue || validSpFacturaIds.Contains(x.IDFactura.Value))
                    .ToList();

                buffer.Clear();
                buffer.AddRange(cleaned.Cast<T>());
            }

            db.ChangeTracker.Clear();
            await ds.AddRangeAsync(buffer);
            using var tx = await db.Database.BeginTransactionAsync();
            try
            {
                if (useIdentityInsert)
                    await db.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {targetTableSql} ON");

                await db.SaveChangesAsync();

                if (useIdentityInsert)
                    await db.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {targetTableSql} OFF");

                await tx.CommitAsync();
            }
            catch
            {
                if (useIdentityInsert)
                {
                    try { await db.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {targetTableSql} OFF"); } catch { }
                }
                throw;
            }
        }

        async Task PersistBufferWithFallbackAsync(List<T> buffer, int lastReadRow)
        {
            if (buffer.Count == 0)
                return;

            var firstRow = lastReadRow - buffer.Count + 1;
            try
            {
                await SaveBufferAsync(buffer);
            }
            catch (Exception batchEx)
            {
                for (var i = 0; i < buffer.Count; i++)
                {
                    var rowNumber = firstRow + i;
                    try
                    {
                        var single = new List<T>(1) { buffer[i] };
                        await SaveBufferAsync(single);
                    }
                    catch (Exception rowEx)
                    {
                        throw new InvalidOperationException($"{tbl}: error persisting row {rowNumber}. {FlattenException(rowEx)}", rowEx);
                    }
                }
            }
        }

        int n = 0;
        try
        {
            using var cn = new OleDbConnection(cs);
            cn.Open();
            using var cm = new OleDbCommand($"SELECT * FROM [{tbl}]", cn);
            using var rd = cm.ExecuteReader();
            var buf = new List<T>(bs);
            while (rd!.Read())
            {
                try
                {
                    buf.Add(map(rd)); n++;
                    if (buf.Count >= bs)
                    {
                        await PersistBufferWithFallbackAsync(buf, n);
                        buf.Clear();
                        Console.Write($"\r  {tbl}... {n}");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"{tbl}: error mapping row {n}. {FlattenException(ex)}", ex);
                }
            }
            if (buf.Count > 0)
            {
                await PersistBufferWithFallbackAsync(buf, n);
            }
            TotalRowsImported += n;
            if (warningSuppressed > 0)
                Console.Write($" [suppressed {warningSuppressed} warnings]");
            Console.WriteLine($"\r  {tbl}... {n} OK       ");
        }
        catch (OleDbException ex) { Console.WriteLine($"ERR({n}): {OleDbError(ex)}"); throw; }
        catch (DbUpdateException dbEx)
        {
            var inner = dbEx.InnerException?.Message ?? dbEx.Message;
            if (tbl == "Empresa" && inner.Contains("FK_Empresa_RegimenAporte_CodRegimenAporte", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"DEFERRED({n}): {inner}");
                return;
            }

            Console.WriteLine($"ERR({n}): " + inner);
            throw;
        }
        catch (Exception ex)
        {
            if (tbl == "Empresa" && ex.Message.Contains("FK_Empresa_RegimenAporte_CodRegimenAporte", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"DEFERRED({n}): {ex.Message}");
                return;
            }

            Console.WriteLine($"ERR({n}): {ex.Message}");
            throw;
        }
    }

    static async Task RNoKey(string cs, MigrationDbContext db, string tbl)
    {
        Console.Write($"  {tbl}... ");
        int n = 0;
        try
        {
            using var cn = new OleDbConnection(cs);
            cn.Open();
            using var cm = new OleDbCommand($"SELECT * FROM [{tbl}]", cn);
            using var rd = cm.ExecuteReader();
            if (rd == null)
            {
                Console.WriteLine("0 OK");
                return;
            }

            var fieldCount = rd.FieldCount;
            var targetColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            await using (var dbCmd = db.Database.GetDbConnection().CreateCommand())
            {
                if (dbCmd.Connection?.State != ConnectionState.Open)
                    await dbCmd.Connection!.OpenAsync();

                dbCmd.CommandText = @"SELECT c.name
FROM sys.columns c
INNER JOIN sys.objects o ON c.object_id = o.object_id
INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
WHERE o.type = 'U' AND s.name = 'dbo' AND o.name = @tbl";
                var p = dbCmd.CreateParameter();
                p.ParameterName = "@tbl";
                p.Value = tbl;
                dbCmd.Parameters.Add(p);

                await using var dbRd = await dbCmd.ExecuteReaderAsync();
                while (await dbRd.ReadAsync())
                {
                    targetColumns.Add(dbRd.GetString(0));
                }
            }

            var sourceOrdinalAndName = Enumerable.Range(0, fieldCount)
                .Select(i => new { Ordinal = i, Name = rd.GetName(i) })
                .Where(x => targetColumns.Contains(x.Name))
                .ToList();

            if (sourceOrdinalAndName.Count == 0)
            {
                Console.WriteLine("0 OK");
                return;
            }

            var columnList = string.Join(",", sourceOrdinalAndName.Select(x => $"[{x.Name}]"));
            var valuePlaceholders = string.Join(",", Enumerable.Range(0, sourceOrdinalAndName.Count).Select(i => "{" + i + "}"));
            var insertSql = $"INSERT INTO [{tbl}] ({columnList}) VALUES ({valuePlaceholders})";

            using var tx = await db.Database.BeginTransactionAsync();
            while (rd.Read())
            {
                var values = new object[sourceOrdinalAndName.Count];
                for (var i = 0; i < sourceOrdinalAndName.Count; i++)
                {
                    values[i] = rd.GetValue(sourceOrdinalAndName[i].Ordinal);
                }
                for (var i = 0; i < values.Length; i++)
                {
                    if (values[i] == DBNull.Value)
                        values[i] = null!;
                }

                await db.Database.ExecuteSqlRawAsync(insertSql, values);
                n++;
            }
            await tx.CommitAsync();
            TotalRowsImported += n;
            Console.WriteLine($"{n} OK");
        }
        catch (OleDbException ex) { Console.WriteLine($"ERR({n}): {OleDbError(ex)}"); throw; }
        catch (DbUpdateException dbEx) { var inner = dbEx.InnerException?.Message ?? dbEx.Message; Console.WriteLine($"ERR({n}): " + inner); throw; }
        catch (Exception ex) { Console.WriteLine($"ERR({n}): {ex.Message}"); throw; }
    }

    static string? S(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?null:r.GetString(i).Trim();}catch{return null;}}
    static int I(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?0:Convert.ToInt32(r.GetValue(i));}catch{return 0;}}
    static int? IN(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?null:Convert.ToInt32(r.GetValue(i));}catch{return null;}}
    static long L(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?0:Convert.ToInt64(r.GetValue(i));}catch{return 0;}}
    static long? LN(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?null:Convert.ToInt64(r.GetValue(i));}catch{return null;}}
    static double? Db(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?null:Convert.ToDouble(r.GetValue(i));}catch{return null;}}
    static float? Fl(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?null:Convert.ToSingle(r.GetValue(i));}catch{return null;}}
    static byte By(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?(byte)0:Convert.ToByte(r.GetValue(i));}catch{return 0;}}
    static byte? ByN(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?null:Convert.ToByte(r.GetValue(i));}catch{return null;}}
    static short? SHN(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?null:Convert.ToInt16(r.GetValue(i));}catch{return null;}}
    static bool B(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return!r.IsDBNull(i)&&Convert.ToBoolean(r.GetValue(i));}catch{return false;}}
    static DateTime? Dt(OleDbDataReader r,string c){try{int i=r.GetOrdinal(c);return r.IsDBNull(i)?null:Convert.ToDateTime(r.GetValue(i));}catch{return null;}}

    static void GenerateAccessQueriesSqlArtifacts()
    {
        var projectDir = FindProjectDirectory();
        var accessSpecsDir = Path.GetFullPath(Path.Combine(projectDir, "..", "..", "Migration", "Access"));
        var outputDir = Path.Combine(projectDir, "Generated", "AccessSql");
        Directory.CreateDirectory(outputDir);

        var specFiles = Directory.GetFiles(accessSpecsDir, "*.mdb-specs.txt", SearchOption.TopDirectoryOnly)
            .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var allQueries = new List<AccessQueryDefinition>();

        foreach (var spec in specFiles)
        {
            var parsed = AccessQueryParser.ParseFile(spec);
            var initialPlan = AccessQueryDependencyPlanner.BuildPlan(parsed);
            var effectiveQueries = BuildQueriesWithPropagatedParameters(parsed, initialPlan);
            allQueries.AddRange(effectiveQueries);

            var plan = AccessQueryDependencyPlanner.BuildPlan(effectiveQueries);

            var dbName = Path.GetFileName(spec)
                .Replace(".mdb-specs.txt", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty);

            var outSql = Path.Combine(outputDir, $"{dbName}.generated.sql");
            var scriptBuilder = new System.Text.StringBuilder();

            scriptBuilder.AppendLine($"-- Auto-generated from {Path.GetFileName(spec)}");
            scriptBuilder.AppendLine($"-- Query count: {effectiveQueries.Count}");
            scriptBuilder.AppendLine($"-- Ordered by dependencies (nested queries first)");
            if (plan.CycleQueryNames.Count > 0)
            {
                scriptBuilder.AppendLine($"-- WARNING: Cycles detected: {string.Join(", ", plan.CycleQueryNames)}");
            }

            scriptBuilder.AppendLine();

            // 1) Data objects for dependency chain (views/iTVFs for SELECT queries).
            foreach (var item in plan.OrderedItems)
            {
                var dataObjectSql = AccessToSqlTranslator.BuildDataObjectSql(item.Query);
                if (string.IsNullOrWhiteSpace(dataObjectSql))
                    continue;

                scriptBuilder.AppendLine($"-- ===== DATA OBJECT FOR QUERY: {item.Query.Name} =====");
                if (item.Dependencies.Count > 0)
                    scriptBuilder.AppendLine($"-- DependsOn: {string.Join(", ", item.Dependencies)}");
                scriptBuilder.AppendLine(dataObjectSql);
                scriptBuilder.AppendLine("GO");
                scriptBuilder.AppendLine();
            }

            // 1b) Compatibility objects for original Access query names (no renaming) if missing.
            foreach (var item in plan.OrderedItems)
            {
                var compatSql = AccessToSqlTranslator.BuildCompatibilityObjectSql(item.Query);
                if (string.IsNullOrWhiteSpace(compatSql))
                    continue;

                scriptBuilder.AppendLine($"-- ===== COMPAT OBJECT FOR QUERY: {item.Query.Name} =====");
                scriptBuilder.AppendLine(compatSql);
                scriptBuilder.AppendLine("GO");
                scriptBuilder.AppendLine();
            }

            // 2) Wrapper stored procedures to preserve Access/VB6 invocation style.
            foreach (var item in plan.OrderedItems)
            {
                scriptBuilder.AppendLine($"-- ===== WRAPPER PROCEDURE FOR QUERY: {item.Query.Name} =====");
                if (item.Dependencies.Count > 0)
                    scriptBuilder.AppendLine($"-- DependsOn: {string.Join(", ", item.Dependencies)}");
                scriptBuilder.AppendLine(AccessToSqlTranslator.BuildStoredProcedureSql(item.Query));
                scriptBuilder.AppendLine("GO");
                scriptBuilder.AppendLine();
            }

            var finalScript = AccessToSqlTranslator.NormalizeFinalScript(scriptBuilder.ToString());
            finalScript = AccessToSqlTranslator.ApplyDataObjectReferenceRewrites(finalScript, Path.GetFileName(spec), effectiveQueries);
            finalScript = AccessToSqlTranslator.ApplyDataObjectReferenceRewrites(finalScript, Path.GetFileName(spec), effectiveQueries);
            finalScript = finalScript.Replace("q_q_400_Suma_Importe.", "acc_sgpa_400_Suma_Importe_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_400_Suma_Puestos.", "acc_sgpa_400_Suma_Puestos_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_460_Imponible.", "acc_sgpa_460_Imponible_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_480_Certificacion.", "acc_sgpa_480_Certificacion_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_480_F_Ult_Certif.", "acc_sgpa_480_F_Ult_Certif_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_480_Promedio.", "acc_sgpa_480_Promedio_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_480_SumaProrrogas.", "acc_sgpa_480_SumaProrrogas_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_480_Prorrogas.", "acc_sgpa_480_Prorrogas_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_490_Subsidio.", "acc_sgpa_490_Subsidio_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_500_Prorrogas.", "acc_sgpa_500_Prorrogas_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_500_Rpt_Certificacion_UltFecha.", "acc_sgpa_500_Rpt_Certificacion_UltFecha_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_500_Rpt_CertificadoEmpresa_S.", "acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_500_Rpt_ResumenSubsidio.", "acc_sgpa_500_Rpt_ResumenSubsidio_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("500_Rpt_NoPagarMutualista.", "acc_sgpa_500_Rpt_NoPagarMutualista_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_506_Rpt_Subsidio.", "acc_sgpa_506_Rpt_Subsidio_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_812_AfiliadoActivoEspecialidad.", "acc_sgpa_812_AfiliadoActivoEspecialidad_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_400_Promedio_Mes.", "acc_sgpa_400_Promedio_Mes_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_400_Promedio_Mes_Puesto.", "acc_sgpa_400_Promedio_Mes_Puesto_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_1000_AfiliadoImpLiquidoxMes.", "acc_sp_1000_AfiliadoImpLiquidoxMes_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_1009_MinFechaVencimiento.", "acc_sp_1009_MinFechaVencimiento_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_1011_Prestamo_MinFechaVencimiento.", "acc_sp_1011_Prestamo_MinFechaVencimiento_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_1120_Retencion_Amort.", "acc_sp_1120_Retencion_Amort_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_1120_Retencion_Tel.", "acc_sp_1120_Retencion_Tel_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_1030_FlujoTIR.", "acc_sp_1030_FlujoTIR_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("xw_FacturasCantidadXPrestamo.", "acc_sp_xw_FacturasCantidadXPrestamo_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_PrestamoPctRetenciones.", "acc_sp_Rs_PrestamoPctRetenciones_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("1025_PagoParcialFromPrestamo.", "acc_sp_1025_PagoParcialFromPrestamo_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("1030_FacturaFlujo", "acc_sp_1030_FacturaFlujo_q", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_PrestamoActivo.", "acc_sp_Rs_PrestamoActivo_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("300_SubsidioItemCod_Full_Data", "acc_sgpa_300_SubsidioItemCod_Full_Data_q", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [480_Certificacion]", "FROM [acc_sgpa_480_Certificacion_q] AS acc_sgpa_480_Certificacion_q", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("([480_Certificacion] INNER JOIN Afiliado ON acc_sgpa_480_Certificacion_q.CI = Afiliado.CI)", "([acc_sgpa_480_Certificacion_q] AS acc_sgpa_480_Certificacion_q INNER JOIN Afiliado ON acc_sgpa_480_Certificacion_q.CI = Afiliado.CI)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("200_PagarMutualista.", "acc_sgpa_200_PagarMutualista_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("[200_PagarMutualista].", "acc_sgpa_200_PagarMutualista_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [200_PagarMutualista]", "FROM [acc_sgpa_200_PagarMutualista_q](@pMes, @pMesIni, @pSMN)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("([200_PagarMutualista] INNER JOIN", "([acc_sgpa_200_PagarMutualista_q](@pMes, @pMesIni, @pSMN) AS acc_sgpa_200_PagarMutualista_q INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_Bps2.", "acc_sgpa_Rs_Bps2_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rpt_NoPagarMutualista.", "acc_sgpa_Rpt_NoPagarMutualista_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM Rpt_NoPagarMutualista", "FROM [acc_sgpa_Rpt_NoPagarMutualista_q]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_Rpt_NoPagarMutualista_q]", "FROM [acc_sgpa_Rpt_NoPagarMutualista_q](@pMes, @pMesIni, @pSMN)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("LEN([Rpt_NoPagarMutualista].[CI])", "LEN(acc_sgpa_Rpt_NoPagarMutualista_q.[CI])", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FORMAT([Rpt_NoPagarMutualista].[CI]", "FORMAT(acc_sgpa_Rpt_NoPagarMutualista_q.[CI]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, CodEmpresa, Fechaingreso, Mes, Anio, Concepto, IdTrabaja, DiasTrabajados, Importe, Usr, Ts, AnioMes )", "INSERT INTO Imponible ( CI, CodEmpresa, Fechaingreso, Mes, Anio, Concepto, IdTrabaja, DiasTrabajados, Importe, Usr, Ts, AnioMes )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( IdSubsidio, Mes, Anio, CodEmpresa, Dias, Importe, Usr, Ts )", "INSERT INTO SubsidioImponible ( IdSubsidio, Mes, Anio, CodEmpresa, Dias, Importe, Usr, Ts )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( IdSubsidio, Dias, CodEmpresa, Importe, Mes, Anio, Usr, Ts )", "INSERT INTO SubsidioImponible ( IdSubsidio, Dias, CodEmpresa, Importe, Mes, Anio, Usr, Ts )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, Apellido1, Apellido2, Nombres, CodEmpresa, Mes, Anio, Usr, Ts )", "INSERT INTO NoCargadoHL ( CI, Apellido1, Apellido2, Nombres, CodEmpresa, Mes, Anio, Usr, Ts )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, DiasTrabajados, CodEmpresa, Importe, Anio, Mes, IdTrabaja, Fechaingreso, Concepto, AnioMes )", "INSERT INTO Imponible ( CI, DiasTrabajados, CodEmpresa, Importe, Anio, Mes, IdTrabaja, Fechaingreso, Concepto, AnioMes )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, Concepto, Importe, CodEmpresa, Mes, Anio, DiasTrabajados, Usr, Ts, Fechaingreso, IdTrabaja, AnioMes )", "INSERT INTO Imponible ( CI, Concepto, Importe, CodEmpresa, Mes, Anio, DiasTrabajados, Usr, Ts, Fechaingreso, IdTrabaja, AnioMes )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, Concepto, Importe, CodEmpresa, Mes, Anio, Fechaingreso, IdTrabaja, DiasTrabajados, Usr, Ts, AnioMes )", "INSERT INTO Imponible ( CI, Concepto, Importe, CodEmpresa, Mes, Anio, Fechaingreso, IdTrabaja, DiasTrabajados, Usr, Ts, AnioMes )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( login, clave, orden, value1, value2, value3, value4, value5 )", "INSERT INTO xUsrParam ( login, clave, orden, value1, value2, value3, value4, value5 )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, CodEmpresa, Fechaingreso, IdTrabaja, Mes, Anio, AnioMes, Importe, Usr, Ts )", "INSERT INTO SP_ImpLiquido ( CI, CodEmpresa, Fechaingreso, IdTrabaja, Mes, Anio, AnioMes, Importe, Usr, Ts )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, CodEmpresa, Fechaingreso, Mes, Anio, Usr, Ts, IdTrabaja, Importe, AnioMes )", "INSERT INTO SP_ImpLiquido ( CI, CodEmpresa, Fechaingreso, Mes, Anio, Usr, Ts, IdTrabaja, Importe, AnioMes )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CodEmpresa, Fechaingreso, CI, Importe, IdTrabaja, Mes, Anio, AnioMes, Usr, Ts )", "INSERT INTO SP_ImpLiquido ( CodEmpresa, Fechaingreso, CI, Importe, IdTrabaja, Mes, Anio, AnioMes, Usr, Ts )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, Mes, Anio, CodEmpresa, Fechaingreso, IdTrabaja, Importe, AnioMes, Usr, Ts )", "INSERT INTO SP_ImpLiquido ( CI, Mes, Anio, CodEmpresa, Fechaingreso, IdTrabaja, Importe, AnioMes, Usr, Ts )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("DELETE FROM SP_ImpLiquido INNER JOIN SP_Trabaja ON", "DELETE SP_ImpLiquido FROM SP_ImpLiquido INNER JOIN SP_Trabaja ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("TRY_CONVERT(float,SUBSTRING(CONVERT(nvarchar(max),@pAnioMes),5)) AS Expr1", "TRY_CONVERT(float,SUBSTRING(CONVERT(nvarchar(max),@pAnioMes),5,2)) AS Expr1", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE (((SP_Trabaja.CI)=@pCI) AND ((SP_Trabaja.CodEmpresa)=@pCodEmpresa) AND ((SP_Trabaja.FechaBaja) Is Null) AND (((YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso])))<=@pAnioMes)) OR (((SP_Trabaja.CI)=@pCI) AND ((SP_Trabaja.CodEmpresa)=@pCodEmpresa) AND (((YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso])))<=@pAnioMes) AND (((YEAR([SP_Trabaja].[FechaBaja]) * 100 + MONTH([SP_Trabaja].[FechaBaja])))>@pAnioMes));", "WHERE (SP_Trabaja.CI=@pCI AND SP_Trabaja.CodEmpresa=@pCodEmpresa AND (YEAR(SP_Trabaja.FechaIngreso) * 100 + MONTH(SP_Trabaja.FechaIngreso))<=@pAnioMes AND (SP_Trabaja.FechaBaja IS NULL OR (YEAR(SP_Trabaja.FechaBaja) * 100 + MONTH(SP_Trabaja.FechaBaja))>@pAnioMes));", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("SELECT SP_FacturaDetalle.*, SP_FacturaDetalle.CodItemPago", "SELECT SP_FacturaDetalle.*", StringComparison.OrdinalIgnoreCase);
            // NOTA: SP_Prestamo ya incluye las columnas Tasa/Saldo/Promedio/CodPrestamoTipo/FechaCobro/
            // IDPrestamoRef/etc. (ver business object SpPrestamo + tools/sql/sp-prestamo-extend.sql del
            // proyecto Blazor). Por eso se eliminaron los workarounds que las anulaban (0/NULL) y quitaban
            // el join a SP_PrestamoTipo y el self-join por IDPrestamoRef: las queries de reporte ahora usan
            // las columnas reales.
            finalScript = finalScript.Replace("FROM [acc_sp_Rpt_Factura_q]", "FROM [acc_sp_Rpt_Factura_q] AS Rpt_Factura", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sp_Rpt_Factura_DBG_q]", "FROM [acc_sp_Rpt_Factura_DBG_q] AS Rpt_Factura", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sp_Rpt_PrestamoCuadro_q]", "FROM [acc_sp_Rpt_PrestamoCuadro_q] AS Rpt_PrestamoCuadro", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sp_Rs_AfiliadoComentario_q]", "FROM [acc_sp_Rs_AfiliadoComentario_q] AS Rs_AfiliadoComentario", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sp_wRetencionDetalle_q]", "FROM [acc_sp_wRetencionDetalle_q] AS wRetencionDetalle", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_spserv_envios_q] LEFT JOIN [acc_spserv_Cobros_q] ON", "FROM [acc_spserv_envios_q] AS envios LEFT JOIN [acc_spserv_Cobros_q] AS Cobros ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("(CASE WHEN abs(envios.Importe-cobros)<1 THEN 0 ELSE envios.Importe-cobros END) AS saldo", "(CASE WHEN ABS(envios.Importe-ISNULL(Cobros.Importe, 0))<1 THEN 0 ELSE envios.Importe-ISNULL(Cobros.Importe, 0) END) AS saldo", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("TRY_CONVERT(float,[Rpt_Factura].[Impresiones] + '') + 1", "ISNULL(SP_Factura.Impresiones, 0) + 1", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_spserv_rsFacturaEstado_q]", "FROM [acc_spserv_rsFacturaEstado_q] AS rsFacturaEstado", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("q_q_1015_CargaLiquidos.", "acc_sp_1015_CargaLiquidos_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM SP_Trabaja INNER JOIN [acc_sp_1015_CargaLiquidos_q] ON", "FROM SP_Trabaja INNER JOIN [acc_sp_1015_CargaLiquidos_q] AS acc_sp_1015_CargaLiquidos_q ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_PrestamoInteresAmortizacion.", "acc_sp_Rs_PrestamoInteresAmortizacion_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("ON SP_Prestamo.IDPrestamo = Rs_PrestamoInteresAmortizacion.IDPrestamo", "ON SP_Prestamo.IDPrestamo = acc_sp_Rs_PrestamoInteresAmortizacion_q.IDPrestamo", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("(Rs_PrestamoActivo INNER JOIN", "([acc_sp_Rs_PrestamoActivo_q] AS acc_sp_Rs_PrestamoActivo_q INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sp_Rs_PrestamoActivo_q] INNER JOIN", "FROM [acc_sp_Rs_PrestamoActivo_q] AS acc_sp_Rs_PrestamoActivo_q INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace(" INNER JOIN SP_PrestamoTipo ON acc_sp_Rs_PrestamoActivo_q.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo", "", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace(" AND ((SP_PrestamoTipo.TopeaImporte)= 1)", "", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("[SP_Factura].[", "[Rpt_Factura].[", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("[Rpt_Factura].[Impresiones]", "SP_Factura.Impresiones", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rpt_Factura.Impresiones", "SP_Factura.Impresiones", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rpt_Factura.SP_FacturaDetalle.Importe", "Rpt_Factura.Importe", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rpt_Factura.SP_Factura.Importe", "Rpt_Factura.Importe", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace(", Rpt_Factura.CodigoBarra, SP_Factura.Impresiones", ", Rpt_Factura.CodigoBarra, Rpt_Factura.Impresiones", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rpt_Factura.CodFacturaEstado, SP_Factura.Impresiones", "Rpt_Factura.CodFacturaEstado, Rpt_Factura.Impresiones", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("TRY_CONVERT(float,SP_Factura.Impresiones + '') + 1", "ISNULL(SP_Factura.Impresiones, 0) + 1", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("CInt(Sum(wRetencionDetalle.Importe))", "TRY_CONVERT(int, Sum(wRetencionDetalle.Importe))", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("MIN(P.FechaCobro) AS Fecha", "MIN(P.Fecha) AS Fecha", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("MIN(P.Tasa) AS Tasa", "0 AS Tasa", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("(Cant_Fac_Ret)/Cant_Facturas AS Pct_Retenidas", "CASE WHEN Count(*)=0 THEN 0 ELSE Sum((CASE WHEN F.CodFacturaEstado='ret' THEN 1 ELSE 0 END))*1.0/Count(*) END AS Pct_Retenidas", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("UPDATE SP_Prestamo SET SP_Prestamo.CuotasPagas = SP_Prestamo.CuotasPagas + 1, SP_Prestamo.Saldo = SP_Prestamo.Saldo - @pImporte", "IF COL_LENGTH('dbo.SP_Prestamo','CuotasPagas') IS NOT NULL AND COL_LENGTH('dbo.SP_Prestamo','Saldo') IS NOT NULL\r\n    BEGIN\r\n        UPDATE SP_Prestamo SET SP_Prestamo.CuotasPagas = SP_Prestamo.CuotasPagas + 1, SP_Prestamo.Saldo = SP_Prestamo.Saldo - @pImporte\r\n        WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp;\r\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("    END\r\n    WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));", "    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("    END\n    WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));", "    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("IF COL_LENGTH('dbo.SP_Prestamo','CuotasPagas') IS NOT NULL AND COL_LENGTH('dbo.SP_Prestamo','Saldo') IS NOT NULL\r\n    BEGIN\r\n        UPDATE SP_Prestamo SET SP_Prestamo.CuotasPagas = SP_Prestamo.CuotasPagas + 1, SP_Prestamo.Saldo = SP_Prestamo.Saldo - @pImporte\r\n        WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp;\r\n    END", "IF COL_LENGTH('dbo.SP_Prestamo','CuotasPagas') IS NOT NULL AND COL_LENGTH('dbo.SP_Prestamo','Saldo') IS NOT NULL\r\n    BEGIN\r\n        EXEC sp_executesql N'UPDATE SP_Prestamo SET CuotasPagas = CuotasPagas + 1, Saldo = Saldo - @pImporte WHERE IdPrestamo = @pIdPrestamo', N'@pImporte NVARCHAR(MAX), @pIdPrestamo INT', @pImporte=@pImporte, @pIdPrestamo=@pIdPrestamo;\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp;\r\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("IF COL_LENGTH('dbo.SP_Prestamo','CuotasPagas') IS NOT NULL AND COL_LENGTH('dbo.SP_Prestamo','Saldo') IS NOT NULL\n    BEGIN\n        UPDATE SP_Prestamo SET SP_Prestamo.CuotasPagas = SP_Prestamo.CuotasPagas + 1, SP_Prestamo.Saldo = SP_Prestamo.Saldo - @pImporte\n        WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));\n    END\n    ELSE\n    BEGIN\n        SELECT 1 AS NoOp;\n    END", "IF COL_LENGTH('dbo.SP_Prestamo','CuotasPagas') IS NOT NULL AND COL_LENGTH('dbo.SP_Prestamo','Saldo') IS NOT NULL\n    BEGIN\n        EXEC sp_executesql N'UPDATE SP_Prestamo SET CuotasPagas = CuotasPagas + 1, Saldo = Saldo - @pImporte WHERE IdPrestamo = @pIdPrestamo', N'@pImporte NVARCHAR(MAX), @pIdPrestamo INT', @pImporte=@pImporte, @pIdPrestamo=@pIdPrestamo;\n    END\n    ELSE\n    BEGIN\n        SELECT 1 AS NoOp;\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("SELECT 1 AS NoOp\r\n    WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));", "SELECT 1 AS NoOp;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("SELECT 1 AS NoOp\n    WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));", "SELECT 1 AS NoOp;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("[ImpFactura]", "SP_Factura.Importe", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Sum(C.IMPORTE) AS Importe", "Sum(C.LIQUIDO) AS Importe", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("(CASE WHEN @pFechaFin < (CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END) THEN @pFechaFin ELSE (CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END) END) + 1", "DATEADD(day, 1, (CASE WHEN @pFechaFin < (CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END) THEN @pFechaFin ELSE (CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END) END))", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, Importe, Mes, Anio, Usr, Ts )", "INSERT INTO AdPreJubPago ( CI, Importe, Mes, Anio, Usr, Ts )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("IIF((CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN 1 ELSE 0 END), @pFechaFin, AdPreJub.FechaJubilacion)", "(CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("END) + 1)  / (DATEDIFF(day, @pFechaIni, @pFechaFin) + 1))", "END))  / NULLIF((DATEDIFF(day, @pFechaIni, @pFechaFin) + 1), 0))", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_300_SubsidioItemCod_Full_Data_q]", "FROM [acc_sgpa_300_SubsidioItemCod_Full_Data_q](@pFecha, @pCI)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [807_CertificadosEspecialidad]", "FROM [807_CertificadosEspecialidad](NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [808_CertificadosAfecciones]", "FROM [808_CertificadosAfecciones](NULL, NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [818_Certificados_Patologia]", "FROM [818_Certificados_Patologia](NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [819_Certificados_AfeccionGrupo]", "FROM [819_Certificados_AfeccionGrupo](NULL, NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [820_Certificados_AfeccionTipo]", "FROM [820_Certificados_AfeccionTipo](NULL, NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [810_AfiliadosActivos_GE]", "FROM [810_AfiliadosActivos_GE](NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [827_AfiliadosActivos_GE_Sexo]", "FROM [827_AfiliadosActivos_GE_Sexo](NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [824_PrestacionesCantidad]", "FROM [824_PrestacionesCantidad](NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [825_PrestacionesImporte_Pesos]", "FROM [825_PrestacionesImporte_Pesos](NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [826_PrestacionesImporte_Dolares]", "FROM [826_PrestacionesImporte_Dolares](NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [1025_PagoParcialFromPrestamo]", "FROM [acc_sp_1025_PagoParcialFromPrestamo_q](@pIDPrestamo) AS [1025_PagoParcialFromPrestamo]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [1017_LiquidoSubsidioxMesAnio] INNER JOIN", "FROM [1017_LiquidoSubsidioxMesAnio](@pMes, @pAnio) AS [1017_LiquidoSubsidioxMesAnio] INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM 1130_FacturaInteresMes) d;", "FROM [1130_FacturaInteresMes](@pCodMoneda)) d;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM 1130_FacturaInteresMes) src '", "FROM [1130_FacturaInteresMes](@pCodMoneda)) src '", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [806_CertificadosCantidad]", "FROM [806_CertificadosCantidad](NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [806_CertificadosEntre]", "FROM [806_CertificadosEntre](0, 0, NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [813_CertificadosAfeccion]", "FROM [813_CertificadosAfeccion](NULL, NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [220_AfiliadoImponibleMes]", "FROM [220_AfiliadoImponibleMes](@pCI, @pMes, @pMesIni)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM 220_AfiliadoImponibleMes", "FROM [220_AfiliadoImponibleMes](@pCI, @pMes, @pMesIni)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("[220_AfiliadoImponibleMes](@pCI, @pMes, @pMesIni)(@pCI, @pMes, @pMesIni)", "[220_AfiliadoImponibleMes](@pCI, @pMes, @pMesIni)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [220_AfiliadoImponibleMes](@pCI, @pMes, @pMesIni)", "FROM [acc_sgpa_220_AfiliadoImponibleMes_q](@pCI, @pMes, @pMesIni) AS [220_AfiliadoImponibleMes]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [250_ActivosXEmpresaAUnaFecha](@pFecha) LEFT JOIN [250_AportantesAUnMes](@pAnioMes) ON [250_ActivosXEmpresaAUnaFecha].CodEmpresa = [250_AportantesAUnMes].CodEmpresa", "FROM [acc_sgpa_250_ActivosXEmpresaAUnaFecha_q](@pFecha) AS [250_ActivosXEmpresaAUnaFecha] LEFT JOIN [acc_sgpa_250_AportantesAUnMes_q](@pAnioMes) AS [250_AportantesAUnMes] ON [250_ActivosXEmpresaAUnaFecha].CodEmpresa = [250_AportantesAUnMes].CodEmpresa", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [250_ActivosXEmpresaAUnaFecha] LEFT JOIN [250_AportantesAUnMes] ON [250_ActivosXEmpresaAUnaFecha].CodEmpresa = [250_AportantesAUnMes].CodEmpresa", "FROM [acc_sgpa_250_ActivosXEmpresaAUnaFecha_q](@pFecha) AS [250_ActivosXEmpresaAUnaFecha] LEFT JOIN [acc_sgpa_250_AportantesAUnMes_q](@pAnioMes) AS [250_AportantesAUnMes] ON [250_ActivosXEmpresaAUnaFecha].CodEmpresa = [250_AportantesAUnMes].CodEmpresa", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INNER JOIN [300_TrabajaActivo](@pMes) ON", "INNER JOIN [acc_sgpa_300_TrabajaActivo_q](@pMes) AS [300_TrabajaActivo] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INNER JOIN [300_AfiliadoAporteOk](@pMesIniImp, @pMesFin) ON", "INNER JOIN [acc_sgpa_300_AfiliadoAporteOk_q](@pMesIniImp, @pMesFin) AS [300_AfiliadoAporteOk] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [460_IMS_Actual](@pAnioMes), IMS INNER JOIN", "FROM [acc_sgpa_460_IMS_Actual_q](@pAnioMes) AS [460_IMS_Actual], IMS INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM 460_IMS_Actual, IMS INNER JOIN", "FROM [acc_sgpa_460_IMS_Actual_q](@pAnioMes) AS [460_IMS_Actual], IMS INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [300_AfiliadoDiasImporte](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)", "FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM 300_AfiliadoDiasImporte(@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)", "FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)", "FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp) AS [300_AfiliadoDiasImporte]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [801_CI_Todos](@pMes, @pMesIni, @pCodEmpresa)", "FROM [acc_sgpa_801_CI_Todos_q](@pMes, @pMesIni, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [801_Promedio_Ult6](@pMes, @pMesIni, @pCodEmpresa)", "FROM [acc_sgpa_801_Promedio_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [801_Promedio_UltMes](@pMes, @pCodEmpresa)", "FROM [acc_sgpa_801_Promedio_UltMes_q](@pMes, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [802_>0_Ult6](@pMes, @pMesIni, @pCodEmpresa)", "FROM [acc_sgpa_802__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [802_>0_UltMes](@pMes, @pCodEmpresa)", "FROM [acc_sgpa_802__0_UltMes_q](@pMes, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [802_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_802_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [802_UltMes](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_802_UltMes_q](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [803_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_803_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [803_UltMes](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_803_UltMes_q](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [804_>0_Ult6](@pMes, @pMesIni, @pCodEmpresa)", "FROM [acc_sgpa_804__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [804_>0_UltMes](@pMes, @pCodEmpresa)", "FROM [acc_sgpa_804__0_UltMes_q](@pMes, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [804_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_804_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [804_UltMes](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_804_UltMes_q](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_804__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)", "FROM [acc_sgpa_804__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa) AS [804_>0_Ult6]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_804__0_UltMes_q](@pMes, @pCodEmpresa)", "FROM [acc_sgpa_804__0_UltMes_q](@pMes, @pCodEmpresa) AS [804_>0_UltMes]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_804_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_804_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa) AS [804_Ult6]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_804_UltMes_q](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_804_UltMes_q](@pMes, @pSMN, @pCodEmpresa) AS [804_UltMes]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [811_Afiliado<125_Pct_Ult6](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [811_Afiliado>125_Pct_Ult6](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_1_q](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [811_Afiliado<125_Pct_UltMes](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_UltMes_q](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [811_Afiliado>125_Pct_UltMes](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_UltMes_1_q](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [811_Afiliado<125_Pct_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_Ult6]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [811_Afiliado<125_Pct_UltMes](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_UltMes_q](@pMes, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_UltMes]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_Ult6]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_Ult6]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_811_Afiliado_125_Pct_UltMes_q](@pMes, @pSMN, @pCodEmpresa)", "FROM [acc_sgpa_811_Afiliado_125_Pct_UltMes_q](@pMes, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_UltMes]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("AS [811_Afiliado<125_Pct_Ult6] AS [811_Afiliado<125_Pct_Ult6]", "AS [811_Afiliado<125_Pct_Ult6]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("AS [811_Afiliado<125_Pct_UltMes] AS [811_Afiliado<125_Pct_UltMes]", "AS [811_Afiliado<125_Pct_UltMes]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM ((([250_AfiliadoConDerecho] INNER JOIN Trabaja ON [250_AfiliadoConDerecho].ci = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON [250_AfiliadoConDerecho].ci = Afiliado.CI) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista", "FROM ((([acc_sgpa_250_AfiliadoConDerecho_q](@pMesFin, @pMesIni, @pSMN) AS [250_AfiliadoConDerecho] INNER JOIN Trabaja ON [250_AfiliadoConDerecho].ci = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON [250_AfiliadoConDerecho].ci = Afiliado.CI) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM (([250_AfiliadoConDerecho] INNER JOIN", "FROM (([acc_sgpa_250_AfiliadoConDerecho_q](@pMesFin, @pMesIni, @pSMN) AS [250_AfiliadoConDerecho] INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("SELECT [acc_sgpa_300_SubsidioItemCod_Full_Data_q].*\r\nFROM [acc_sgpa_300_SubsidioItemCod_Full_Data_q](@pFecha, @pCI)", "SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip, SubsidioItemCod.Tipo, SubsidioItemCod.ValorTipo, SubsidioItemCod.Signo, SubsidioItemCod.Comparar, SubsidioItemCod.CompararContra, SubsidioItemCod.Valor, SubsidioItemCod.TipoComp, SubsidioItemCod.Operador, SubsidioItemCod.ValorMin, SubsidioItemCod.ValorMax, SubsidioItemCod.Procesar, SubsidioItemCod.FechaVigencia, SubsidioItemCod.FechaBaja, SubsidioItemCod.ModificaNominal, 0 As CI\r\nFROM SubsidioItemCod\r\nWHERE (((SubsidioItemCod.Procesar)= 1) AND ((SubsidioItemCod.FechaVigencia)<=@pFecha) AND ((SubsidioItemCod.FechaBaja)>@pFecha Or (SubsidioItemCod.FechaBaja) Is Null))\r\nUNION ALL SELECT *\r\nFROM [acc_sgpa_Rs_SubsidioItemCodXCI_q]\r\nWHERE (((acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaVigencia)<=@pFecha) AND ((acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaBaja)>@pFecha Or (acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaBaja) Is Null)) and CI = @pCI", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [201_PagarMutualista](@pMes, @pMesIni, @pSMN)", "FROM [acc_sgpa_201_PagarMutualista_q](@pMes, @pMesIni, @pSMN) AS [201_PagarMutualista]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("LEFT JOIN [201_PagarMutualista](@pMes, @pMesIni, @pSMN) ON", "LEFT JOIN [acc_sgpa_201_PagarMutualista_q](@pMes, @pMesIni, @pSMN) AS [201_PagarMutualista] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("LEFT JOIN [506_Rpt_LiquidacionBPS](@pMes, @pAnio) ON", "LEFT JOIN [acc_sgpa_506_Rpt_LiquidacionBPS_q](@pMes, @pAnio) AS [506_Rpt_LiquidacionBPS] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("LEFT JOIN 506_Rpt_LiquidacionBPS ON", "LEFT JOIN [acc_sgpa_506_Rpt_LiquidacionBPS_q](@pMes, @pAnio) AS [506_Rpt_LiquidacionBPS] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("SELECT CI FROM [765_CertificacionContinua](@pFecha)", "SELECT CI FROM [acc_sgpa_765_CertificacionContinua_q](@pFecha)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("UNION SELECT CI FROM [765_CertificacionEmpalma](@pFecha)", "UNION SELECT CI FROM [acc_sgpa_765_CertificacionEmpalma_q](@pFecha)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [800_AfiliadoImponible_Mes](@pCodEmpresa) AS I", "FROM [acc_sgpa_800_AfiliadoImponible_Mes_q](@pCodEmpresa) AS I", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [800_AfiliadoImponible_Mes_Fecha](@pCodEmpresa, @pMes) AS I", "FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INNER JOIN [813_CertificacionAfeccionDistintas](@pFechaIni, @pFechaFin) ON", "INNER JOIN [acc_sgpa_813_CertificacionAfeccionDistintas_q](@pFechaIni, @pFechaFin) AS [813_CertificacionAfeccionDistintas] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("LEFT JOIN [830_CantidadPorPuesto](@pFecha) ON", "LEFT JOIN [acc_sgpa_830_CantidadPorPuesto_q](@pFecha) AS [830_CantidadPorPuesto] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("LEFT JOIN [830_CantidadPorPuestoNo0](@pFecha, @pMes) ON", "LEFT JOIN [acc_sgpa_830_CantidadPorPuestoNo0_q](@pFecha, @pMes) AS [830_CantidadPorPuestoNo0] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM Rpt_Mutualista;", "FROM [acc_sgpa_Rpt_Mutualista_q];", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM Rs_Bps4;", "FROM [acc_sgpa_Rs_Bps4_q] AS Rs_Bps4;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM Empresa INNER JOIN (Rs_MaxImp_Afiliado AS I INNER JOIN Afiliado ON I.CI = Afiliado.CI) ON Empresa.CodEmpresa = I.CodEmpresa", "FROM Empresa INNER JOIN ((SELECT Imponible.CI, MIN(Imponible.CodEmpresa) AS CodEmpresa, MIN(Imponible.Mes) AS Mes, MIN(Imponible.Anio) AS Anio, Max(Imponible.Importe) AS Importe FROM Imponible GROUP BY Imponible.CI) AS I INNER JOIN Afiliado ON I.CI = Afiliado.CI) ON Empresa.CodEmpresa = I.CodEmpresa", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM ((Rs_Bps4 INNER JOIN Afiliado ON", "FROM (([acc_sgpa_Rs_Bps4_q] AS Rs_Bps4 INNER JOIN Afiliado ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("LEFT JOIN [101_ImponibleMes](@pMes, @pAno) AS I ON", "LEFT JOIN [acc_sgpa_101_ImponibleMes_q](@pMes, @pAno) AS I ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM (([815_AfiliadoImponible] INNER JOIN", "FROM (([815_AfiliadoImponible](@pMes, @pSMN, @pCodEmpresa) INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [815_AfiliadoImponible]", "FROM [815_AfiliadoImponible](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [814_AfiliadoImponibleFranja]", "FROM [814_AfiliadoImponibleFranja](@pMes, @pSMN, @pCodEmpresa)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [814_AfiliadoImponibleFranja] INNER JOIN", "FROM [814_AfiliadoImponibleFranja](@pMes, @pSMN, @pCodEmpresa) INNER JOIN", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [816_Certificados_GrupoAfeccion]", "FROM [816_Certificados_GrupoAfeccion](NULL, NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [817_Certificados_Patologia]", "FROM [817_Certificados_Patologia](NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [812_AfiliadoActivoEspecialidad]", "FROM [acc_sgpa_812_AfiliadoActivoEspecialidad_q] AS acc_sgpa_812_AfiliadoActivoEspecialidad_q", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [812_AfiliadosEspecialidad]", "FROM [812_AfiliadosEspecialidad](NULL)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [500_Rpt_NoPagarMutualista]", "FROM [500_Rpt_NoPagarMutualista](@pMes, @pMesIni, @pSMN)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("LEFT JOIN [756_NoBaja] ON", "LEFT JOIN [756_NoBaja](@pFecha) AS [756_NoBaja] ON", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].Id = SubsidioCabezal.IdSubsidio", "IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL\r\n    BEGIN\r\n        IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\r\n            UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].IdSubsidio = SubsidioCabezal.IdSubsidio\r\n        ELSE IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','Id') IS NOT NULL\r\n            UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].Id = SubsidioCabezal.IdSubsidio\r\n        ELSE\r\n            SELECT 1 AS NoOp\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp\r\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL\r\n    BEGIN\r\n        IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\r\n            UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].IdSubsidio = SubsidioCabezal.IdSubsidio\r\n        ELSE IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','Id') IS NOT NULL\r\n            UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].Id = SubsidioCabezal.IdSubsidio\r\n        ELSE\r\n            SELECT 1 AS NoOp\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp\r\n    END", "IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL\r\n    BEGIN\r\n        IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\r\n            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.IdSubsidio = sc.IdSubsidio');\r\n        ELSE IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','Id') IS NOT NULL\r\n            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.Id = sc.IdSubsidio');\r\n        ELSE\r\n            SELECT 1 AS NoOp\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp\r\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL\n    BEGIN\n        IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\n            UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].IdSubsidio = SubsidioCabezal.IdSubsidio\n        ELSE IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','Id') IS NOT NULL\n            UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].Id = SubsidioCabezal.IdSubsidio\n        ELSE\n            SELECT 1 AS NoOp\n    END\n    ELSE\n    BEGIN\n        SELECT 1 AS NoOp\n    END", "IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL\n    BEGIN\n        IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\n            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.IdSubsidio = sc.IdSubsidio');\n        ELSE IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','Id') IS NOT NULL\n            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.Id = sc.IdSubsidio');\n        ELSE\n            SELECT 1 AS NoOp\n    END\n    ELSE\n    BEGIN\n        SELECT 1 AS NoOp\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].IdSubsidio = SubsidioCabezal.IdSubsidio", "IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL AND COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\r\n    BEGIN\r\n        UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].IdSubsidio = SubsidioCabezal.IdSubsidio\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp\r\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL AND COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\r\n    BEGIN\r\n        UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].IdSubsidio = SubsidioCabezal.IdSubsidio\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp\r\n    END", "IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL\r\n    BEGIN\r\n        IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\r\n            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.IdSubsidio = sc.IdSubsidio');\r\n        ELSE IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','Id') IS NOT NULL\r\n            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.Id = sc.IdSubsidio');\r\n        ELSE\r\n            SELECT 1 AS NoOp\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SELECT 1 AS NoOp\r\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL AND COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\n    BEGIN\n        UPDATE SubsidioCabezal SET SubsidioCabezal.ImpLiquido = [506_Rpt_Liquidos_Subsidios].[LiquidoPagar] FROM [506_Rpt_Liquidos_Subsidios] INNER JOIN SubsidioCabezal ON [506_Rpt_Liquidos_Subsidios].IdSubsidio = SubsidioCabezal.IdSubsidio\n    END\n    ELSE\n    BEGIN\n        SELECT 1 AS NoOp\n    END", "IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL\n    BEGIN\n        IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL\n            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.IdSubsidio = sc.IdSubsidio');\n        ELSE IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','Id') IS NOT NULL\n            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.Id = sc.IdSubsidio');\n        ELSE\n            SELECT 1 AS NoOp\n    END\n    ELSE\n    BEGIN\n        SELECT 1 AS NoOp\n    END", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [400_Promedio_Mes];", "FROM [acc_sgpa_400_Promedio_Mes_q] AS acc_sgpa_400_Promedio_Mes_q;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [400_Promedio_Mes_Puesto];", "FROM [acc_sgpa_400_Promedio_Mes_Puesto_q] AS acc_sgpa_400_Promedio_Mes_Puesto_q;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rpt_Afiliado.", "acc_sgpa_Rpt_Afiliado_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rpt_Certificacion.", "acc_sgpa_Rpt_Certificacion_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_AfiliadoImponibleMes.", "acc_sgpa_Rs_AfiliadoImponibleMes_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_AfiliadoImponibleMesNoBaja.", "acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_AfiliadoEspecialidadDesc.", "acc_sgpa_Rs_AfiliadoEspecialidadDesc_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_SubsidioItemCodXCI.", "acc_sgpa_Rs_SubsidioItemCodXCI_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_SubsidioXMes.", "acc_sgpa_Rs_SubsidioXMes_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_TrabajaActivo.", "acc_sgpa_Rs_TrabajaActivo_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("xEmpresaCantidad.", "acc_sgpa_xEmpresaCantidad_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("xEmpresaPromedio.", "acc_sgpa_xEmpresaPromedio_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FORMAT(acc_sgpa_xEmpresaPromedio_q.PromedioDeImporte, '0.00')", "FORMAT(TRY_CONVERT(decimal(18,2),acc_sgpa_xEmpresaPromedio_q.PromedioDeImporte), '0.00')", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("xEmpresaPromedioTodo.", "acc_sgpa_xEmpresaPromedioTodo_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FORMAT(acc_sgpa_xEmpresaPromedioTodo_q.PromedioDeImporte,'0.00')", "FORMAT(TRY_CONVERT(decimal(18,2),acc_sgpa_xEmpresaPromedioTodo_q.PromedioDeImporte),'0.00')", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("xEmpresaPromedioNo0.", "acc_sgpa_xEmpresaPromedioNo0_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("xMutualistaCantidad.", "acc_sgpa_xMutualistaCantidad_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("xSinImponiblexEmpresa.", "acc_sgpa_xSinImponiblexEmpresa_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("Rs_Imponible_Ult.", "acc_sgpa_Rs_Imponible_Ult_q.", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("[Afiliado].[CI]", "acc_sgpa_Rpt_Afiliado_q.CI", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("acc_sgpa_Rpt_Certificacion_q.[CI]", "acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE (Certificacion.[CI] = 13606922)", "WHERE (acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI] = 13606922)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM Rpt_Afiliado;", "FROM [acc_sgpa_Rpt_Afiliado_q] AS acc_sgpa_Rpt_Afiliado_q;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM Rpt_Certificacion;", "FROM [acc_sgpa_Rpt_Certificacion_q] AS acc_sgpa_Rpt_Certificacion_q;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM (Rpt_Certificacion INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q]", "FROM ([acc_sgpa_Rpt_Certificacion_q] AS acc_sgpa_Rpt_Certificacion_q INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM Rs_AfiliadoImponibleMes", "FROM [acc_sgpa_Rs_AfiliadoImponibleMes_q]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM Rs_AfiliadoImponibleMesNoBaja", "FROM [acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("UNION ALL SELECT *\r\nFROM [acc_sgpa_Rs_SubsidioItemCodXCI_q]", "UNION ALL SELECT acc_sgpa_Rs_SubsidioItemCodXCI_q.*\r\nFROM [acc_sgpa_Rs_SubsidioItemCodXCI_q]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace(
                "SELECT SubsidioItemCod.*,0 As CI",
                "SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip, SubsidioItemCod.Tipo, SubsidioItemCod.ValorTipo, SubsidioItemCod.Signo, SubsidioItemCod.Comparar, SubsidioItemCod.CompararContra, SubsidioItemCod.Valor, SubsidioItemCod.TipoComp, SubsidioItemCod.Operador, SubsidioItemCod.ValorMin, SubsidioItemCod.ValorMax, SubsidioItemCod.Procesar, SubsidioItemCod.FechaVigencia, SubsidioItemCod.FechaBaja, SubsidioItemCod.ModificaNominal, 0 As CI",
                StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("acc_sgpa_Rs_Certificacion_Nombre_q.[CI]", "Certificacion.[CI]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE ( Certificacion.[CI] = 11391501 )", "WHERE ( acc_sgpa_Rs_Certificacion_Nombre_q.[CI] = 11391501 )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE ( Certificacion.[CI] = 11391501 );", "WHERE ( acc_sgpa_Rs_Certificacion_Nombre_q.[CI] = 11391501 );", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE ( acc_sgpa_Rs_Certificacion_Nombre_q.[CI] = 11391501 )", "WHERE ( Certificacion.[CI] = 11391501 )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("CREATE OR ALTER VIEW [dbo].[acc_sgpa_480_Certificacion_q]", "CREATE OR ALTER VIEW [dbo].[acc_sgpa_480_Certificacion_q]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE ( Certificacion.[CI] = 11391501 )\r\nGO\r\n\r\n-- ===== DATA OBJECT FOR QUERY: 510_Rpt_Trabaja =====", "WHERE ( acc_sgpa_Rs_Certificacion_Nombre_q.[CI] = 11391501 )\r\nGO\r\n\r\n-- ===== DATA OBJECT FOR QUERY: 510_Rpt_Trabaja =====", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_Rs_Certificacion_Nombre_q]\r\nWHERE ( Certificacion.[CI] = 11391501 )", "FROM [acc_sgpa_Rs_Certificacion_Nombre_q]\r\nWHERE ( acc_sgpa_Rs_Certificacion_Nombre_q.[CI] = 11391501 )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [acc_sgpa_Rs_Certificacion_Nombre_q]\r\n    WHERE ( Certificacion.[CI] = 11391501 );", "FROM [acc_sgpa_Rs_Certificacion_Nombre_q]\r\n    WHERE ( acc_sgpa_Rs_Certificacion_Nombre_q.[CI] = 11391501 );", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE Mes = 07 AND Anio = 2013 AND Imponible.CodEmpresa = 900", "WHERE Mes = 07 AND Anio = 2013 AND CodEmpresa = 900", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("CASE WHEN Importe > 0 And Importe <=", "CASE WHEN Avg([I].[Importe]) > 0 And Avg([I].[Importe]) <=", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace(" WHEN Importe > ", " WHEN Avg([I].[Importe]) > ", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace(" And Importe <= ", " And Avg([I].[Importe]) <= ", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("(CASE WHEN Importe<=10000 THEN 'Mas de 0 hasta 10.000' WHEN Importe>10000 And Importe<=20000 THEN 'Mas de 10.000 hasta 20.000' WHEN Importe>20000 THEN 'Mas de 20.000' ELSE NULL END)", "(CASE WHEN Avg([I].[Importe])<=10000 THEN 'Mas de 0 hasta 10.000' WHEN Avg([I].[Importe])>10000 And Avg([I].[Importe])<=20000 THEN 'Mas de 10.000 hasta 20.000' WHEN Avg([I].[Importe])>20000 THEN 'Mas de 20.000' ELSE NULL END)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("(I.Fechaingreso = acc_sgpa_Rs_TrabajaActivo_q.FechaIngreso)", "(I.CI = acc_sgpa_Rs_TrabajaActivo_q.CI)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE ( ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes) AND acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa)=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END)", "WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END))", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END))\r\nGROUP BY I.CI", "WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END)))\r\nGROUP BY I.CI", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END))\r\n    GROUP BY I.CI", "WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END)))\r\n    GROUP BY I.CI", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END))\n    GROUP BY I.CI", "WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END)))\n    GROUP BY I.CI", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("HAVING (((Avg([I].[Importe]))=0))", "HAVING (Avg([I].[Importe])=0)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, NOMBRE, APELLIDO, MONTO_TOTAL, MES_DE_CARGO, NOM_EMPRESA, PCT_POR_EMPRESA, FECHA_PER_DESDE, FECHA_PER_HASTA, [N_ ENTREGA], FECHA_DE_ENTREGA, MES, ANIO, LIQUIDO )", "INSERT INTO Liquidacion_BPS ( CI, NOMBRE, APELLIDO, MONTO_TOTAL, MES_DE_CARGO, NOM_EMPRESA, PCT_POR_EMPRESA, FECHA_PER_DESDE, FECHA_PER_HASTA, [N_ ENTREGA], FECHA_DE_ENTREGA, MES, ANIO, LIQUIDO )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [506_Rpt_BPS]", "FROM [acc_sgpa_506_Rpt_BPS_q] AS [506_Rpt_BPS]", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("[506_Rpt_BPS].[MES DE CARGO]\\100", "([506_Rpt_BPS].[MES DE CARGO]/100)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("INSERT ( CI, Importe, Mes, Anio, Usr, Ts )", "INSERT INTO AdPreJubPago ( CI, Importe, Mes, Anio, Usr, Ts )", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("IIF((CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN 1 ELSE 0 END), @pFechaFin, AdPreJub.FechaJubilacion)", "(CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END)", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("END) + 1)  / (DATEDIFF(day, @pFechaIni, @pFechaFin) + 1))", "DATEADD(day, 1, END))  / NULLIF((DATEDIFF(day, @pFechaIni, @pFechaFin) + 1), 0))", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("GROUP BY acc_sgpa_Rpt_Certificacion_q.CI, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo\r\n    ORDER BY [FechaIni];", "GROUP BY acc_sgpa_Rpt_Certificacion_q.CI, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("GROUP BY acc_sgpa_Rpt_Certificacion_q.CI, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo\n    ORDER BY [FechaIni];", "GROUP BY acc_sgpa_Rpt_Certificacion_q.CI, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo;", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [805_CertificacionesxAnio](@pFechaIni, @pFechaFin);", "FROM [805_CertificacionesxAnio](NULL, NULL);", StringComparison.OrdinalIgnoreCase);
            finalScript = finalScript.Replace("FROM [805_Certificados_AnioCI]", "FROM [805_Certificados_AnioCI](NULL, NULL)", StringComparison.OrdinalIgnoreCase);
            // FIX ValorJornal (300_AfiliadoDiasImporte): el promedio por empresa debe dividirse por los días de la
            // VENTANA COMPLETA (meses de la ventana × 30), no por Sum(DiasTrabajados) de los meses con registro.
            // La query Access original (Importe/Dias) sólo daba bien cuando existían imponibles (incluso importe 0)
            // para todos los meses; al faltar registros undercounteaba. Coincide con la regla de negocio y con la
            // query Casemed (Sum(Importe/180)). Ver tools/sql/fix-diasimporte.sql en SgpaBlazor.
            finalScript = finalScript.Replace(
                "(CASE WHEN Sum(Imponible.DiasTrabajados)>0 THEN Sum(Imponible.Importe)/Sum(Imponible.DiasTrabajados) ELSE 0 END) AS Promedio",
                "Sum(Imponible.Importe) / (((@pMesFin / 100 - @pMesIni / 100) * 12 + (@pMesFin % 100 - @pMesIni % 100) + 1) * 30.0) AS Promedio",
                StringComparison.OrdinalIgnoreCase);
            File.WriteAllText(outSql, finalScript);

            Console.WriteLine($"Generated SQL procedures: {outSql} ({effectiveQueries.Count} queries)");
        }

        var summaryPath = Path.Combine(outputDir, "_summary.txt");
        File.WriteAllLines(summaryPath,
        [
            $"GeneratedAtUtc={DateTime.UtcNow:O}",
            $"SpecsDirectory={accessSpecsDir}",
            $"TotalSpecFiles={specFiles.Count}",
            $"TotalQueries={allQueries.Count}",
            $"OutputDirectory={outputDir}"
        ]);

        GenerateBootstrapMissingTablesArtifacts(specFiles, outputDir);

        Console.WriteLine($"Access query SQL generation complete. Total queries: {allQueries.Count}");
    }

    sealed class AccessSpecColumn
    {
        public required string Name { get; init; }
        public required string AccessType { get; init; }
        public int? Length { get; init; }
        public bool IsRequired { get; init; }
    }

    static void GenerateBootstrapMissingTablesArtifacts(IReadOnlyList<string> specFiles, string outputDir)
    {
        var allSpecTables = new Dictionary<string, List<AccessSpecColumn>>(StringComparer.OrdinalIgnoreCase);
        foreach (var spec in specFiles)
        {
            var tables = ParseAccessSpecTables(spec);
            foreach (var kv in tables)
            {
                if (!allSpecTables.TryGetValue(kv.Key, out var existing)
                    || existing.Count == 0
                    || kv.Value.Count > existing.Count)
                {
                    allSpecTables[kv.Key] = kv.Value;
                }
            }
        }

        var allSpecTablesPath = Path.Combine(outputDir, "_all_spec_tables.txt");
        File.WriteAllLines(allSpecTablesPath,
            allSpecTables.Keys
                .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                .ToList());

        var preferredMissingPath = Path.Combine(outputDir, "_missing_tables_after_subsidioimponible.txt");
        var fallbackMissingPath = Path.Combine(outputDir, "_missing_tables.txt");
        var missingPath = File.Exists(preferredMissingPath)
            ? preferredMissingPath
            : fallbackMissingPath;

        if (!File.Exists(missingPath))
            return;

        var missingObjects = File.ReadAllLines(missingPath)
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Where(x => !IsSqlCmdNoiseLine(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
            .ToList();

        foreach (var required in new[] { "xLiq1", "xLiq2", "Rpt_Historia_Vandalismo_S", "BpsFormat", "zRs_AEsp", "Casecasm", "acc_sp_1030_FacturaFlujo_q", "Cristalin" })
        {
            if (missingObjects.Contains(required, StringComparer.OrdinalIgnoreCase))
                continue;
            missingObjects.Add(required);
        }

        missingObjects = missingObjects
            .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("-- Auto-generated bootstrap for missing Access tables in SQL Server");
        sb.AppendLine("SET NOCOUNT ON;");
        sb.AppendLine();

        foreach (var objectName in missingObjects)
        {
            var escapedObjectName = EscapeSqlIdentifier(objectName);
            sb.AppendLine($"IF OBJECT_ID('dbo.{escapedObjectName}') IS NULL");
            sb.AppendLine("BEGIN");

            if (allSpecTables.TryGetValue(objectName, out var columns) && columns.Count > 0)
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                for (var i = 0; i < columns.Count; i++)
                {
                    var c = columns[i];
                    var comma = i < columns.Count - 1 ? "," : string.Empty;
                    sb.AppendLine($"        [{EscapeSqlIdentifier(c.Name)}] {MapAccessColumnTypeToSql(c)} {(c.IsRequired ? "NOT NULL" : "NULL")}{comma}");
                }

                sb.AppendLine("    );");
            }
            else if (objectName.Equals("xLiq1", StringComparison.OrdinalIgnoreCase)
                || objectName.Equals("xLiq2", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                sb.AppendLine("        [CI] int NULL");
                sb.AppendLine("    );");
            }
            else if (objectName.Equals("Rpt_Historia_Vandalismo_S", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                sb.AppendLine("        [Id] int IDENTITY(1,1) NOT NULL");
                sb.AppendLine("    );");
            }
            else if (objectName.Equals("BpsFormat", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                sb.AppendLine("        [Cedula] nvarchar(50) NULL,");
                sb.AppendLine("        [Mutualista] nvarchar(255) NULL");
                sb.AppendLine("    );");
            }
            else if (objectName.Equals("zRs_AEsp", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                sb.AppendLine("        [CI] int NULL,");
                sb.AppendLine("        [EspNom1] nvarchar(255) NULL,");
                sb.AppendLine("        [EspNom2] nvarchar(255) NULL,");
                sb.AppendLine("        [EspNom3] nvarchar(255) NULL");
                sb.AppendLine("    );");
            }
            else if (objectName.Equals("acc_sp_1030_FacturaFlujo_q", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                sb.AppendLine("        [IdPrestamo] int NULL,");
                sb.AppendLine("        [Mes] int NULL,");
                sb.AppendLine("        [Importe] float NULL");
                sb.AppendLine("    );");
            }
            else if (objectName.Equals("Casecasm", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                sb.AppendLine("        [Campo1] nvarchar(255) NULL,");
                sb.AppendLine("        [Campo2] nvarchar(255) NULL,");
                sb.AppendLine("        [Campo3] nvarchar(255) NULL,");
                sb.AppendLine("        [Campo4] nvarchar(255) NULL,");
                sb.AppendLine("        [Campo5] nvarchar(255) NULL");
                sb.AppendLine("    );");
            }
            else if (objectName.Equals("Cristalin", StringComparison.OrdinalIgnoreCase))
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                sb.AppendLine("        [DOCUMENTO] nvarchar(50) NULL,");
                sb.AppendLine("        [1ER APELLIDO] nvarchar(255) NULL,");
                sb.AppendLine("        [2DO APELLIDO] nvarchar(255) NULL,");
                sb.AppendLine("        [1ER NOMBRE] nvarchar(255) NULL,");
                sb.AppendLine("        [2DO NOMBRE] nvarchar(255) NULL");
                sb.AppendLine("    );");
            }
            else
            {
                sb.AppendLine($"    CREATE TABLE dbo.[{escapedObjectName}] (");
                sb.AppendLine("        [Id] int IDENTITY(1,1) NOT NULL");
                sb.AppendLine("    );");
            }

            sb.AppendLine("END;");
            sb.AppendLine("GO");
            sb.AppendLine();
        }

        sb.AppendLine("IF OBJECT_ID('dbo.Casecasm') IS NOT NULL");
        sb.AppendLine("BEGIN");
        sb.AppendLine("    IF COL_LENGTH('dbo.Casecasm','Campo1') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo1] nvarchar(255) NULL;");
        sb.AppendLine("    IF COL_LENGTH('dbo.Casecasm','Campo2') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo2] nvarchar(255) NULL;");
        sb.AppendLine("    IF COL_LENGTH('dbo.Casecasm','Campo3') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo3] nvarchar(255) NULL;");
        sb.AppendLine("    IF COL_LENGTH('dbo.Casecasm','Campo4') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo4] nvarchar(255) NULL;");
        sb.AppendLine("    IF COL_LENGTH('dbo.Casecasm','Campo5') IS NULL ALTER TABLE dbo.[Casecasm] ADD [Campo5] nvarchar(255) NULL;");
        sb.AppendLine("END;");
        sb.AppendLine("GO");
        sb.AppendLine();

        var bootstrapPath = Path.Combine(outputDir, "_bootstrap_missing_tables.sql");
        File.WriteAllText(bootstrapPath, sb.ToString());
    }

    static Dictionary<string, List<AccessSpecColumn>> ParseAccessSpecTables(string specFile)
    {
        var result = new Dictionary<string, List<AccessSpecColumn>>(StringComparer.OrdinalIgnoreCase);
        var lines = File.ReadAllLines(specFile);
        var inTablesSection = false;
        string? currentTable = null;
        var currentColumns = new List<AccessSpecColumn>();

        void FlushTable()
        {
            if (string.IsNullOrWhiteSpace(currentTable))
            {
                currentColumns.Clear();
                return;
            }

            result[currentTable] = [.. currentColumns];
            currentTable = null;
            currentColumns.Clear();
        }

        foreach (var rawLine in lines)
        {
            var line = rawLine.TrimEnd();
            var trimmed = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
                continue;

            if (!inTablesSection)
            {
                if (trimmed.Equals("=== TABLES ===", StringComparison.OrdinalIgnoreCase))
                    inTablesSection = true;
                continue;
            }

            if (trimmed.Equals("=== QUERIES ===", StringComparison.OrdinalIgnoreCase))
            {
                FlushTable();
                break;
            }

            if (trimmed.StartsWith("TABLE:", StringComparison.OrdinalIgnoreCase))
            {
                FlushTable();
                currentTable = trimmed[6..].Trim();
                continue;
            }

            if (currentTable is null)
                continue;

            var parts = trimmed.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
                continue;

            var name = parts[0].Trim();
            var typeToken = parts[1].Trim();
            var accessType = typeToken;
            int? len = null;
            var m = Regex.Match(typeToken, @"^(?<type>[A-Za-z]+)(?:\((?<len>\d+)\))?$");
            if (m.Success)
            {
                accessType = m.Groups["type"].Value;
                if (int.TryParse(m.Groups["len"].Value, out var parsedLen))
                    len = parsedLen;
            }

            var required = parts.Skip(2)
                .Any(p => p.Contains("Required=Verdadero", StringComparison.OrdinalIgnoreCase)
                    || p.Contains("Required=True", StringComparison.OrdinalIgnoreCase));

            currentColumns.Add(new AccessSpecColumn
            {
                Name = name,
                AccessType = accessType,
                Length = len,
                IsRequired = required
            });
        }

        FlushTable();
        return result;
    }

    static bool IsSqlCmdNoiseLine(string value)
    {
        return Regex.IsMatch(value, @"^\(\d+\s+rows?\s+affected\)$", RegexOptions.IgnoreCase);
    }

    static string EscapeSqlIdentifier(string value)
    {
        return value.Replace("]", "]]", StringComparison.Ordinal);
    }

    static string MapAccessColumnTypeToSql(AccessSpecColumn column)
    {
        return column.AccessType.ToLowerInvariant() switch
        {
            "byte" => "tinyint",
            "integer" => "smallint",
            "long" => "int",
            "single" => "real",
            "double" => "float",
            "date" => "datetime2(0)",
            "boolean" => "bit",
            "text" => $"nvarchar({NormalizeTextLength(column.Length)})",
            _ => "nvarchar(max)"
        };
    }

    static int NormalizeTextLength(int? length)
    {
        if (!length.HasValue || length.Value <= 0)
            return 255;

        return Math.Min(length.Value, 4000);
    }

    static string FindProjectDirectory()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "NewSgpa.Migration.csproj")))
                return dir.FullName;

            dir = dir.Parent;
        }

        throw new DirectoryNotFoundException("Could not locate NewSgpa.Migration project directory from current execution path.");
    }

    static IReadOnlyList<AccessQueryDefinition> BuildQueriesWithPropagatedParameters(
        IReadOnlyList<AccessQueryDefinition> parsed,
        AccessQueryPlan initialPlan)
    {
        var byName = parsed.ToDictionary(q => q.Name, StringComparer.OrdinalIgnoreCase);
        var effective = parsed.ToDictionary(
            q => q.Name,
            q => q.Parameters.ToList(),
            StringComparer.OrdinalIgnoreCase);

        foreach (var item in initialPlan.OrderedItems)
        {
            if (!effective.TryGetValue(item.Query.Name, out var current))
                continue;

            foreach (var dep in item.Dependencies)
            {
                if (!effective.TryGetValue(dep, out var depParams))
                    continue;

                foreach (var p in depParams)
                {
                    if (!IsAccessParameterName(p.Name))
                        continue;

                    if (current.Any(x => x.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase)))
                        continue;

                    current.Add(new AccessQueryParameter
                    {
                        Name = p.Name,
                        AccessType = p.AccessType
                    });
                }
            }
        }

        return parsed
            .Select(q => new AccessQueryDefinition
            {
                SourceFile = q.SourceFile,
                Name = q.Name,
                RawSql = q.RawSql,
                Parameters = effective[q.Name]
                    .Where(p => IsAccessParameterName(p.Name))
                    .ToList()
            })
            .ToList();
    }

    static bool IsAccessParameterName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;
        return Regex.IsMatch(name, @"^p[A-Z]\w*$");
    }
}




