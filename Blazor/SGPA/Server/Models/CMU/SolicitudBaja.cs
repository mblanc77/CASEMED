using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SolicitudBaja", Schema = "dbo")]
    public partial class SolicitudBaja
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
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public int? DJInactividad { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaDesde { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaHasta { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaProcesado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaDesdeTotal { get; set; }

        [ConcurrencyCheck]
        public bool? AceptaCondiciones { get; set; }

        [ConcurrencyCheck]
        public bool? AusenciaPaisChk { get; set; }

        [ConcurrencyCheck]
        public bool? LicenciaEnfermedadChk { get; set; }

        [ConcurrencyCheck]
        public bool? JubilacionChk { get; set; }

        [ConcurrencyCheck]
        public bool? BajaTemporariaChk { get; set; }

        [ConcurrencyCheck]
        public bool? BajaDefinitivaChk { get; set; }

        [ConcurrencyCheck]
        public string OtrosMotivo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIngresado { get; set; }

        [ConcurrencyCheck]
        public int? Estado { get; set; }

        [ConcurrencyCheck]
        public string Observaciones { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public Colegiado Colegiado1 { get; set; }

        public ColegiadoDeclaracionJuradum ColegiadoDeclaracionJuradum { get; set; }

        public ICollection<SolicitudBajaFileAttachment> SolicitudBajaFileAttachments { get; set; }

    }
}