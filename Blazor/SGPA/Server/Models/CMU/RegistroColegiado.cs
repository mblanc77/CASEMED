using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("RegistroColegiado", Schema = "dbo")]
    public partial class RegistroColegiado
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OID { get; set; }

        [ConcurrencyCheck]
        public int? Documento { get; set; }

        [ConcurrencyCheck]
        public string EMail { get; set; }

        [ConcurrencyCheck]
        public string Nombres { get; set; }

        [ConcurrencyCheck]
        public string PrimerApellido { get; set; }

        [ConcurrencyCheck]
        public string SegundoApellido { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaNacimiento { get; set; }

        [ConcurrencyCheck]
        public int? Sexo { get; set; }

        [ConcurrencyCheck]
        public int? NroMatricula { get; set; }

        [ConcurrencyCheck]
        public int? Departamento { get; set; }

        [ConcurrencyCheck]
        public string Localidad { get; set; }

        [ConcurrencyCheck]
        public string Direccion { get; set; }

        [ConcurrencyCheck]
        public string NumeroPuerta { get; set; }

        [ConcurrencyCheck]
        public string Apartamento { get; set; }

        [ConcurrencyCheck]
        public decimal? CodigoPostal { get; set; }

        [ConcurrencyCheck]
        public string Celular { get; set; }

        [ConcurrencyCheck]
        public string TelefonoParticular { get; set; }

        [ConcurrencyCheck]
        public string TelefonoLaboral { get; set; }

        [ConcurrencyCheck]
        public string LugarNacimiento { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaHabilitacionMSP { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaExpedicionTitulo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaConfirmado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaRechazado { get; set; }

        [ConcurrencyCheck]
        public string ComentarioRechazo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIngresado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaAnulado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaProcesado { get; set; }

        [ConcurrencyCheck]
        public string UrlID_Frente { get; set; }

        [ConcurrencyCheck]
        public string UrlID_Reverso { get; set; }

        [ConcurrencyCheck]
        public string UrlTitulo_Frente { get; set; }

        [ConcurrencyCheck]
        public string UrlTitulo_Reverso { get; set; }

        [ConcurrencyCheck]
        public string UrlConstancia_MSP { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public Guid? Guid { get; set; }

        [Column("false")]
        [ConcurrencyCheck]
        public string false1 { get; set; }

        [ConcurrencyCheck]
        public bool? AceptaCondiciones { get; set; }

        [ConcurrencyCheck]
        public int? UniversidadTitulo { get; set; }

        [ConcurrencyCheck]
        public string NombreUniversidadTitulo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaObservado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaCorregido { get; set; }

        [ConcurrencyCheck]
        public int? PaisTitulo { get; set; }

        [ConcurrencyCheck]
        public int? UniversidadTituloGrado { get; set; }

        [ConcurrencyCheck]
        public int? Estado { get; set; }

        public Departamento Departamento1 { get; set; }

        public Pai Pai { get; set; }

        public Universidad Universidad { get; set; }

        public UniversidadTituloGrado UniversidadTituloGrado1 { get; set; }

        public ICollection<RegistroColegiadoNotificacion> RegistroColegiadoNotificacions { get; set; }

    }
}