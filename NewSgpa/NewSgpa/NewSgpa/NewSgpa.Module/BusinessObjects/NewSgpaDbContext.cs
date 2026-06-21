using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.ExpressApp.EFCore.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NewSgpa.Module.BusinessObjects.Sgpa;
using NewSgpa.Module.BusinessObjects.Prestamos;
using NewSgpa.Module.BusinessObjects.Shared;

namespace NewSgpa.Module.BusinessObjects
{
    [TypesInfoInitializer(typeof(DbContextTypesInfoInitializer<NewSgpaEFCoreDbContext>))]
    public class NewSgpaEFCoreDbContext : DbContext
    {
        public NewSgpaEFCoreDbContext(DbContextOptions<NewSgpaEFCoreDbContext> options) : base(options)
        {
        }
        //public DbSet<ModuleInfo> ModulesInfo { get; set; }
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
            modelBuilder.UseDeferredDeletion(this);
            modelBuilder.UseOptimisticLock();
            modelBuilder.SetOneToManyAssociationDeleteBehavior(DeleteBehavior.SetNull, DeleteBehavior.Cascade);
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
            modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
            modelBuilder.Entity<ErrCargaAbitab>().HasKey(e => new { e.Fecha, e.NroReng });
            modelBuilder.Entity<MapeoAbitab>().HasKey(e => new { e.Inicio, e.Largo });
            modelBuilder.Entity<SpMoneda>().HasKey(e => e.CodMoneda);
            modelBuilder.Entity<SpPrestamoEstado>().HasKey(e => e.CodPrestamoEstado);
            modelBuilder.Entity<SpItemPago>().HasKey(e => e.CodItemPago);
            modelBuilder.Entity<SpFacturaEstado>().HasKey(e => e.CodFacturaEstado);
            modelBuilder.Entity<SpFacturaTipo>().HasKey(e => e.CodFacturaTipo);
            modelBuilder.Entity<SpCuotaEstado>().HasKey(e => e.CodCuotaEstado);
            modelBuilder.Entity<SpPagoOrigen>().HasKey(e => e.CodPagoOrigen);
            modelBuilder.Entity<SpRetencionItemCod>().HasKey(e => e.CodRetencionItemCod);
            modelBuilder.Entity<SpPrestamoTipo>().HasKey(e => e.CodPrestamoTipo);

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

            modelBuilder.Entity<Afiliado>().HasKey(e => e.CI);
            // El CI es la cédula (la ingresa el usuario), no un surrogate: NO debe ser IDENTITY.
            modelBuilder.Entity<Afiliado>().Property(e => e.CI).ValueGeneratedNever();
            modelBuilder.Entity<Mutualista>().HasKey(e => e.CodMutualista);
            modelBuilder.Entity<Empresa>().HasKey(e => e.CodEmpresa);
            modelBuilder.Entity<AfeccionGrupo>().HasKey(e => e.CodAfeccionGrupo);
            modelBuilder.Entity<AfeccionTipo>().HasKey(e => e.CodAfeccionTipo);
            modelBuilder.Entity<Patologia>().HasKey(e => e.CodPatologia);
            modelBuilder.Entity<Certificador>().HasKey(e => e.CodCertificador);
            modelBuilder.Entity<SalidaTipo>().HasKey(e => e.CodSalidaTipo);
            modelBuilder.Entity<BajaMotivo>().HasKey(e => e.CodBajaMotivo);
            modelBuilder.Entity<Banco>().HasKey(e => e.CodBanco);
            modelBuilder.Entity<FormaPago>().HasKey(e => e.CodFormaPago);
            modelBuilder.Entity<RegimenAporte>().HasKey(e => e.CodRegimenAporte);
            modelBuilder.Entity<RegimenJubilatorio>().HasKey(e => e.CodRegimenJubilatorio);
            modelBuilder.Entity<AporteTipo>().HasKey(e => e.CodAporteTipo);
            modelBuilder.Entity<SituacionPago>().HasKey(e => e.CodSituacionPago);
            modelBuilder.Entity<SituacionMutual>().HasKey(e => e.CodSituacionMutual);
            modelBuilder.Entity<Departamento>().HasKey(e => e.CodDepartamento);
            modelBuilder.Entity<Especialidad>().HasKey(e => e.CodEspecialidad);
            modelBuilder.Entity<SubsidioItemCod>().HasKey(e => e.CodSubsidioItemCod);

