using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("MensajePush", Schema = "dbo")]
    public partial class MensajePush
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
        public string Titulo { get; set; }

        [ConcurrencyCheck]
        public string Mensaje { get; set; }

        [ConcurrencyCheck]
        public string Url { get; set; }

        [ConcurrencyCheck]
        public string small_icon { get; set; }

        [ConcurrencyCheck]
        public bool? Prioritario { get; set; }

        [ConcurrencyCheck]
        public bool? EnvioInmediato { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaHoraAEnviar { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaHoraEnviado { get; set; }

        [ConcurrencyCheck]
        public string Emails { get; set; }

        [ConcurrencyCheck]
        public bool? iOS { get; set; }

        [ConcurrencyCheck]
        public bool? Android { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public bool? SoloPush { get; set; }

        public ICollection<MensajePushAdd> MensajePushAdds { get; set; }

        public ICollection<MensajeSegmento> MensajeSegmentos { get; set; }

    }
}