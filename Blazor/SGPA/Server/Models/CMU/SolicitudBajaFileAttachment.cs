using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SolicitudBajaFileAttachment", Schema = "dbo")]
    public partial class SolicitudBajaFileAttachment
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [ConcurrencyCheck]
        public Guid? File { get; set; }

        [Key]
        [Required]
        public Guid Guid { get; set; }

        [ConcurrencyCheck]
        public string Comentario { get; set; }

        [ConcurrencyCheck]
        public int? Solicitud { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public FileDatum FileDatum { get; set; }

        public SolicitudBaja SolicitudBaja { get; set; }

    }
}