            // Keyless transactional entities
            modelBuilder.Entity<Trabaja>().HasNoKey();
            modelBuilder.Entity<Cuenta>().HasNoKey();
            modelBuilder.Entity<AfiliadoApunte>().HasNoKey();
            modelBuilder.Entity<AfiliadoEspecialidad>().HasNoKey();
            modelBuilder.Entity<Certificacion>().HasNoKey();
            modelBuilder.Entity<CertificacionProrroga>().HasNoKey();
            modelBuilder.Entity<Prestacion>().HasNoKey();
            modelBuilder.Entity<Receta>().HasNoKey();
            modelBuilder.Entity<Imponible>().HasNoKey();
            modelBuilder.Entity<EmpresaPago>().HasNoKey();
            modelBuilder.Entity<ReintegroMutual>().HasNoKey();
            modelBuilder.Entity<SubsidioEnfermedad>().HasNoKey();
            modelBuilder.Entity<SubsidioItem>().HasNoKey();
            modelBuilder.Entity<SubsidioCabezalEmpresa>().HasNoKey();
            modelBuilder.Entity<SubsidioCabezalBps>().HasNoKey();
            modelBuilder.Entity<SubsidioItemEmpresa>().HasNoKey();
            modelBuilder.Entity<SubsidioImponible>().HasNoKey();
            modelBuilder.Entity<SpFacturaDetalle>().HasNoKey();
            modelBuilder.Entity<SpCuota>().HasNoKey();
            modelBuilder.Entity<SpCuadroAmortizacion>().HasNoKey();
            modelBuilder.Entity<SpPago>().HasNoKey();
            modelBuilder.Entity<SpPagoItemPago>().HasNoKey();
            modelBuilder.Entity<SpImpLiquido>().HasNoKey();
            modelBuilder.Entity<SpRetencionPrestamo>().HasNoKey();
            modelBuilder.Entity<SpRetencion>().HasNoKey();
            modelBuilder.Entity<SpRetencionAviso>().HasNoKey();
            modelBuilder.Entity<SpRetencionItem>().HasNoKey();
            modelBuilder.Entity<SpRetencionPago>().HasNoKey();
            modelBuilder.Entity<SpPagoError>().HasNoKey();
            modelBuilder.Entity<SpPagoParcial>().HasNoKey();
            modelBuilder.Entity<SpAfiliadoComentario>().HasNoKey();
            modelBuilder.Entity<SpPrestamoComentario>().HasNoKey();
            modelBuilder.Entity<SpCtrlPrestamoEstado>().HasNoKey();
            modelBuilder.Entity<SpParametro>().HasNoKey();
            modelBuilder.Entity<AdPreJub>().HasNoKey();
            modelBuilder.Entity<AdPreJubPago>().HasNoKey();
            modelBuilder.Entity<PrimaFallecimiento>().HasNoKey();
            modelBuilder.Entity<FranjaIrpf>().HasNoKey();
            modelBuilder.Entity<GrupoEtario>().HasKey(e => e.EdadIni);
            modelBuilder.Entity<CertificacionesTmp>().HasNoKey();
            modelBuilder.Entity<Parametros>().HasNoKey();
            modelBuilder.Entity<NoCargadoHL>().HasNoKey();
            modelBuilder.Entity<Seleccion>().HasKey(e => e.IdSeleccion);
            modelBuilder.Entity<Ims>().HasKey(e => new { e.Mes, e.Anio });
            modelBuilder.Entity<InformeEstadistico>().HasKey(e => e.IdRpt);
            modelBuilder.Entity<PrestacionTipo>().HasKey(e => e.CodPrestacionTipo);
            modelBuilder.Entity<RecetaDistancia>().HasKey(e => e.CodRecetaDistancia);
            modelBuilder.Entity<SubsidioCabezal>().HasKey(e => e.IdSubsidio);
            modelBuilder.Entity<SubsidioItemCodAfiliado>().HasKey(e => e.SubItmCodAfiId);
            modelBuilder.Entity<SpPrestamo>().HasKey(e => e.IDPrestamo);
            modelBuilder.Entity<SpFactura>().HasKey(e => e.IDFactura);
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

