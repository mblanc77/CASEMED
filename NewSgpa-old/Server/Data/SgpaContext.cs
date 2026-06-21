using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SgpaNew.Server.Models.Sgpa;

namespace SgpaNew.Server.Data
{
    public partial class SgpaContext : DbContext
    {
        public SgpaContext()
        {
        }

        public SgpaContext(DbContextOptions<SgpaContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Cuentum>().HasNoKey();

            builder.Entity<SgpaNew.Server.Models.Sgpa.Discount>().HasNoKey();

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imp>().HasNoKey();

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>().HasNoKey();

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>().HasNoKey();

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJub>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.AdPreJubs)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJubPago>()
              .HasOne(i => i.AdPreJub)
              .WithMany(i => i.AdPreJubPagos)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>()
              .HasOne(i => i.Patologium)
              .WithMany(i => i.AfeccionGrupos)
              .HasForeignKey(i => i.CodPatologia)
              .HasPrincipalKey(i => i.CodPatologia);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionTipo>()
              .HasOne(i => i.AfeccionGrupo)
              .WithMany(i => i.AfeccionTipos)
              .HasForeignKey(i => i.CodAfeccionGrupo)
              .HasPrincipalKey(i => i.CodAfeccionGrupo);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .HasOne(i => i.Banco)
              .WithMany(i => i.Afiliados)
              .HasForeignKey(i => i.CodBanco)
              .HasPrincipalKey(i => i.CodBanco);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .HasOne(i => i.Mutualistum)
              .WithMany(i => i.Afiliados)
              .HasForeignKey(i => i.CodMutualista)
              .HasPrincipalKey(i => i.CodMutualista);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .HasOne(i => i.RegimenJubilatorio)
              .WithMany(i => i.Afiliados)
              .HasForeignKey(i => i.CodRegimenJubilatorio)
              .HasPrincipalKey(i => i.CodRegimenJubilatorio);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.AfiliadoApuntes)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.AfiliadoEspecialidads)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>()
              .HasOne(i => i.Especialidad)
              .WithMany(i => i.AfiliadoEspecialidads)
              .HasForeignKey(i => i.CodEspecialidad)
              .HasPrincipalKey(i => i.CodEspecialidad);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.Certificacions)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .HasOne(i => i.AfeccionTipo)
              .WithMany(i => i.Certificacions)
              .HasForeignKey(i => i.CodAfeccionTipo)
              .HasPrincipalKey(i => i.CodAfeccionTipo);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .HasOne(i => i.Certificador)
              .WithMany(i => i.Certificacions)
              .HasForeignKey(i => i.CodCertificador)
              .HasPrincipalKey(i => i.CodCertificador);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .HasOne(i => i.SalidaTipo)
              .WithMany(i => i.Certificacions)
              .HasForeignKey(i => i.CodSalidaTipo)
              .HasPrincipalKey(i => i.CodSalidaTipo);

            builder.Entity<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.CertificacionProrrogas)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .HasOne(i => i.RegimenAporte)
              .WithMany(i => i.Empresas)
              .HasForeignKey(i => i.CodRegimenAporte)
              .HasPrincipalKey(i => i.CodRegimenAporte);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .HasOne(i => i.SituacionPago)
              .WithMany(i => i.Empresas)
              .HasForeignKey(i => i.CodSituacionPago)
              .HasPrincipalKey(i => i.CodSituacionPago);

            builder.Entity<SgpaNew.Server.Models.Sgpa.EmpresaPago>()
              .HasOne(i => i.Empresa)
              .WithMany(i => i.EmpresaPagos)
              .HasForeignKey(i => i.CodEmpresa)
              .HasPrincipalKey(i => i.CodEmpresa);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .HasOne(i => i.Trabaja)
              .WithMany(i => i.Imponibles)
              .HasForeignKey(i => i.IdTrabaja)
              .HasPrincipalKey(i => i.IdTrabaja);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Mutualistum>()
              .HasOne(i => i.FormaPago)
              .WithMany(i => i.Mutualista)
              .HasForeignKey(i => i.CodFormaPago)
              .HasPrincipalKey(i => i.CodFormaPago);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.Prestacions)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .HasOne(i => i.PrestacionTipo)
              .WithMany(i => i.Prestacions)
              .HasForeignKey(i => i.CodPrestacionTipo)
              .HasPrincipalKey(i => i.CodPrestacionTipo);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .HasOne(i => i.Monedum)
              .WithMany(i => i.Prestacions)
              .HasForeignKey(i => i.Moneda)
              .HasPrincipalKey(i => i.Moneda1);

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.PrimaFallecimientos)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .HasOne(i => i.RecetaDistancium)
              .WithMany(i => i.Receta)
              .HasForeignKey(i => i.CodRecetaDistancia)
              .HasPrincipalKey(i => i.CodRecetaDistancia);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .HasOne(i => i.Prestacion)
              .WithMany(i => i.Receta)
              .HasForeignKey(i => i.PrestacionId)
              .HasPrincipalKey(i => i.PrestacionId);

            builder.Entity<SgpaNew.Server.Models.Sgpa.ReintegroMutual>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.ReintegroMutuals)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.ReintegroMutual>()
              .HasOne(i => i.Mutualistum)
              .WithMany(i => i.ReintegroMutuals)
              .HasForeignKey(i => i.CodMutualista)
              .HasPrincipalKey(i => i.CodMutualista);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.SubsidioCabezals)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>()
              .HasOne(i => i.SubsidioCabezal)
              .WithMany(i => i.SubsidiocabezalBps)
              .HasForeignKey(i => i.IdSubsidio)
              .HasPrincipalKey(i => i.IdSubsidio);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .HasOne(i => i.Empresa)
              .WithMany(i => i.SubsidioCabezalEmpresas)
              .HasForeignKey(i => i.CodEmpresa)
              .HasPrincipalKey(i => i.CodEmpresa);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .HasOne(i => i.SubsidioCabezal)
              .WithMany(i => i.SubsidioCabezalEmpresas)
              .HasForeignKey(i => i.IdSubsidio)
              .HasPrincipalKey(i => i.IdSubsidio);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .HasOne(i => i.SubsidioCabezal)
              .WithMany(i => i.SubsidioEnfermedads)
              .HasForeignKey(i => i.IdSubsidio)
              .HasPrincipalKey(i => i.IdSubsidio);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItem>()
              .HasOne(i => i.SubsidioItemCod)
              .WithMany(i => i.SubsidioItems)
              .HasForeignKey(i => i.CodSubsidioItemCod)
              .HasPrincipalKey(i => i.CodSubsidioItemCod);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItem>()
              .HasOne(i => i.SubsidioCabezal)
              .WithMany(i => i.SubsidioItems)
              .HasForeignKey(i => i.IdSubsidio)
              .HasPrincipalKey(i => i.IdSubsidio);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>()
              .HasOne(i => i.Empresa)
              .WithMany(i => i.SubsidioItemEmpresas)
              .HasForeignKey(i => i.CodEmpresa)
              .HasPrincipalKey(i => i.CodEmpresa);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .HasOne(i => i.Afiliado)
              .WithMany(i => i.Trabajas)
              .HasForeignKey(i => i.CI)
              .HasPrincipalKey(i => i.CI);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .HasOne(i => i.BajaMotivo)
              .WithMany(i => i.Trabajas)
              .HasForeignKey(i => i.CodBajaMotivo)
              .HasPrincipalKey(i => i.CodBajaMotivo);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .HasOne(i => i.Empresa)
              .WithMany(i => i.Trabajas)
              .HasForeignKey(i => i.CodEmpresa)
              .HasPrincipalKey(i => i.CodEmpresa);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJub>()
              .Property(p => p.ImporteMensual)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJubPago>()
              .Property(p => p.Mes)
              .HasDefaultValueSql(@"(datepart(month,CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1))))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJubPago>()
              .Property(p => p.Anio)
              .HasDefaultValueSql(@"(datepart(year,CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1))))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJubPago>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>()
              .Property(p => p.CodPatologia)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionTipo>()
              .Property(p => p.CodAfeccionGrupo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionTipo>()
              .Property(p => p.CodDiameg)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.CodMutualista)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.CodRegimenJubilatorio)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.CodDepartamento)
              .HasDefaultValueSql(@"('NDF')");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.PagaMutualista)
              .HasDefaultValueSql(@"((1))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.CodBanco)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.CI)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.CodAfeccionTipo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.CodCertificador)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.CodSalidaTipo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.Efectiva)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.ImporteDeducible)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.Trabaja)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>()
              .Property(p => p.Dias)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificador>()
              .Property(p => p.CobraLlamado)
              .HasDefaultValueSql(@"((1))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Cuentum>()
              .Property(p => p.CI)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Cuentum>()
              .Property(p => p.CodBanco)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .Property(p => p.AporteCasemed)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .Property(p => p.AporteAguinaldo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .Property(p => p.CodRegimenAporte)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .Property(p => p.CodSituacionPago)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .Property(p => p.Liquidar)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .Property(p => p.Ficticia)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.EmpresaPago>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.FranjaIrpf>()
              .Property(p => p.Hasta)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.FranjaIrpf>()
              .Property(p => p.Porcentaje)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imp>()
              .Property(p => p.CI)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imp>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .Property(p => p.IdTrabaja)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .Property(p => p.DiasTrabajados)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .Property(p => p.AnioMes)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.InformeEstadistico>()
              .Property(p => p.Orden)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.InformeEstadistico>()
              .Property(p => p.MesAnio)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.InformeEstadistico>()
              .Property(p => p.Periodo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.InformeEstadistico>()
              .Property(p => p.Empresa)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.InformeEstadistico>()
              .Property(p => p.Fecha)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.InformeEstadistico>()
              .Property(p => p.GrupoEtario)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.InformeEstadistico>()
              .Property(p => p.Patologia)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.MES)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.ANIO)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.LIQUIDO)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.NroCuenta)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.EstCivil)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.CodCargo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.InfDbla)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.AsigCuenta)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Mutualistum>()
              .Property(p => p.DiaPago)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Mutualistum>()
              .Property(p => p.CodFormaPago)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Mutualistum>()
              .Property(p => p.Cuota)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Mutualistum>()
              .Property(p => p.Ficticia)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>()
              .Property(p => p.SMN)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>()
              .Property(p => p.TopeJubilatorio)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>()
              .Property(p => p.TopePrima)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>()
              .Property(p => p.UR)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>()
              .Property(p => p.PctAdPreJub)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>()
              .Property(p => p.BCP)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>()
              .Property(p => p.TopeLiquidoBPS)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Parametro>()
              .Property(p => p.pctBPS)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .Property(p => p.Boleta)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrestacionTipo>()
              .Property(p => p.ImporteTopeDISSE)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrestacionTipo>()
              .Property(p => p.ImporteTopeCASEMED)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrestacionTipo>()
              .Property(p => p.PeriodoRenovacion)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrestacionTipo>()
              .Property(p => p.Receta)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .Property(p => p.Esf_I)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .Property(p => p.Esf_D)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .Property(p => p.Cil_I)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .Property(p => p.Cil_D)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.RegimenAporte>()
              .Property(p => p.Porcentaje)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.ReintegroMutual>()
              .Property(p => p.CodMutualista)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.ReintegroMutual>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Seleccion>()
              .Property(p => p.System)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SituacionMutual>()
              .Property(p => p.Pagar)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.Mes)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.Anio)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.CI)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.Liquidar)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.ValorJornal)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.Dias)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.ImpNominal)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.ImpAguinaldo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.ImpLiquido)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.NroRecibo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.CodBanco)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>()
              .Property(p => p.DiasBPS)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>()
              .Property(p => p.LiquidoBPS)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>()
              .Property(p => p.AguinaldoBPS)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>()
              .Property(p => p.LiquidoPagar)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .Property(p => p.ValorJornal)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .Property(p => p.Dias)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .Property(p => p.ImpNominal)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .Property(p => p.ImpAguinaldo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .Property(p => p.ImpLiquido)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.NroLlamado)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.Dias)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>()
              .Property(p => p.IdSubsidio)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>()
              .Property(p => p.Mes)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>()
              .Property(p => p.Anio)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>()
              .Property(p => p.CodEmpresa)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>()
              .Property(p => p.Dias)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItem>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItem>()
              .Property(p => p.AbiEmp)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.Signo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.Comparar)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.CompararContra)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.Valor)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.ValorMin)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.ValorMax)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.Procesar)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.AperturaXEmpresa)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.ModificaNominal)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>()
              .Property(p => p.CodSubsidioItemCod)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>()
              .Property(p => p.CI)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>()
              .Property(p => p.Valor)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>()
              .Property(p => p.Importe)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.CodBajaMotivo)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJub>()
              .Property(p => p.FechaPresentacion)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJub>()
              .Property(p => p.FechaJubilacion)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJub>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJubPago>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJubPago>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionTipo>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.FechaNacimiento)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.FechaIngMutualista)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.FechaBajaMutualista)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AporteTipo>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.BajaMotivo>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Banco>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.FechaRecibido)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.FechaCertificacion)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.FechaIni)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.FechaFin)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificador>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.EmpresaPago>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Especialidad>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.FormaPago>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .Property(p => p.Fechaingreso)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.FECHA_PER_DESDE)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.FECHA_PER_HASTA)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.FECHA_DE_ENTREGA)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.FecNac)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.FecIngreso)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.FecInfDbla)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.FecAsigCta)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Mutualistum>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.NoCargadoHl>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Patologium>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrestacionTipo>()
              .Property(p => p.FechaVigencia)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrestacionTipo>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.FechaFirma)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.FechaFallecimiento)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.FechaPago)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.RegimenAporte>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.ReintegroMutual>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.ReintegroMutual>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SalidaTipo>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SituacionMutual>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SituacionPago>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.FechaPago)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.FechaIni)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.FechaFin)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.FechaIniSubsidio)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.FechaFinSubsidio)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItem>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.FechaVigencia)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.FechaBaja)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>()
              .Property(p => p.Vigencia)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.FechaIngreso)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.FechaBaja)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.FechaIngCasemed)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.Ts)
              .HasColumnType("datetime2(0)");

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJub>()
              .Property(p => p.FechaPresentacion)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJub>()
              .Property(p => p.FechaJubilacion)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJub>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJubPago>()
              .Property(p => p.Fecha)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AdPreJubPago>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfeccionTipo>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.FechaNacimiento)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.FechaIngMutualista)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.FechaBajaMutualista)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Afiliado>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>()
              .Property(p => p.Fecha)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.AporteTipo>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.BajaMotivo>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Banco>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.FechaRecibido)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.FechaCertificacion)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.FechaIni)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.FechaFin)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificacion>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>()
              .Property(p => p.Fecha)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Certificador>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Empresa>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.EmpresaPago>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Especialidad>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.FormaPago>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .Property(p => p.Fechaingreso)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Imponible>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.FECHA_PER_DESDE)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.FECHA_PER_HASTA)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.LiquidacionBp>()
              .Property(p => p.FECHA_DE_ENTREGA)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.FecNac)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.FecIngreso)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.FecInfDbla)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.MaeFun>()
              .Property(p => p.FecAsigCta)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Mutualistum>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.NoCargadoHl>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Patologium>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .Property(p => p.Fecha)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Prestacion>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrestacionTipo>()
              .Property(p => p.FechaVigencia)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrestacionTipo>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.FechaFirma)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.FechaFallecimiento)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.FechaPago)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .Property(p => p.Fecha)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Recetum>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.RegimenAporte>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.ReintegroMutual>()
              .Property(p => p.Fecha)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.ReintegroMutual>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SalidaTipo>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SituacionMutual>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SituacionPago>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.FechaPago)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.FechaIni)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.FechaFin)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.FechaIniSubsidio)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.FechaFinSubsidio)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioImponible>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItem>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.FechaVigencia)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>()
              .Property(p => p.FechaBaja)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>()
              .Property(p => p.Vigencia)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>()
              .Property(p => p.Ts)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.FechaIngreso)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.FechaBaja)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.FechaIngCasemed)
              .HasPrecision(0);

            builder.Entity<SgpaNew.Server.Models.Sgpa.Trabaja>()
              .Property(p => p.Ts)
              .HasPrecision(0);
            this.OnModelBuilding(builder);
        }

        public DbSet<SgpaNew.Server.Models.Sgpa.AdPreJub> AdPreJubs { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.AdPreJubPago> AdPreJubPagos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> AfeccionGrupos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.AfeccionTipo> AfeccionTipos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Afiliado> Afiliados { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> AfiliadoApuntes { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> AfiliadoEspecialidads { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.AporteTipo> AporteTipos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.BajaMotivo> BajaMotivos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Banco> Bancos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Certificacion> Certificacions { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> CertificacionProrrogas { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Certificador> Certificadors { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Ctasbrou> Ctasbrous { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Cuentum> Cuenta { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Departamento> Departamentos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Discount> Discounts { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Empresa> Empresas { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.EmpresaPago> EmpresaPagos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Especialidad> Especialidads { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.FormaPago> FormaPagos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.FranjaIrpf> FranjaIrpfs { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Imp> Imps { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Imponible> Imponibles { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.InformeEstadistico> InformeEstadisticos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.LiquidacionBp> LiquidacionBps { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.MaeFun> MaeFuns { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Monedum> Moneda { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Mutualistum> Mutualista { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.NoCargadoHl> NoCargadoHls { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Parametro> Parametros { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Patologium> Patologia { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Prestacion> Prestacions { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.PrestacionTipo> PrestacionTipos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> PrimaFallecimientos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Recetum> Receta { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.RecetaDistancium> RecetaDistancia { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.RegimenAporte> RegimenAportes { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> RegimenJubilatorios { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.ReintegroMutual> ReintegroMutuals { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SalidaTipo> SalidaTipos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Seleccion> Seleccions { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SituacionMutual> SituacionMutuals { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SituacionPago> SituacionPagos { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> SubsidioCabezals { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> SubsidiocabezalBps { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> SubsidioCabezalEmpresas { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> SubsidioEnfermedads { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidioImponible> SubsidioImponibles { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidioItem> SubsidioItems { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> SubsidioItemCods { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> SubsidioitemcodAfiliados { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> SubsidioItemEmpresas { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.Trabaja> Trabajas { get; set; }

        public DbSet<SgpaNew.Server.Models.Sgpa.XUsrParam> XUsrParams { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}