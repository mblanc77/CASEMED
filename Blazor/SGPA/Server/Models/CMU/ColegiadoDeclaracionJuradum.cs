using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("ColegiadoDeclaracionJurada", Schema = "dbo")]
    public partial class ColegiadoDeclaracionJuradum
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
        public int Id { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public decimal? MontoNominalDJ { get; set; }

        [ConcurrencyCheck]
        public string Comentario { get; set; }

        [ConcurrencyCheck]
        public int? NroFormulario { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVencimiento { get; set; }

        [ConcurrencyCheck]
        public int? Tipo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVerificacion { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaEnvioNotificacionVencimiento { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaProcesamientoVencimiento { get; set; }

        [ConcurrencyCheck]
        public bool? ExcluirFiltroVencimiento { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaPresentacion { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public bool? Anulada { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaAnulada { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIngreso { get; set; }

        [ConcurrencyCheck]
        public int? Origen { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        [ConcurrencyCheck]
        public int? Cedula { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaNacimiento { get; set; }

        [ConcurrencyCheck]
        public int? MotivoInactividad { get; set; }

        [ConcurrencyCheck]
        public string MotivoOtrosInactividad { get; set; }

        public ICollection<AjusteRetroactivo> AjusteRetroactivos { get; set; }

        public Colegiado Colegiado1 { get; set; }

        public DjInactividadMotivo DjinactividadMotivo { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public DeclaracionJuradaTipo DeclaracionJuradaTipo { get; set; }

        public ICollection<DeclaracionJuradaAdjunto> DeclaracionJuradaAdjuntos { get; set; }

        public ICollection<SolicitudBaja> SolicitudBajas { get; set; }

    }
}