            // ============================================================
            // Alternate keys (original Access composite PKs preserved as unique indexes)
            // ============================================================

            // SGPA: Afiliado - CI is the original Access PK
            modelBuilder.Entity<Afiliado>()
                .HasAlternateKey(a => a.CI);

            modelBuilder.Entity<SubsidioCabezal>()
                .HasAlternateKey(s => s.IdSubsidio);
            modelBuilder.Entity<InformeEstadistico>()
                .HasAlternateKey(i => i.IdRpt);

            // SP: Alternate keys
            modelBuilder.Entity<SpPrestamo>()
                .HasAlternateKey(p => p.IDPrestamo);
            modelBuilder.Entity<SpFactura>()
                .HasAlternateKey(f => f.IDFactura);

            // Entities with string-based codes: use unique indexes
            modelBuilder.Entity<AporteTipo>()
                .HasIndex(a => a.CodAporteTipo).IsUnique();
            modelBuilder.Entity<SituacionMutual>()
                .HasIndex(s => s.CodSituacionMutual).IsUnique();
            modelBuilder.Entity<Departamento>()
                .HasIndex(d => d.CodDepartamento).IsUnique();
            modelBuilder.Entity<PrestacionTipo>()
                .HasIndex(p => p.CodPrestacionTipo).IsUnique();
            modelBuilder.Entity<RecetaDistancia>()
                .HasIndex(r => r.CodRecetaDistancia).IsUnique();
            modelBuilder.Entity<SpMoneda>()
                .HasIndex(m => m.CodMoneda).IsUnique();
            modelBuilder.Entity<SpPrestamoEstado>()
                .HasIndex(e => e.CodPrestamoEstado).IsUnique();
            modelBuilder.Entity<SpItemPago>()
                .HasIndex(i => i.CodItemPago).IsUnique();
            modelBuilder.Entity<SpFacturaEstado>()
                .HasIndex(f => f.CodFacturaEstado).IsUnique();
            modelBuilder.Entity<SpCuotaEstado>()
                .HasIndex(c => c.CodCuotaEstado).IsUnique();
            modelBuilder.Entity<SpPagoOrigen>()
                .HasIndex(p => p.CodPagoOrigen).IsUnique();
            modelBuilder.Entity<SpRetencionItemCod>()
                .HasIndex(r => r.CodRetencionItemCod).IsUnique();

            // Composite unique indexes for entities with original composite PKs
            modelBuilder.Entity<Trabaja>()
                .HasIndex(t => new { t.CI, t.CodEmpresa, t.FechaIngreso }).IsUnique();
            modelBuilder.Entity<Imponible>()
                .HasIndex(i => new { i.CI, i.CodEmpresa, i.Mes, i.Anio, i.Concepto }).IsUnique();
            modelBuilder.Entity<EmpresaPago>()
                .HasIndex(e => new { e.CodEmpresa, e.Mes, e.Anio }).IsUnique();
            modelBuilder.Entity<Ims>()
                .HasIndex(i => new { i.Mes, i.Anio }).IsUnique();
        }
    }

    public class NewSgpaAuditingDbContext : DbContext
    {
        public NewSgpaAuditingDbContext(DbContextOptions<NewSgpaAuditingDbContext> options) : base(options)
        {
        }
        public DbSet<AuditDataItemPersistent> AuditData { get; set; }
        public DbSet<AuditEFCoreWeakReference> AuditEFCoreWeakReferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseDeferredDeletion(this);
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
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
        }
    }
}



