using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using SGPA.Server.Models.CMU;

namespace SGPA.Server.Data
{
    public partial class CMUContext : DbContext
    {
        public CMUContext()
        {
        }

        public CMUContext(DbContextOptions<CMUContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SGPA.Server.Models.CMU.TmpMese>().HasKey(table => new {
                table.Mes, table.Año
            });

            builder.Entity<SGPA.Server.Models.CMU.ActaConsejo>()
              .HasOne(i => i.FileDatum)
              .WithMany(i => i.ActaConsejos)
              .HasForeignKey(i => i.Archivo)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.AgenteCobranza>()
              .HasOne(i => i.AgenteCobranzaTipo)
              .WithMany(i => i.AgenteCobranzas)
              .HasForeignKey(i => i.AgenteTipo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AgenteCobranza>()
              .HasOne(i => i.CuentaBancarium)
              .WithMany(i => i.AgenteCobranzas)
              .HasForeignKey(i => i.CuentaBancaria)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AgenteCobranza>()
              .HasOne(i => i.Departamento1)
              .WithMany(i => i.AgenteCobranzas)
              .HasForeignKey(i => i.Departamento)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AgenteCobranza>()
              .HasOne(i => i.AgenteGrupo)
              .WithMany(i => i.AgenteCobranzas)
              .HasForeignKey(i => i.Grupo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AgenteCobranza>()
              .HasOne(i => i.OrigenMovimiento)
              .WithMany(i => i.AgenteCobranzas)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AgenteCobranza>()
              .HasOne(i => i.Region1)
              .WithMany(i => i.AgenteCobranzas)
              .HasForeignKey(i => i.Region)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AgenteCobranzaDebito>()
              .HasOne(i => i.AgenteCobranza)
              .WithMany(i => i.AgenteCobranzaDebitos)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AjusteDetalle>()
              .HasOne(i => i.AjusteRetroactivo)
              .WithMany(i => i.AjusteDetalles)
              .HasForeignKey(i => i.Ajuste)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AjusteRetroactivo>()
              .HasOne(i => i.AjusteRetroactivo1)
              .WithMany(i => i.AjusteRetroactivos1)
              .HasForeignKey(i => i.AnulaA)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AjusteRetroactivo>()
              .HasOne(i => i.ColegiadoCambioCategorium)
              .WithMany(i => i.AjusteRetroactivos)
              .HasForeignKey(i => i.CambioCategoria)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AjusteRetroactivo>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.AjusteRetroactivos)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.AjusteRetroactivo>()
              .HasOne(i => i.ColegiadoDeclaracionJuradum)
              .WithMany(i => i.AjusteRetroactivos)
              .HasForeignKey(i => i.DeclaracionJurada)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.AuditDataItemPersistent>()
              .HasOne(i => i.AuditedObjectWeakReference)
              .WithMany(i => i.AuditDataItemPersistents)
              .HasForeignKey(i => i.AuditedObject)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.AuditDataItemPersistent>()
              .HasOne(i => i.XpweakReference)
              .WithMany(i => i.AuditDataItemPersistents)
              .HasForeignKey(i => i.NewObject)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.AuditDataItemPersistent>()
              .HasOne(i => i.XpweakReference1)
              .WithMany(i => i.AuditDataItemPersistents1)
              .HasForeignKey(i => i.OldObject)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.AuditedObjectWeakReference>()
              .HasOne(i => i.XpweakReference)
              .WithMany(i => i.AuditedObjectWeakReferences)
              .HasForeignKey(i => i.Oid)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.CategoriaColegiado>()
              .HasOne(i => i.CategoriaColegiado1)
              .WithMany(i => i.CategoriaColegiados1)
              .HasForeignKey(i => i.CategoriaDependiente)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.CategoriaColegiadoValor>()
              .HasOne(i => i.CategoriaColegiado1)
              .WithMany(i => i.CategoriaColegiadoValors)
              .HasForeignKey(i => i.CategoriaColegiado)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Cobro>()
              .HasOne(i => i.AgenteCobranza1)
              .WithMany(i => i.Cobros)
              .HasForeignKey(i => i.AgenteCobranza)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Cobro>()
              .HasOne(i => i.CuentaBancarium)
              .WithMany(i => i.Cobros)
              .HasForeignKey(i => i.CuentaBancaria)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Cobro>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.Cobros)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.CobroNomina>()
              .HasOne(i => i.Cobro1)
              .WithMany(i => i.CobroNominas)
              .HasForeignKey(i => i.Cobro)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.CobroNomina>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.CobroNominas)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.CobroNomina>()
              .HasOne(i => i.Convenio1)
              .WithMany(i => i.CobroNominas)
              .HasForeignKey(i => i.Convenio)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.CobroNomina>()
              .HasOne(i => i.MovimientoCuentum)
              .WithMany(i => i.CobroNominas)
              .HasForeignKey(i => i.Movimiento)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.CobroNomina>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.CobroNominas)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .HasOne(i => i.AgenteCobranza)
              .WithMany(i => i.Colegiados)
              .HasForeignKey(i => i.AgenteCobro)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .HasOne(i => i.BajaMotivo1)
              .WithMany(i => i.Colegiados)
              .HasForeignKey(i => i.BajaMotivo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .HasOne(i => i.CategoriaColegiado1)
              .WithMany(i => i.Colegiados)
              .HasForeignKey(i => i.CategoriaColegiado)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .HasOne(i => i.Departamento1)
              .WithMany(i => i.Colegiados)
              .HasForeignKey(i => i.Departamento)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .HasOne(i => i.Pai)
              .WithMany(i => i.Colegiados)
              .HasForeignKey(i => i.PaisTitulo)
              .HasPrincipalKey(i => i.Codigo);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .HasOne(i => i.Regional)
              .WithMany(i => i.Colegiados)
              .HasForeignKey(i => i.RegionalTrabaja)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .HasOne(i => i.AgenteCobranza1)
              .WithMany(i => i.Colegiados1)
              .HasForeignKey(i => i.UltimoAgenteCobro)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .HasOne(i => i.UniversidadTituloGrado1)
              .WithMany(i => i.Colegiados)
              .HasForeignKey(i => i.UniversidadTituloGrado)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.ColegiadoActualizacionDps)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoBitacora>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.ColegiadoBitacoras)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoBitacora>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.ColegiadoBitacoras)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>()
              .HasOne(i => i.ColegiadoBitacoraNotum)
              .WithMany(i => i.ColegiadoBitacoraEMailEnvios)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>()
              .HasOne(i => i.ColegiadoBitacoraNotum)
              .WithMany(i => i.ColegiadoBitacoraEMailRecepcions)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>()
              .HasOne(i => i.ColegiadoBitacora)
              .WithMany(i => i.ColegiadoBitacoraNota)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>()
              .HasOne(i => i.CategoriaColegiado)
              .WithMany(i => i.ColegiadoCambioCategoria)
              .HasForeignKey(i => i.Categoria)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.ColegiadoCambioCategoria)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.ColegiadoCertificadoExpedidos)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>()
              .HasOne(i => i.AgenteCobranzaDebito)
              .WithMany(i => i.ColegiadoDebitoBancarioAsociados)
              .HasForeignKey(i => i.AgenteDebito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.ColegiadoDebitoBancarioAsociados)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.ColegiadoDeclaracionJurada)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .HasOne(i => i.DjinactividadMotivo)
              .WithMany(i => i.ColegiadoDeclaracionJurada)
              .HasForeignKey(i => i.MotivoInactividad)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.ColegiadoDeclaracionJurada)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .HasOne(i => i.DeclaracionJuradaTipo)
              .WithMany(i => i.ColegiadoDeclaracionJurada)
              .HasForeignKey(i => i.Tipo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoImagene>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.ColegiadoImagenes)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>()
              .HasOne(i => i.AgenteCobranzaDebito)
              .WithMany(i => i.ColegiadoTarjetaDebitoAsociada)
              .HasForeignKey(i => i.AgenteDebito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.ColegiadoTarjetaDebitoAsociada)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.Contacto>()
              .HasOne(i => i.AreaContacto)
              .WithMany(i => i.Contactos)
              .HasForeignKey(i => i.Area)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Contacto>()
              .HasOne(i => i.CargoContacto)
              .WithMany(i => i.Contactos)
              .HasForeignKey(i => i.Cargo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Contacto>()
              .HasOne(i => i.GrupoContacto)
              .WithMany(i => i.Contactos)
              .HasForeignKey(i => i.Grupo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.ContactoInfoAdicional>()
              .HasOne(i => i.Contacto1)
              .WithMany(i => i.ContactoInfoAdicionals)
              .HasForeignKey(i => i.Contacto)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Convenio>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.Convenios)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.Convenio>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.Convenios)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.ConvenioFinanciacion>()
              .HasOne(i => i.Convenio)
              .WithMany(i => i.ConvenioFinanciacions)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.CuentaBancarium>()
              .HasOne(i => i.Banco1)
              .WithMany(i => i.CuentaBancaria)
              .HasForeignKey(i => i.Banco)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Debito>()
              .HasOne(i => i.AgenteCobranzaDebito)
              .WithMany(i => i.Debitos)
              .HasForeignKey(i => i.AgenteDebito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Debito>()
              .HasOne(i => i.Cobro)
              .WithMany(i => i.Debitos)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DebitoAdjunto>()
              .HasOne(i => i.Debito1)
              .WithMany(i => i.DebitoAdjuntos)
              .HasForeignKey(i => i.Debito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DebitoAdjunto>()
              .HasOne(i => i.FileDatum)
              .WithMany(i => i.DebitoAdjuntos)
              .HasForeignKey(i => i.FileData)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.DebitoNomina>()
              .HasOne(i => i.Debito1)
              .WithMany(i => i.DebitoNominas)
              .HasForeignKey(i => i.Debito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DebitoNomina>()
              .HasOne(i => i.CobroNomina)
              .WithMany(i => i.DebitoNominas)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>()
              .HasOne(i => i.ColegiadoDeclaracionJuradum)
              .WithMany(i => i.DeclaracionJuradaAdjuntos)
              .HasForeignKey(i => i.Declaracion)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>()
              .HasOne(i => i.FileDatum)
              .WithMany(i => i.DeclaracionJuradaAdjuntos)
              .HasForeignKey(i => i.FileData)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>()
              .HasOne(i => i.CategoriaColegiado)
              .WithMany(i => i.DeclaracionJuradaTipos)
              .HasForeignKey(i => i.Categoria)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Departamento>()
              .HasOne(i => i.Regional1)
              .WithMany(i => i.Departamentos)
              .HasForeignKey(i => i.Regional)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Deposito>()
              .HasOne(i => i.Cobro)
              .WithMany(i => i.Depositos)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DepositoNomina>()
              .HasOne(i => i.Deposito1)
              .WithMany(i => i.DepositoNominas)
              .HasForeignKey(i => i.Deposito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DepositoNomina>()
              .HasOne(i => i.CobroNomina)
              .WithMany(i => i.DepositoNominas)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>()
              .HasOne(i => i.DepositoNomina)
              .WithMany(i => i.DepositoNominaMultiBrous)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>()
              .HasOne(i => i.Deposito1)
              .WithMany(i => i.DepositoNominaNoIdentificada)
              .HasForeignKey(i => i.Deposito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.DepositoNominaRedPago>()
              .HasOne(i => i.DepositoNomina)
              .WithMany(i => i.DepositoNominaRedPagos)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.KpiDefinition>()
              .HasOne(i => i.KpiInstance1)
              .WithMany(i => i.KpiDefinitions)
              .HasForeignKey(i => i.KpiInstance)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.KpiHistoryItem>()
              .HasOne(i => i.KpiInstance1)
              .WithMany(i => i.KpiHistoryItems)
              .HasForeignKey(i => i.KpiInstance)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.KpiInstance>()
              .HasOne(i => i.KpiDefinition1)
              .WithMany(i => i.KpiInstances)
              .HasForeignKey(i => i.KpiDefinition)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>()
              .HasOne(i => i.KpiInstance)
              .WithMany(i => i.KpiscorecardscorecardsKpiinstanceindicators)
              .HasForeignKey(i => i.Indicators)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>()
              .HasOne(i => i.KpiScorecard)
              .WithMany(i => i.KpiscorecardscorecardsKpiinstanceindicators)
              .HasForeignKey(i => i.Scorecards)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.LugarRetiroCarne>()
              .HasOne(i => i.Departamento1)
              .WithMany(i => i.LugarRetiroCarnes)
              .HasForeignKey(i => i.Departamento)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.LugarRetiroCarne>()
              .HasOne(i => i.GrupoLugarRetiroCarne)
              .WithMany(i => i.LugarRetiroCarnes)
              .HasForeignKey(i => i.Grupo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.MensajePushAdd>()
              .HasOne(i => i.MensajePush)
              .WithMany(i => i.MensajePushAdds)
              .HasForeignKey(i => i.Mensaje)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.MensajeSegmento>()
              .HasOne(i => i.MensajePush)
              .WithMany(i => i.MensajeSegmentos)
              .HasForeignKey(i => i.Mensaje)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentum>()
              .HasOne(i => i.AjusteRetroactivo)
              .WithMany(i => i.MovimientoCuenta)
              .HasForeignKey(i => i.Ajuste)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentum>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.MovimientoCuenta)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentum>()
              .HasOne(i => i.MovimientoCuentum1)
              .WithMany(i => i.MovimientoCuenta1)
              .HasForeignKey(i => i.MovimientoReferencia)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentum>()
              .HasOne(i => i.MovimientoTipo1)
              .WithMany(i => i.MovimientoCuenta)
              .HasForeignKey(i => i.MovimientoTipo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentum>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.MovimientoCuenta)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentum>()
              .HasOne(i => i.OrigenMovimiento)
              .WithMany(i => i.MovimientoCuenta)
              .HasForeignKey(i => i.Origen)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>()
              .HasOne(i => i.CategoriaColegiado)
              .WithMany(i => i.MovimientoCuentaCuota)
              .HasForeignKey(i => i.Categoria)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>()
              .HasOne(i => i.MovimientoCuentum)
              .WithMany(i => i.MovimientoCuentaCuota)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentaManual>()
              .HasOne(i => i.MovimientoCuentum)
              .WithMany(i => i.MovimientoCuentaManuals)
              .HasForeignKey(i => i.Id)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.OrigenMovimiento>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.OrigenMovimientos)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.AgenteCobranza)
              .WithMany(i => i.Parametros)
              .HasForeignKey(i => i.AgenteIMM)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.AgenteCobranzaDebito)
              .WithMany(i => i.Parametros)
              .HasForeignKey(i => i.AgenteVisaNET)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.CategoriaColegiado)
              .WithMany(i => i.Parametros)
              .HasForeignKey(i => i.CategoriaColegiadoDefecto)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.CategoriaColegiado1)
              .WithMany(i => i.Parametros1)
              .HasForeignKey(i => i.CategoriaColegiadoMora)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.DeclaracionJuradaTipo)
              .WithMany(i => i.Parametros)
              .HasForeignKey(i => i.DeclaracionJuradaTipoPaternidad)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.DeclaracionJuradaTipo1)
              .WithMany(i => i.Parametros1)
              .HasForeignKey(i => i.DJ05)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.MovimientoTipo)
              .WithMany(i => i.Parametros)
              .HasForeignKey(i => i.MovimientoTipoCobroManual)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.MovimientoTipo1)
              .WithMany(i => i.Parametros1)
              .HasForeignKey(i => i.MovimientoTipoCuota)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.MovimientoTipo2)
              .WithMany(i => i.Parametros2)
              .HasForeignKey(i => i.MovimientoTipoDebito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Parametro>()
              .HasOne(i => i.MovimientoTipo3)
              .WithMany(i => i.Parametros3)
              .HasForeignKey(i => i.MovimientoTipoDeposito)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Regional>()
              .HasOne(i => i.Region1)
              .WithMany(i => i.Regionals)
              .HasForeignKey(i => i.Region)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>()
              .HasOne(i => i.CuentaBancarium)
              .WithMany(i => i.RegionalregionalesCuentabancariacuentabancaria)
              .HasForeignKey(i => i.CuentaBancarias)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>()
              .HasOne(i => i.Regional)
              .WithMany(i => i.RegionalregionalesCuentabancariacuentabancaria)
              .HasForeignKey(i => i.Regionales)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .HasOne(i => i.Departamento1)
              .WithMany(i => i.RegistroColegiados)
              .HasForeignKey(i => i.Departamento)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .HasOne(i => i.Pai)
              .WithMany(i => i.RegistroColegiados)
              .HasForeignKey(i => i.PaisTitulo)
              .HasPrincipalKey(i => i.Codigo);

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .HasOne(i => i.Universidad)
              .WithMany(i => i.RegistroColegiados)
              .HasForeignKey(i => i.UniversidadTitulo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .HasOne(i => i.UniversidadTituloGrado1)
              .WithMany(i => i.RegistroColegiados)
              .HasForeignKey(i => i.UniversidadTituloGrado)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>()
              .HasOne(i => i.RegistroColegiado1)
              .WithMany(i => i.RegistroColegiadoNotificacions)
              .HasForeignKey(i => i.RegistroColegiado)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.Rol>()
              .HasOne(i => i.SecuritySystemRole)
              .WithMany(i => i.Rols)
              .HasForeignKey(i => i.Oid)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>()
              .HasOne(i => i.MovimientoTipo)
              .WithMany(i => i.RolrolesMovimientotipomovimientostipos)
              .HasForeignKey(i => i.MovimientosTipo)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>()
              .HasOne(i => i.Rol)
              .WithMany(i => i.RolrolesMovimientotipomovimientostipos)
              .HasForeignKey(i => i.Roles)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SalaReserva>()
              .HasOne(i => i.MyFileDatum)
              .WithMany(i => i.SalaReservas)
              .HasForeignKey(i => i.Folleto)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SalaReserva>()
              .HasOne(i => i.SalaOrganizador)
              .WithMany(i => i.SalaReservas)
              .HasForeignKey(i => i.Organizador)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.SalaReserva>()
              .HasOne(i => i.SalaCmu)
              .WithMany(i => i.SalaReservas)
              .HasForeignKey(i => i.Sala)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.SalaReservaRegistro>()
              .HasOne(i => i.SalaReserva)
              .WithMany(i => i.SalaReservaRegistros)
              .HasForeignKey(i => i.Reserva)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>()
              .HasOne(i => i.SecuritySystemTypePermissionsObject)
              .WithMany(i => i.SecuritySystemMemberPermissionsObjects)
              .HasForeignKey(i => i.Owner)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>()
              .HasOne(i => i.SecuritySystemTypePermissionsObject)
              .WithMany(i => i.SecuritySystemObjectPermissionsObjects)
              .HasForeignKey(i => i.Owner)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SecuritySystemRole>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.SecuritySystemRoles)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>()
              .HasOne(i => i.SecuritySystemRole)
              .WithMany(i => i.SecuritysystemroleparentrolesSecuritysystemrolechildroles)
              .HasForeignKey(i => i.ChildRoles)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>()
              .HasOne(i => i.SecuritySystemRole1)
              .WithMany(i => i.SecuritysystemroleparentrolesSecuritysystemrolechildroles1)
              .HasForeignKey(i => i.ParentRoles)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.SecuritySystemTypePermissionsObjects)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>()
              .HasOne(i => i.SecuritySystemRole)
              .WithMany(i => i.SecuritySystemTypePermissionsObjects)
              .HasForeignKey(i => i.Owner)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SecuritySystemUser>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.SecuritySystemUsers)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>()
              .HasOne(i => i.SecuritySystemRole)
              .WithMany(i => i.SecuritysystemuserusersSecuritysystemroleroles)
              .HasForeignKey(i => i.Roles)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>()
              .HasOne(i => i.SecuritySystemUser)
              .WithMany(i => i.SecuritysystemuserusersSecuritysystemroleroles)
              .HasForeignKey(i => i.Users)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBaja>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.SolicitudBajas)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBaja>()
              .HasOne(i => i.ColegiadoDeclaracionJuradum)
              .WithMany(i => i.SolicitudBajas)
              .HasForeignKey(i => i.DJInactividad)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>()
              .HasOne(i => i.FileDatum)
              .WithMany(i => i.SolicitudBajaFileAttachments)
              .HasForeignKey(i => i.File)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>()
              .HasOne(i => i.SolicitudBaja)
              .WithMany(i => i.SolicitudBajaFileAttachments)
              .HasForeignKey(i => i.Solicitud)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.TramiteInfoadjuntabases)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>()
              .HasOne(i => i.TramiteCarne)
              .WithMany(i => i.TramiteInfoadjuntabases)
              .HasForeignKey(i => i.Tramite)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>()
              .HasOne(i => i.TramiteInfoAdjuntaBase)
              .WithMany(i => i.TramiteInfoadjuntacedulas)
              .HasForeignKey(i => i.OID)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>()
              .HasOne(i => i.Especialidad1)
              .WithMany(i => i.TramiteInfoadjuntaespecialidads)
              .HasForeignKey(i => i.Especialidad)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>()
              .HasOne(i => i.TramiteInfoAdjuntaTitulo)
              .WithMany(i => i.TramiteInfoadjuntaespecialidads)
              .HasForeignKey(i => i.OID)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>()
              .HasOne(i => i.TramiteInfoAdjuntaBase)
              .WithMany(i => i.TramiteInfoadjuntafotocarnes)
              .HasForeignKey(i => i.OID)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>()
              .HasOne(i => i.TramiteInfoAdjuntaBase)
              .WithMany(i => i.TramiteInfoadjuntatitulos)
              .HasForeignKey(i => i.OID)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>()
              .HasOne(i => i.UniversidadTituloGrado)
              .WithMany(i => i.TramiteInfoadjuntatitulos)
              .HasForeignKey(i => i.Universidad)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarne>()
              .HasOne(i => i.Colegiado1)
              .WithMany(i => i.TramiteCarnes)
              .HasForeignKey(i => i.Colegiado)
              .HasPrincipalKey(i => i.Documento);

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarne>()
              .HasOne(i => i.LugarRetiroCarne)
              .WithMany(i => i.TramiteCarnes)
              .HasForeignKey(i => i.LugarRetiro)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarneEstado>()
              .HasOne(i => i.TramiteCarneEstadoCodigo)
              .WithMany(i => i.TramiteCarneEstados)
              .HasForeignKey(i => i.Estado)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarneEstado>()
              .HasOne(i => i.TramiteCarne)
              .WithMany(i => i.TramiteCarneEstados)
              .HasForeignKey(i => i.Tramite)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>()
              .HasOne(i => i.TramiteCarneEstadoCodigo)
              .WithMany(i => i.TramiteCarneEstadoWorkFlows)
              .HasForeignKey(i => i.EstadoActual)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>()
              .HasOne(i => i.TramiteCarneEstadoCodigo1)
              .WithMany(i => i.TramiteCarneEstadoWorkFlows1)
              .HasForeignKey(i => i.EstadoSiguiente)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.Usuario>()
              .HasOne(i => i.SecuritySystemUser)
              .WithMany(i => i.Usuarios)
              .HasForeignKey(i => i.Oid)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.UsuarioAcceso>()
              .HasOne(i => i.Usuario1)
              .WithMany(i => i.UsuarioAccesos)
              .HasForeignKey(i => i.Usuario)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.UsuarioInstitucion>()
              .HasOne(i => i.AgenteCobranza)
              .WithMany(i => i.UsuarioInstitucions)
              .HasForeignKey(i => i.Institucion)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.UsuarioInstitucion>()
              .HasOne(i => i.Usuario)
              .WithMany(i => i.UsuarioInstitucions)
              .HasForeignKey(i => i.Oid)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.UsuarioRegional>()
              .HasOne(i => i.Usuario)
              .WithMany(i => i.UsuarioRegionals)
              .HasForeignKey(i => i.Oid)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.UsuarioRegional>()
              .HasOne(i => i.Regional1)
              .WithMany(i => i.UsuarioRegionals)
              .HasForeignKey(i => i.Regional)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<SGPA.Server.Models.CMU.XpObjectModified>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.XpObjectModifieds)
              .HasForeignKey(i => i.XPObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.XpoState>()
              .HasOne(i => i.XpoStateMachine)
              .WithMany(i => i.XpoStates)
              .HasForeignKey(i => i.StateMachine)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.XpoStateAppearance>()
              .HasOne(i => i.XpoState)
              .WithMany(i => i.XpoStateAppearances)
              .HasForeignKey(i => i.State)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.XpoStateMachine>()
              .HasOne(i => i.XpoState)
              .WithMany(i => i.XpoStateMachines)
              .HasForeignKey(i => i.StartState)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.XpoTransition>()
              .HasOne(i => i.XpoState)
              .WithMany(i => i.XpoTransitions)
              .HasForeignKey(i => i.SourceState)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.XpoTransition>()
              .HasOne(i => i.XpoState1)
              .WithMany(i => i.XpoTransitions1)
              .HasForeignKey(i => i.TargetState)
              .HasPrincipalKey(i => i.Oid);

            builder.Entity<SGPA.Server.Models.CMU.XpWeakReference>()
              .HasOne(i => i.XpobjectType)
              .WithMany(i => i.XpWeakReferences)
              .HasForeignKey(i => i.ObjectType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.XpWeakReference>()
              .HasOne(i => i.XpobjectType1)
              .WithMany(i => i.XpWeakReferences1)
              .HasForeignKey(i => i.TargetType)
              .HasPrincipalKey(i => i.OID);

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.Elector2015)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.Origen)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<SGPA.Server.Models.CMU.ActaConsejo>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.AjusteDetalle>()
              .Property(p => p.FechaOrigen)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.AjusteRetroactivo>()
              .Property(p => p.FechaYHora)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.AjusteRetroactivo>()
              .Property(p => p.FechaAnulado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.AuditDataItemPersistent>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.CategoriaColegiadoValor>()
              .Property(p => p.FechaDesde)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.CategoriaColegiadoValor>()
              .Property(p => p.FechaHasta)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Cobro>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Cobro>()
              .Property(p => p.FechaIniCargo)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Cobro>()
              .Property(p => p.FechaFinCargo)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.CobroNomina>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaNacimiento)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaPresentacionDJ)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaBaja)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaPresentacionTitulo)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaVencimientoDJ)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaHabilitacionMSP)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaExpedicionTitulo)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.UltimoCambioContraseña)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaFinExoneracion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaVerificacionalta)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaVerificacionBaja)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.UltimaActualizacionDatos)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaAlta)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaVinculacion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaCargaEMail)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.Fecha1erPago)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaIngresoBaja)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaSolicitudBaja)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaSolicitudAlta)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiado>()
              .Property(p => p.FechaExoneracionAnterior)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>()
              .Property(p => p.FechaYHora)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoBitacora>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>()
              .Property(p => p.FechaHoraRecepcion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>()
              .Property(p => p.FechaDesde)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>()
              .Property(p => p.FechaHasta)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>()
              .Property(p => p.FechaYHora)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.FechaVencimiento)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.FechaVerificacion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.FechaEnvioNotificacionVencimiento)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.FechaProcesamientoVencimiento)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.FechaPresentacion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.FechaAnulada)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.FechaIngreso)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>()
              .Property(p => p.FechaNacimiento)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoImagene>()
              .Property(p => p.FechaYHora)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Colegiados2011>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>()
              .Property(p => p.FechaVerificacion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>()
              .Property(p => p.FechaSolicitud)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>()
              .Property(p => p.FechaIngreso)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Convenio>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Convenio>()
              .Property(p => p.FechaAnulado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Convenio>()
              .Property(p => p.FechaCancelacion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.Debito>()
              .Property(p => p.FechaCargaResultado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.DebitoNomina>()
              .Property(p => p.FechaTransaccion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.EmailEnvio>()
              .Property(p => p.FechaHoraEnvio)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.KpiDefinition>()
              .Property(p => p.Changed)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.KpiDefinition>()
              .Property(p => p.ChangedOn)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.KpiHistoryItem>()
              .Property(p => p.RangeStart)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.KpiHistoryItem>()
              .Property(p => p.RangeEnd)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.KpiInstance>()
              .Property(p => p.ForceMeasurementDateTime)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.MensajePush>()
              .Property(p => p.FechaHoraAEnviar)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.MensajePush>()
              .Property(p => p.FechaHoraEnviado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.MensajePush>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentum>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>()
              .Property(p => p.FechaPago)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaNacimiento)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaHabilitacionMSP)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaExpedicionTitulo)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaConfirmado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaRechazado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaIngresado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaAnulado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaProcesado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaObservado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiado>()
              .Property(p => p.FechaCorregido)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>()
              .Property(p => p.FechaHora)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SalaReserva>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SalaReserva>()
              .Property(p => p.HoraDesde)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SalaReserva>()
              .Property(p => p.HoraHasta)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SalaReserva>()
              .Property(p => p.FechaEnvioEMailConfirmacion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SalaReservaRegistro>()
              .Property(p => p.FechaHoraIngreso)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SalaReservaRegistro>()
              .Property(p => p.FechaEnvioCorreo)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBaja>()
              .Property(p => p.FechaDesde)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBaja>()
              .Property(p => p.FechaHasta)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBaja>()
              .Property(p => p.FechaProcesado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBaja>()
              .Property(p => p.FechaDesdeTotal)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.SolicitudBaja>()
              .Property(p => p.FechaIngresado)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.TmpFecha>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>()
              .Property(p => p.FechaTitulo)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarne>()
              .Property(p => p.FechaInicio)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarne>()
              .Property(p => p.FechaValidacionEntrega)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarneEstado>()
              .Property(p => p.Fecha)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.TramiteCarneEstado>()
              .Property(p => p.FechaEnvioNotificacion)
              .HasColumnType("datetime");

            builder.Entity<SGPA.Server.Models.CMU.UsuarioAcceso>()
              .Property(p => p.FechaYHora)
              .HasColumnType("datetime");
            this.OnModelBuilding(builder);
        }

        public DbSet<SGPA.Server.Models.CMU.ActaConsejo> ActaConsejos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AgenteCobranza> AgenteCobranzas { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AgenteCobranzaDebito> AgenteCobranzaDebitos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AgenteCobranzaTipo> AgenteCobranzaTipos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AgenteGrupo> AgenteGrupos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AjusteDetalle> AjusteDetalles { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AjusteRetroactivo> AjusteRetroactivos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Analysis> Analyses { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AreaContacto> AreaContactos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AuditDataItemPersistent> AuditDataItemPersistents { get; set; }

        public DbSet<SGPA.Server.Models.CMU.AuditedObjectWeakReference> AuditedObjectWeakReferences { get; set; }

        public DbSet<SGPA.Server.Models.CMU.BajaMotivo> BajaMotivos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.BajaTemporalMotivo> BajaTemporalMotivos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Banco> Bancos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.CargoContacto> CargoContactos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.CategoriaColegiado> CategoriaColegiados { get; set; }

        public DbSet<SGPA.Server.Models.CMU.CategoriaColegiadoValor> CategoriaColegiadoValors { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Cjp> Cjps { get; set; }

        public DbSet<SGPA.Server.Models.CMU.CjpMat> CjpMats { get; set; }

        public DbSet<SGPA.Server.Models.CMU.CjpOld> CjpOlds { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Cobro> Cobros { get; set; }

        public DbSet<SGPA.Server.Models.CMU.CobroNomina> CobroNominas { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Colegiado> Colegiados { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> ColegiadoActualizacionDps { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoBitacora> ColegiadoBitacoras { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> ColegiadoBitacoraEMailEnvios { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> ColegiadoBitacoraEMailRecepcions { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> ColegiadoBitacoraNota { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> ColegiadoCambioCategoria { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> ColegiadoCertificadoExpedidos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> ColegiadoDebitoBancarioAsociados { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> ColegiadoDeclaracionJurada { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoImagene> ColegiadoImagenes { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoMovimiento> ColegiadoMovimientos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Colegiados2011> Colegiados2011S { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> ColegiadoTarjetaDebitoAsociada { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Contacto> Contactos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ContactoInfoAdicional> ContactoInfoAdicionals { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Convenio> Convenios { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ConvenioFinanciacion> ConvenioFinanciacions { get; set; }

        public DbSet<SGPA.Server.Models.CMU.CuentaBancarium> CuentaBancaria { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Debito> Debitos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DebitoAdjunto> DebitoAdjuntos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DebitoNomina> DebitoNominas { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> DeclaracionJuradaAdjuntos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> DeclaracionJuradaTipos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Departamento> Departamentos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Deposito> Depositos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DepositoNomina> DepositoNominas { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> DepositoNominaMultiBrous { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> DepositoNominaNoIdentificada { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DepositoNominaRedPago> DepositoNominaRedPagos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DjInactividadMotivo> DjInactividadMotivos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.DynamicListViewFilter> DynamicListViewFilters { get; set; }

        public DbSet<SGPA.Server.Models.CMU.EmailEnvio> EmailEnvios { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Especialidad> Especialidads { get; set; }

        public DbSet<SGPA.Server.Models.CMU.FacultadTitulo> FacultadTitulos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.FileDatum> FileData { get; set; }

        public DbSet<SGPA.Server.Models.CMU.GrupoContacto> GrupoContactos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> GrupoLugarRetiroCarnes { get; set; }

        public DbSet<SGPA.Server.Models.CMU.KpiDefinition> KpiDefinitions { get; set; }

        public DbSet<SGPA.Server.Models.CMU.KpiHistoryItem> KpiHistoryItems { get; set; }

        public DbSet<SGPA.Server.Models.CMU.KpiInstance> KpiInstances { get; set; }

        public DbSet<SGPA.Server.Models.CMU.KpiScorecard> KpiScorecards { get; set; }

        public DbSet<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> KpiscorecardscorecardsKpiinstanceindicators { get; set; }

        public DbSet<SGPA.Server.Models.CMU.LugarRetiroCarne> LugarRetiroCarnes { get; set; }

        public DbSet<SGPA.Server.Models.CMU.MensajePush> MensajePushes { get; set; }

        public DbSet<SGPA.Server.Models.CMU.MensajePushAdd> MensajePushAdds { get; set; }

        public DbSet<SGPA.Server.Models.CMU.MensajeSegmento> MensajeSegmentos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ModuleInfo> ModuleInfos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.MovimientoCuentum> MovimientoCuenta { get; set; }

        public DbSet<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> MovimientoCuentaCuota { get; set; }

        public DbSet<SGPA.Server.Models.CMU.MovimientoCuentaManual> MovimientoCuentaManuals { get; set; }

        public DbSet<SGPA.Server.Models.CMU.MovimientoTipo> MovimientoTipos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.MyFileDatum> MyFileData { get; set; }

        public DbSet<SGPA.Server.Models.CMU.OrigenMovimiento> OrigenMovimientos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Pai> Pais { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Parametro> Parametros { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Region> Regions { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Regional> Regionals { get; set; }

        public DbSet<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> RegionalregionalesCuentabancariacuentabancaria { get; set; }

        public DbSet<SGPA.Server.Models.CMU.RegistroColegiado> RegistroColegiados { get; set; }

        public DbSet<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> RegistroColegiadoNotificacions { get; set; }

        public DbSet<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> RegistroColegiadoRechazoParams { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ReportDatum> ReportData { get; set; }

        public DbSet<SGPA.Server.Models.CMU.ReportDataV2> ReportDataV2S { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Rol> Rols { get; set; }

        public DbSet<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> RolrolesMovimientotipomovimientostipos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SalaCmu> SalaCmus { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SalaOrganizador> SalaOrganizadors { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SalaReserva> SalaReservas { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SalaReservaRegistro> SalaReservaRegistros { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> SecuritySystemMemberPermissionsObjects { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> SecuritySystemObjectPermissionsObjects { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SecuritySystemRole> SecuritySystemRoles { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> SecuritysystemroleparentrolesSecuritysystemrolechildroles { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> SecuritySystemTypePermissionsObjects { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SecuritySystemUser> SecuritySystemUsers { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> SecuritysystemuserusersSecuritysystemroleroles { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SolicitudBaja> SolicitudBajas { get; set; }

        public DbSet<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> SolicitudBajaFileAttachments { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TmpCarneEntregado> TmpCarneEntregados { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TmpCarneRetirar> TmpCarneRetirars { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TmpFecha> TmpFechas { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TmpMese> TmpMeses { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> TramiteInfoadjuntabases { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> TramiteInfoadjuntacedulas { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> TramiteInfoadjuntaespecialidads { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> TramiteInfoadjuntafotocarnes { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> TramiteInfoadjuntatitulos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteCarne> TramiteCarnes { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteCarneEstado> TramiteCarneEstados { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> TramiteCarneEstadoCodigos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> TramiteCarneEstadoWorkFlows { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Universidad> Universidads { get; set; }

        public DbSet<SGPA.Server.Models.CMU.UniversidadTituloGrado> UniversidadTituloGrados { get; set; }

        public DbSet<SGPA.Server.Models.CMU.UserResetPasswordRequest> UserResetPasswordRequests { get; set; }

        public DbSet<SGPA.Server.Models.CMU.Usuario> Usuarios { get; set; }

        public DbSet<SGPA.Server.Models.CMU.UsuarioAcceso> UsuarioAccesos { get; set; }

        public DbSet<SGPA.Server.Models.CMU.UsuarioInstitucion> UsuarioInstitucions { get; set; }

        public DbSet<SGPA.Server.Models.CMU.UsuarioRegional> UsuarioRegionals { get; set; }

        public DbSet<SGPA.Server.Models.CMU.XpObjectModified> XpObjectModifieds { get; set; }

        public DbSet<SGPA.Server.Models.CMU.XpObjectType> XpObjectTypes { get; set; }

        public DbSet<SGPA.Server.Models.CMU.XpoState> XpoStates { get; set; }

        public DbSet<SGPA.Server.Models.CMU.XpoStateAppearance> XpoStateAppearances { get; set; }

        public DbSet<SGPA.Server.Models.CMU.XpoStateMachine> XpoStateMachines { get; set; }

        public DbSet<SGPA.Server.Models.CMU.XpoTransition> XpoTransitions { get; set; }

        public DbSet<SGPA.Server.Models.CMU.XpWeakReference> XpWeakReferences { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}