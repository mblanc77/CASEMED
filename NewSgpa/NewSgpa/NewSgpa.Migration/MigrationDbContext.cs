using Microsoft.EntityFrameworkCore;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using NewSgpa.Module.BusinessObjects.Sgpa;
using NewSgpa.Module.BusinessObjects.Prestamos;
using NewSgpa.Module.BusinessObjects.Shared;

namespace NewSgpa.Migration
{
    /// <summary>
    /// Standalone Migration DbContext - NO XAF DECORATIONS.
    /// Completely independent from NewSgpaEFCoreDbContext to avoid XAF TypesInfoInitializer requirement.
    /// This context can run in a simple console app without XAF's DI container.
    /// </summary>
    public class MigrationDbContext : DbContext
    {
        public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
        {
        }

        // === XAF Core Tables ===
        public DbSet<ModelDifference> ModelDifferences { get; set; }
        public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
        public DbSet<PermissionPolicyRole> Roles { get; set; }
        public DbSet<NewSgpa.Module.BusinessObjects.ApplicationUser> Users { get; set; }
        public DbSet<NewSgpa.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginsInfo { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<ReportDataV2> ReportDataV2 { get; set; }
        public DbSet<DashboardData> DashboardData { get; set; }
        public DbSet<AuditDataItemPersistent> AuditData { get; set; }
        public DbSet<AuditEFCoreWeakReference> AuditEFCoreWeakReferences { get; set; }
        public DbSet<HCategory> HCategories { get; set; }

        // === SGPA Domain ===
        public DbSet<Afiliado> Afiliados { get; set; }
        public DbSet<AfiliadoApunte> AfiliadoApuntes { get; set; }
        public DbSet<AfiliadoEspecialidad> AfiliadoEspecialidades { get; set; }
        public DbSet<Trabaja> Trabajos { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<AdPreJub> AdPreJubs { get; set; }
        public DbSet<AdPreJubPago> AdPreJubPagos { get; set; }
        public DbSet<Certificacion> Certificaciones { get; set; }
        public DbSet<CertificacionProrroga> CertificacionProrrogas { get; set; }
        public DbSet<Prestacion> Prestaciones { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Imponible> Imponibles { get; set; }
        public DbSet<EmpresaPago> EmpresaPagos { get; set; }
        public DbSet<ReintegroMutual> ReintegrosMutuales { get; set; }
        public DbSet<SubsidioCabezal> SubsidioCabezales { get; set; }
        public DbSet<SubsidioEnfermedad> SubsidioEnfermedades { get; set; }
        public DbSet<SubsidioCabezalEmpresa> SubsidioCabezalEmpresas { get; set; }
        public DbSet<SubsidioCabezalBps> SubsidioCabezalesBps { get; set; }
        public DbSet<SubsidioItem> SubsidioItems { get; set; }
        public DbSet<SubsidioItemCod> SubsidioItemCods { get; set; }
        public DbSet<SubsidioItemCodAfiliado> SubsidioItemCodAfiliados { get; set; }
        public DbSet<SubsidioItemEmpresa> SubsidioItemEmpresas { get; set; }
        public DbSet<SubsidioImponible> SubsidioImponibles { get; set; }

        // === SGPA Lookups ===
        public DbSet<Mutualista> Mutualistas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<AfeccionGrupo> AfeccionGrupos { get; set; }
        public DbSet<AfeccionTipo> AfeccionTipos { get; set; }
        public DbSet<Patologia> Patologias { get; set; }
        public DbSet<Certificador> Certificadores { get; set; }
        public DbSet<SalidaTipo> SalidaTipos { get; set; }
        public DbSet<BajaMotivo> BajaMotivos { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<FormaPago> FormasPago { get; set; }
        public DbSet<RegimenAporte> RegimenesAporte { get; set; }
        public DbSet<RegimenJubilatorio> RegimenesJubilatorio { get; set; }
        public DbSet<AporteTipo> AporteTipos { get; set; }
        public DbSet<SituacionPago> SituacionesPago { get; set; }
        public DbSet<SituacionMutual> SituacionesMutual { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<PrestacionTipo> PrestacionTipos { get; set; }
        public DbSet<RecetaDistancia> RecetaDistancias { get; set; }
        public DbSet<Ims> ImsRegistros { get; set; }
        public DbSet<FranjaIrpf> FranjasIrpf { get; set; }
        public DbSet<GrupoEtario> GruposEtarios { get; set; }
        public DbSet<CertificacionesTmp> CertificacionesTmp { get; set; }
        public DbSet<InformeEstadistico> InformesEstadisticos { get; set; }
        public DbSet<Parametros> Parametros { get; set; }
        public DbSet<PrimaFallecimiento> PrimasFallecimiento { get; set; }
        public DbSet<NoCargadoHL> NoCargadosHL { get; set; }

        // === SP (Sistema de Préstamos) ===
        public DbSet<SpPrestamo> SpPrestamos { get; set; }
        public DbSet<SpPrestamoComentario> SpPrestamoComentarios { get; set; }
        public DbSet<SpAfiliadoComentario> SpAfiliadoComentarios { get; set; }
        public DbSet<SpFactura> SpFacturas { get; set; }
        public DbSet<SpFacturaDetalle> SpFacturaDetalles { get; set; }
        public DbSet<SpCuota> SpCuotas { get; set; }
        public DbSet<SpCuadroAmortizacion> SpCuadrosAmortizacion { get; set; }
        public DbSet<SpPago> SpPagos { get; set; }
        public DbSet<SpPagoItemPago> SpPagoItemPagos { get; set; }
        public DbSet<SpRetencion> SpRetenciones { get; set; }
        public DbSet<SpRetencionAviso> SpRetencionAvisos { get; set; }
        public DbSet<SpRetencionItem> SpRetencionItems { get; set; }
        public DbSet<SpRetencionPago> SpRetencionPagos { get; set; }
        public DbSet<SpRetencionPrestamo> SpRetencionPrestamos { get; set; }
        public DbSet<SpImpLiquido> SpImpLiquidos { get; set; }
        public DbSet<SpMoneda> SpMonedas { get; set; }
        public DbSet<SpPrestamoEstado> SpPrestamoEstados { get; set; }
        public DbSet<SpItemPago> SpItemPagos { get; set; }
        public DbSet<SpFacturaEstado> SpFacturaEstados { get; set; }
        public DbSet<SpFacturaTipo> SpFacturaTipos { get; set; }
        public DbSet<SpCuotaEstado> SpCuotaEstados { get; set; }
        public DbSet<SpPagoOrigen> SpPagoOrigenes { get; set; }
        public DbSet<SpRetencionItemCod> SpRetencionItemCods { get; set; }
        public DbSet<SpCtrlPrestamoEstado> SpCtrlPrestamoEstados { get; set; }
        public DbSet<SpPagoParcial> SpPagoParciales { get; set; }
        public DbSet<SpPagoError> SpPagoErrores { get; set; }
        public DbSet<SpParametro> SpParametros { get; set; }
        public DbSet<SpPrestamoTipo> SpPrestamoTipos { get; set; }
        public DbSet<MapeoAbitab> MapeoAbitabs { get; set; }
        public DbSet<ErrCargaAbitab> ErrCargaAbitabs { get; set; }

        // === Shared ===
        public DbSet<Seleccion> Selecciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Primary Keys ===
            modelBuilder.Entity<ErrCargaAbitab>().HasKey(e => new { e.Fecha, e.NroReng });
            modelBuilder.Entity<MapeoAbitab>().HasKey(e => new { e.Inicio, e.Largo });
            modelBuilder.Entity<Afiliado>().HasKey(e => e.CI);
            // El CI es la cédula (la ingresa el usuario), no un surrogate: NO debe ser IDENTITY. Sin esto, EF
            // lo crea autoincremental por convención (PK simple long) y rompe el alta de afiliados.
            modelBuilder.Entity<Afiliado>().Property(e => e.CI).ValueGeneratedNever();
            modelBuilder.Entity<Seleccion>().HasKey(e => e.IdSeleccion);
            modelBuilder.Entity<Ims>().HasKey(e => new { e.Mes, e.Anio });
            modelBuilder.Entity<InformeEstadistico>().HasKey(e => e.IdRpt);
            modelBuilder.Entity<PrestacionTipo>().HasKey(e => e.CodPrestacionTipo);
            modelBuilder.Entity<RecetaDistancia>().HasKey(e => e.CodRecetaDistancia);
            modelBuilder.Entity<SubsidioCabezal>().HasKey(e => e.IdSubsidio);
            modelBuilder.Entity<SubsidioItemCodAfiliado>().HasKey(e => e.SubItmCodAfiId);
            modelBuilder.Entity<SpPrestamo>().HasKey(e => e.IDPrestamo);
            modelBuilder.Entity<SpFactura>().HasKey(e => e.IDFactura);
            modelBuilder.Entity<Trabaja>().HasKey(e => new { e.CI, e.CodEmpresa, e.FechaIngreso });
            modelBuilder.Entity<AfiliadoApunte>().HasKey(e => new { e.CI, e.Fecha });
            modelBuilder.Entity<AfiliadoEspecialidad>().HasKey(e => new { e.CI, e.CodEspecialidad });
            modelBuilder.Entity<Certificacion>().HasKey(e => e.NroLlamado);
            modelBuilder.Entity<CertificacionProrroga>().HasKey(e => new { e.CI, e.Fecha });
            modelBuilder.Entity<Prestacion>().HasKey(e => new { e.CI, e.Fecha, e.CodPrestacionTipo });
            modelBuilder.Entity<Receta>().HasKey(e => new { e.CI, e.Fecha, e.CodPrestacionTipo, e.CodRecetaDistancia });
            modelBuilder.Entity<Imponible>().HasKey(e => new { e.CI, e.CodEmpresa, e.Fechaingreso, e.Mes, e.Anio, e.Concepto });
            modelBuilder.Entity<EmpresaPago>().HasKey(e => new { e.CodEmpresa, e.Mes, e.Anio });
            modelBuilder.Entity<ReintegroMutual>().HasKey(e => new { e.CI, e.Mes, e.Anio });
            modelBuilder.Entity<SubsidioEnfermedad>().HasKey(e => new { e.IdSubsidio, e.FechaIni });
            modelBuilder.Entity<SubsidioItem>().HasKey(e => new { e.IdSubsidio, e.CodSubsidioItemCod });
            modelBuilder.Entity<SubsidioCabezalEmpresa>().HasKey(e => new { e.IdSubsidio, e.CodEmpresa });
            modelBuilder.Entity<SubsidioCabezalBps>().HasKey(e => e.IdSubsidio);
            modelBuilder.Entity<SubsidioItemEmpresa>().HasKey(e => new { e.IdSubsidio, e.CodSubsidioItemCod, e.CodEmpresa });
            modelBuilder.Entity<SubsidioImponible>().HasNoKey();
            modelBuilder.Entity<AdPreJub>().HasKey(e => e.CI);
            modelBuilder.Entity<AdPreJubPago>().HasKey(e => new { e.CI, e.Mes, e.Anio });
            modelBuilder.Entity<PrimaFallecimiento>().HasKey(e => e.CI);
            modelBuilder.Entity<FranjaIrpf>().HasKey(e => e.Desde);
            modelBuilder.Entity<GrupoEtario>().HasKey(e => e.EdadIni);
            modelBuilder.Entity<CertificacionesTmp>().HasNoKey();
            modelBuilder.Entity<NoCargadoHL>().HasKey(e => new { e.CI, e.CodEmpresa, e.Mes, e.Anio });

            modelBuilder.Entity<SpFacturaDetalle>().HasKey(e => new { e.IdFactura, e.NroReng });
            modelBuilder.Entity<SpCuota>().HasKey(e => new { e.IDPrestamo, e.Nro });
            modelBuilder.Entity<SpCuadroAmortizacion>().HasKey(e => new { e.IDPrestamo, e.NroCuota });
            modelBuilder.Entity<SpPago>().HasKey(e => e.IDFactura);
            modelBuilder.Entity<SpPagoItemPago>().HasKey(e => new { e.IDFactura, e.CodItemPago, e.NroCuota });
            modelBuilder.Entity<SpImpLiquido>().HasKey(e => new { e.CI, e.CodEmpresa, e.Fechaingreso, e.Mes, e.Anio });
            modelBuilder.Entity<SpRetencionPrestamo>().HasKey(e => e.IDPrestamo);
            modelBuilder.Entity<SpRetencion>().HasKey(e => new { e.IDPrestamo, e.Fecha });
            modelBuilder.Entity<SpRetencionAviso>().HasKey(e => new { e.IDPrestamo, e.Fecha });
            modelBuilder.Entity<SpRetencionItem>().HasKey(e => new { e.IDPrestamo, e.Fecha, e.IDFactura, e.CodRetencionItemCod });
            modelBuilder.Entity<SpRetencionPago>().HasKey(e => new { e.IDPrestamo, e.Fecha, e.Mes, e.Anio });
            modelBuilder.Entity<SpPagoError>().HasKey(e => e.IDFactura);
            modelBuilder.Entity<SpPagoParcial>().HasKey(e => new { e.IDPrestamo, e.Fecha });
            modelBuilder.Entity<SpAfiliadoComentario>().HasKey(e => new { e.CI, e.Fecha });
            modelBuilder.Entity<SpCtrlPrestamoEstado>().HasKey(e => new { e.PrestamoEstadoSig, e.PrestamoEstadoAnt });
            
            // === Lookup PKs (by code) ===
            modelBuilder.Entity<Mutualista>().HasKey(m => m.CodMutualista);
            modelBuilder.Entity<Empresa>().HasKey(e => e.CodEmpresa);
            modelBuilder.Entity<AfeccionGrupo>().HasKey(a => a.CodAfeccionGrupo);
            modelBuilder.Entity<AfeccionTipo>().HasKey(a => a.CodAfeccionTipo);
            modelBuilder.Entity<Patologia>().HasKey(p => p.CodPatologia);
            modelBuilder.Entity<Certificador>().HasKey(c => c.CodCertificador);
            modelBuilder.Entity<SalidaTipo>().HasKey(s => s.CodSalidaTipo);
            modelBuilder.Entity<BajaMotivo>().HasKey(b => b.CodBajaMotivo);
            modelBuilder.Entity<Banco>().HasKey(b => b.CodBanco);
            modelBuilder.Entity<FormaPago>().HasKey(f => f.CodFormaPago);
            modelBuilder.Entity<RegimenAporte>().HasKey(r => r.CodRegimenAporte);
            modelBuilder.Entity<RegimenJubilatorio>().HasKey(r => r.CodRegimenJubilatorio);
            modelBuilder.Entity<SituacionPago>().HasKey(s => s.CodSituacionPago);
            modelBuilder.Entity<Especialidad>().HasKey(e => e.CodEspecialidad);
            modelBuilder.Entity<SubsidioItemCod>().HasKey(s => s.CodSubsidioItemCod);
            
            // === SP Lookup PKs (by string code) ===
            modelBuilder.Entity<SpMoneda>().HasKey(m => m.CodMoneda);
            modelBuilder.Entity<SpPrestamoEstado>().HasKey(e => e.CodPrestamoEstado);
            modelBuilder.Entity<SpItemPago>().HasKey(i => i.CodItemPago);
            modelBuilder.Entity<SpFacturaEstado>().HasKey(e => e.CodFacturaEstado);
            modelBuilder.Entity<SpFacturaTipo>().HasKey(f => f.CodFacturaTipo);
            modelBuilder.Entity<SpCuotaEstado>().HasKey(e => e.CodCuotaEstado);
            modelBuilder.Entity<SpPagoOrigen>().HasKey(o => o.CodPagoOrigen);
            modelBuilder.Entity<SpRetencionItemCod>().HasKey(c => c.CodRetencionItemCod);
            modelBuilder.Entity<SpPrestamoTipo>().HasKey(t => t.CodPrestamoTipo);

            // Avoid SQL Server IDENTITY on natural/business keys coming from legacy MDBs.
            modelBuilder.Entity<Mutualista>().Property(m => m.CodMutualista).ValueGeneratedNever();
            modelBuilder.Entity<Empresa>().Property(e => e.CodEmpresa).ValueGeneratedNever();
            modelBuilder.Entity<AfeccionGrupo>().Property(a => a.CodAfeccionGrupo).ValueGeneratedNever();
            modelBuilder.Entity<AfeccionTipo>().Property(a => a.CodAfeccionTipo).ValueGeneratedNever();
            modelBuilder.Entity<Patologia>().Property(p => p.CodPatologia).ValueGeneratedNever();
            modelBuilder.Entity<Certificador>().Property(c => c.CodCertificador).ValueGeneratedNever();
            modelBuilder.Entity<SalidaTipo>().Property(s => s.CodSalidaTipo).ValueGeneratedNever();
            modelBuilder.Entity<BajaMotivo>().Property(b => b.CodBajaMotivo).ValueGeneratedNever();
            modelBuilder.Entity<Banco>().Property(b => b.CodBanco).ValueGeneratedNever();
            modelBuilder.Entity<FormaPago>().Property(f => f.CodFormaPago).ValueGeneratedNever();
            modelBuilder.Entity<RegimenAporte>().Property(r => r.CodRegimenAporte).ValueGeneratedNever();
            modelBuilder.Entity<RegimenJubilatorio>().Property(r => r.CodRegimenJubilatorio).ValueGeneratedNever();
            modelBuilder.Entity<SituacionPago>().Property(s => s.CodSituacionPago).ValueGeneratedNever();
            modelBuilder.Entity<Especialidad>().Property(e => e.CodEspecialidad).ValueGeneratedNever();
            modelBuilder.Entity<PrestacionTipo>().Property(p => p.CodPrestacionTipo).ValueGeneratedNever();
            modelBuilder.Entity<SubsidioItemCod>().Property(s => s.CodSubsidioItemCod).ValueGeneratedNever();
            modelBuilder.Entity<InformeEstadistico>().Property(i => i.IdRpt).ValueGeneratedNever();
            modelBuilder.Entity<GrupoEtario>().Property(g => g.EdadIni).ValueGeneratedNever();
            modelBuilder.Entity<SubsidioCabezal>().Property(s => s.IdSubsidio).ValueGeneratedNever();
            modelBuilder.Entity<SpPrestamo>().Property(s => s.IDPrestamo).ValueGeneratedNever();
            modelBuilder.Entity<SpFactura>().Property(s => s.IDFactura).ValueGeneratedNever();
            modelBuilder.Entity<Seleccion>().Property(s => s.IdSeleccion).ValueGeneratedNever();
            modelBuilder.Entity<Certificacion>().Property(s => s.NroLlamado).ValueGeneratedNever();
            modelBuilder.Entity<SubsidioCabezalBps>().Property(s => s.IdSubsidio).ValueGeneratedNever();
            modelBuilder.Entity<AdPreJub>().Property(s => s.CI).ValueGeneratedNever();
            modelBuilder.Entity<PrimaFallecimiento>().Property(s => s.CI).ValueGeneratedNever();
            modelBuilder.Entity<NoCargadoHL>().Property(s => s.CI).ValueGeneratedNever();
            modelBuilder.Entity<SpPago>().Property(s => s.IDFactura).ValueGeneratedNever();
            modelBuilder.Entity<SpPagoError>().Property(s => s.IDFactura).ValueGeneratedNever();
            modelBuilder.Entity<SpRetencionPrestamo>().Property(s => s.IDPrestamo).ValueGeneratedNever();
            
            // === Tables that remain keyless in source schema ===
            modelBuilder.Entity<Cuenta>().HasNoKey();
            modelBuilder.Entity<SpPrestamoComentario>().HasNoKey();
            modelBuilder.Entity<Parametros>().HasNoKey();
            modelBuilder.Entity<SpParametro>().HasNoKey();

            // === String-based Lookups ===
            modelBuilder.Entity<AporteTipo>().HasKey(a => a.CodAporteTipo);
            modelBuilder.Entity<SituacionMutual>().HasKey(s => s.CodSituacionMutual);
            modelBuilder.Entity<Departamento>().HasKey(d => d.CodDepartamento);

            // === Foreign Key Relationships ===
            
            // SP Foreign Keys
            modelBuilder.Entity<SpPrestamo>()
                .HasOne(p => p.Moneda)
                .WithMany()
                .HasForeignKey(p => p.CodMoneda)
                .HasPrincipalKey(m => m.CodMoneda);
            modelBuilder.Entity<SpPrestamo>()
                .HasOne(p => p.PrestamoEstado)
                .WithMany()
                .HasForeignKey(p => p.CodPrestamoEstado)
                .HasPrincipalKey(e => e.CodPrestamoEstado);

            modelBuilder.Entity<SpFactura>()
                .HasOne(f => f.Moneda)
                .WithMany()
                .HasForeignKey(f => f.CodMoneda)
                .HasPrincipalKey(m => m.CodMoneda);
            modelBuilder.Entity<SpFactura>()
                .HasOne(f => f.FacturaEstado)
                .WithMany()
                .HasForeignKey(f => f.CodFacturaEstado)
                .HasPrincipalKey(e => e.CodFacturaEstado);

            modelBuilder.Entity<SpFacturaDetalle>()
                .HasOne(fd => fd.ItemPago)
                .WithMany()
                .HasForeignKey(fd => fd.CodItemPago)
                .HasPrincipalKey(i => i.CodItemPago);

            modelBuilder.Entity<SpCuota>()
                .HasOne(c => c.CuotaEstado)
                .WithMany()
                .HasForeignKey(c => c.CodCuotaEstado)
                .HasPrincipalKey(e => e.CodCuotaEstado);

            modelBuilder.Entity<SpPago>()
                .HasOne(p => p.PagoOrigen)
                .WithMany()
                .HasForeignKey(p => p.CodPagoOrigen)
                .HasPrincipalKey(o => o.CodPagoOrigen);

            modelBuilder.Entity<SpRetencionItem>()
                .HasOne(r => r.RetencionItemCod)
                .WithMany()
                .HasForeignKey(r => r.CodRetencionItemCod)
                .HasPrincipalKey(c => c.CodRetencionItemCod);

            modelBuilder.Entity<SpFacturaDetalle>()
                .HasOne(fd => fd.Factura)
                .WithMany()
                .HasForeignKey(fd => fd.IdFactura)
                .HasPrincipalKey(f => f.IDFactura);
            modelBuilder.Entity<SpCuota>()
                .HasOne(c => c.Prestamo)
                .WithMany()
                .HasForeignKey(c => c.IDPrestamo)
                .HasPrincipalKey(p => p.IDPrestamo);
            modelBuilder.Entity<SpCuadroAmortizacion>()
                .HasOne(c => c.Prestamo)
                .WithMany()
                .HasForeignKey(c => c.IDPrestamo)
                .HasPrincipalKey(p => p.IDPrestamo);
            modelBuilder.Entity<SpPago>()
                .HasOne(p => p.Factura)
                .WithMany()
                .HasForeignKey(p => p.IDFactura)
                .HasPrincipalKey(f => f.IDFactura);
            modelBuilder.Entity<SpPagoItemPago>()
                .HasOne<SpFactura>()
                .WithMany()
                .HasForeignKey(p => p.IDFactura)
                .HasPrincipalKey(f => f.IDFactura);
            modelBuilder.Entity<SpRetencionPrestamo>()
                .HasOne(r => r.Prestamo)
                .WithMany()
                .HasForeignKey(r => r.IDPrestamo)
                .HasPrincipalKey(p => p.IDPrestamo);
            modelBuilder.Entity<SpRetencion>()
                .HasOne(r => r.Prestamo)
                .WithMany()
                .HasForeignKey(r => r.IDPrestamo)
                .HasPrincipalKey(p => p.IDPrestamo);
            modelBuilder.Entity<SpRetencionAviso>()
                .HasOne(r => r.Prestamo)
                .WithMany()
                .HasForeignKey(r => r.IDPrestamo)
                .HasPrincipalKey(p => p.IDPrestamo);
            modelBuilder.Entity<SpRetencionItem>()
                .HasOne<SpPrestamo>()
                .WithMany()
                .HasForeignKey(r => r.IDPrestamo)
                .HasPrincipalKey(p => p.IDPrestamo);
            modelBuilder.Entity<SpRetencionItem>()
                .HasOne<SpFactura>()
                .WithMany()
                .HasForeignKey(r => r.IDFactura)
                .HasPrincipalKey(f => f.IDFactura);
            modelBuilder.Entity<SpRetencionPago>()
                .HasOne(r => r.Prestamo)
                .WithMany()
                .HasForeignKey(r => r.IDPrestamo)
                .HasPrincipalKey(p => p.IDPrestamo);
            modelBuilder.Entity<SpPagoError>()
                .HasOne<SpFactura>()
                .WithMany()
                .HasForeignKey(p => p.IDFactura)
                .HasPrincipalKey(f => f.IDFactura);
            modelBuilder.Entity<SpPagoParcial>()
                .HasOne(p => p.Prestamo)
                .WithMany()
                .HasForeignKey(p => p.IDPrestamo)
                .HasPrincipalKey(pr => pr.IDPrestamo);
            modelBuilder.Entity<SpAfiliadoComentario>()
                .HasOne<Afiliado>()
                .WithMany()
                .HasForeignKey(p => p.CI)
                .HasPrincipalKey(a => a.CI);
            modelBuilder.Entity<SpImpLiquido>()
                .HasOne<Afiliado>()
                .WithMany()
                .HasForeignKey(i => i.CI)
                .HasPrincipalKey(a => a.CI);
            modelBuilder.Entity<SpImpLiquido>()
                .HasOne<Empresa>()
                .WithMany()
                .HasForeignKey(i => i.CodEmpresa)
                .HasPrincipalKey(e => e.CodEmpresa);

            // SGPA Foreign Keys - Afiliado relationships
            modelBuilder.Entity<AdPreJubPago>()
                .HasOne(a => a.Afiliado)
                .WithMany()
                .HasForeignKey(a => a.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<AdPreJub>()
                .HasOne(a => a.Afiliado)
                .WithMany()
                .HasForeignKey(a => a.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<AfiliadoApunte>()
                .HasOne(a => a.Afiliado)
                .WithMany()
                .HasForeignKey(a => a.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<AfiliadoEspecialidad>()
                .HasOne(a => a.Afiliado)
                .WithMany()
                .HasForeignKey(a => a.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<Trabaja>()
                .HasOne(t => t.Afiliado)
                .WithMany()
                .HasForeignKey(t => t.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Afiliado)
                .WithMany()
                .HasForeignKey(c => c.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<PrimaFallecimiento>()
                .HasOne(p => p.Afiliado)
                .WithMany()
                .HasForeignKey(p => p.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<Certificacion>()
                .HasOne(c => c.Afiliado)
                .WithMany()
                .HasForeignKey(c => c.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<CertificacionProrroga>()
                .HasOne(c => c.Afiliado)
                .WithMany()
                .HasForeignKey(c => c.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<Prestacion>()
                .HasOne(p => p.Afiliado)
                .WithMany()
                .HasForeignKey(p => p.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<Receta>()
                .HasOne(r => r.Afiliado)
                .WithMany()
                .HasForeignKey(r => r.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<Imponible>()
                .HasOne(i => i.Afiliado)
                .WithMany()
                .HasForeignKey(i => i.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<ReintegroMutual>()
                .HasOne(r => r.Afiliado)
                .WithMany()
                .HasForeignKey(r => r.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<SubsidioCabezal>()
                .HasOne(s => s.Afiliado)
                .WithMany()
                .HasForeignKey(s => s.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<SubsidioItemCodAfiliado>()
                .HasOne(s => s.Afiliado)
                .WithMany()
                .HasForeignKey(s => s.CI)
                .HasPrincipalKey(af => af.CI);
            modelBuilder.Entity<Trabaja>()
                .HasOne(t => t.Empresa)
                .WithMany()
                .HasForeignKey(t => t.CodEmpresa)
                .HasPrincipalKey(e => e.CodEmpresa);
            modelBuilder.Entity<Trabaja>()
                .HasOne(t => t.BajaMotivo)
                .WithMany()
                .HasForeignKey(t => t.CodBajaMotivo)
                .HasPrincipalKey(b => b.CodBajaMotivo);
            modelBuilder.Entity<AfiliadoEspecialidad>()
                .HasOne(a => a.Especialidad)
                .WithMany()
                .HasForeignKey(a => a.CodEspecialidad)
                .HasPrincipalKey(e => e.CodEspecialidad);
            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Banco)
                .WithMany()
                .HasForeignKey(c => c.CodBanco)
                .HasPrincipalKey(b => b.CodBanco);
            modelBuilder.Entity<Imponible>()
                .HasOne(i => i.Empresa)
                .WithMany()
                .HasForeignKey(i => i.CodEmpresa)
                .HasPrincipalKey(e => e.CodEmpresa);
            modelBuilder.Entity<ReintegroMutual>()
                .HasOne(r => r.Mutualista)
                .WithMany()
                .HasForeignKey(r => r.CodMutualista)
                .HasPrincipalKey(m => m.CodMutualista);
            modelBuilder.Entity<SubsidioCabezalEmpresa>()
                .HasOne(s => s.SubsidioCabezal)
                .WithMany()
                .HasForeignKey(s => s.IdSubsidio)
                .HasPrincipalKey(c => c.IdSubsidio);
            modelBuilder.Entity<SubsidioCabezalEmpresa>()
                .HasOne(s => s.Empresa)
                .WithMany()
                .HasForeignKey(s => s.CodEmpresa)
                .HasPrincipalKey(e => e.CodEmpresa);
            modelBuilder.Entity<SubsidioCabezalBps>()
                .HasOne(s => s.SubsidioCabezal)
                .WithMany()
                .HasForeignKey(s => s.IdSubsidio)
                .HasPrincipalKey(c => c.IdSubsidio);
            modelBuilder.Entity<SubsidioEnfermedad>()
                .HasOne(s => s.SubsidioCabezal)
                .WithMany()
                .HasForeignKey(s => s.IdSubsidio)
                .HasPrincipalKey(c => c.IdSubsidio);
            modelBuilder.Entity<SubsidioItem>()
                .HasOne(s => s.SubsidioCabezal)
                .WithMany()
                .HasForeignKey(s => s.IdSubsidio)
                .HasPrincipalKey(c => c.IdSubsidio);
            modelBuilder.Entity<SubsidioItem>()
                .HasOne(s => s.SubsidioItemCod)
                .WithMany()
                .HasForeignKey(s => s.CodSubsidioItemCod)
                .HasPrincipalKey(c => c.CodSubsidioItemCod);
            modelBuilder.Entity<SubsidioItemEmpresa>()
                .HasOne(s => s.SubsidioCabezal)
                .WithMany()
                .HasForeignKey(s => s.IdSubsidio)
                .HasPrincipalKey(c => c.IdSubsidio);
            modelBuilder.Entity<SubsidioItemEmpresa>()
                .HasOne(s => s.SubsidioItemCod)
                .WithMany()
                .HasForeignKey(s => s.CodSubsidioItemCod)
                .HasPrincipalKey(c => c.CodSubsidioItemCod);
            modelBuilder.Entity<SubsidioItemEmpresa>()
                .HasOne(s => s.Empresa)
                .WithMany()
                .HasForeignKey(s => s.CodEmpresa)
                .HasPrincipalKey(e => e.CodEmpresa);

            // XAF audit and security relationships
            modelBuilder.Entity<NewSgpa.Module.BusinessObjects.ApplicationUserLoginInfo>(b =>
            {
                b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
            });
            modelBuilder.Entity<AuditEFCoreWeakReference>()
                .HasMany(p => p.AuditItems)
                .WithOne(p => p.AuditedObject);
            modelBuilder.Entity<AuditEFCoreWeakReference>()
                .HasMany(p => p.OldItems)
                .WithOne(p => p.OldObject);
            modelBuilder.Entity<AuditEFCoreWeakReference>()
                .HasMany(p => p.NewItems)
                .WithOne(p => p.NewObject);
            modelBuilder.Entity<AuditEFCoreWeakReference>()
                .HasMany(p => p.UserItems)
                .WithOne(p => p.UserObject);
            modelBuilder.Entity<ModelDifference>()
                .HasMany(t => t.Aspects)
                .WithOne(t => t.Owner)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
