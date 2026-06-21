using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Colegiado", Schema = "dbo")]
    public partial class Colegiado
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [Key]
        [Required]
        public int Documento { get; set; }

        [ConcurrencyCheck]
        public string Nombres { get; set; }

        [ConcurrencyCheck]
        public string Apellido1 { get; set; }

        [ConcurrencyCheck]
        public string Apellido2 { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaNacimiento { get; set; }

        [ConcurrencyCheck]
        public int? Matricula { get; set; }

        [ConcurrencyCheck]
        public int? Sexo { get; set; }

        [ConcurrencyCheck]
        public int? Departamento { get; set; }

        [ConcurrencyCheck]
        public string EMail { get; set; }

        [ConcurrencyCheck]
        public string Celular { get; set; }

        [ConcurrencyCheck]
        public string TelefonoParticular { get; set; }

        [ConcurrencyCheck]
        public string TelefonoLaboral { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaPresentacionDJ { get; set; }

        [ConcurrencyCheck]
        public int? CategoriaColegiado { get; set; }

        [ConcurrencyCheck]
        public int? AgenteCobro { get; set; }

        [ConcurrencyCheck]
        public string NumeroPuerta { get; set; }

        [ConcurrencyCheck]
        public string Apartamento { get; set; }

        [ConcurrencyCheck]
        public decimal? MontoNominalDJ { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaBaja { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaPresentacionTitulo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVencimientoDJ { get; set; }

        [ConcurrencyCheck]
        public int? BajaMotivo { get; set; }

        [ConcurrencyCheck]
        public string ComentarioBaja { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaHabilitacionMSP { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaExpedicionTitulo { get; set; }

        [ConcurrencyCheck]
        public string NumeroTarjeta { get; set; }

        [ConcurrencyCheck]
        public string VencimientoTarjeta { get; set; }

        [ConcurrencyCheck]
        public string Localidad { get; set; }

        [ConcurrencyCheck]
        public string Direccion { get; set; }

        [ConcurrencyCheck]
        public DateTime? UltimoCambioContraseña { get; set; }

        [ConcurrencyCheck]
        public int? CodigoPostal { get; set; }

        [ConcurrencyCheck]
        public string LugarNacimiento { get; set; }

        [ConcurrencyCheck]
        public bool? NoVerificarDocumento { get; set; }

        [ConcurrencyCheck]
        public bool? Importado { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaFinExoneracion { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVerificacionalta { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVerificacionBaja { get; set; }

        [ConcurrencyCheck]
        public int? NroFormularioAlta { get; set; }

        [ConcurrencyCheck]
        public DateTime? UltimaActualizacionDatos { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaAlta { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVinculacion { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaCargaEMail { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha1erPago { get; set; }

        [ConcurrencyCheck]
        public int? NroFormularioBaja { get; set; }

        [ConcurrencyCheck]
        public int? UltimoAgenteCobro { get; set; }

        [ConcurrencyCheck]
        public int? RegionalTrabaja { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public bool? Elector2015 { get; set; }

        [ConcurrencyCheck]
        public string EmailAlt { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIngresoBaja { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaSolicitudBaja { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaSolicitudAlta { get; set; }

        [ConcurrencyCheck]
        public int? PaisTitulo { get; set; }

        [ConcurrencyCheck]
        public int? UniversidadTituloGrado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaExoneracionAnterior { get; set; }

        [ConcurrencyCheck]
        public string LegacyColegiadoCarneId { get; set; }

        public ICollection<AjusteRetroactivo> AjusteRetroactivos { get; set; }

        public ICollection<CobroNomina> CobroNominas { get; set; }

        public AgenteCobranza AgenteCobranza { get; set; }

        public BajaMotivo BajaMotivo1 { get; set; }

        public CategoriaColegiado CategoriaColegiado1 { get; set; }

        public Departamento Departamento1 { get; set; }

        public Pai Pai { get; set; }

        public Regional Regional { get; set; }

        public AgenteCobranza AgenteCobranza1 { get; set; }

        public UniversidadTituloGrado UniversidadTituloGrado1 { get; set; }

        public ICollection<ColegiadoActualizacionDp> ColegiadoActualizacionDps { get; set; }

        public ICollection<ColegiadoBitacora> ColegiadoBitacoras { get; set; }

        public ICollection<ColegiadoCambioCategorium> ColegiadoCambioCategoria { get; set; }

        public ICollection<ColegiadoCertificadoExpedido> ColegiadoCertificadoExpedidos { get; set; }

        public ICollection<ColegiadoDebitoBancarioAsociado> ColegiadoDebitoBancarioAsociados { get; set; }

        public ICollection<ColegiadoDeclaracionJuradum> ColegiadoDeclaracionJurada { get; set; }

        public ICollection<ColegiadoImagene> ColegiadoImagenes { get; set; }

        public ICollection<ColegiadoTarjetaDebitoAsociadum> ColegiadoTarjetaDebitoAsociada { get; set; }

        public ICollection<Convenio> Convenios { get; set; }

        public ICollection<MovimientoCuentum> MovimientoCuenta { get; set; }

        public ICollection<SolicitudBaja> SolicitudBajas { get; set; }

        public ICollection<TramiteCarne> TramiteCarnes { get; set; }

    }
}