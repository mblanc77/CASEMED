using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("ColegiadoBitacoraEMailEnvio", Schema = "dbo")]
    public partial class ColegiadoBitacoraEMailEnvio
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
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string Destinatario { get; set; }

        [ConcurrencyCheck]
        public string FechaHoraEnvio { get; set; }

        [ConcurrencyCheck]
        public string Titulo { get; set; }

        [ConcurrencyCheck]
        public bool? Automatico { get; set; }

        public ColegiadoBitacoraNotum ColegiadoBitacoraNotum { get; set; }

    }
}