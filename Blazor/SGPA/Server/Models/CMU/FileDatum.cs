using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("FileData", Schema = "dbo")]
    public partial class FileDatum
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
        public Guid Oid { get; set; }

        [ConcurrencyCheck]
        public int? size { get; set; }

        [ConcurrencyCheck]
        public string FileName { get; set; }

        [ConcurrencyCheck]
        public byte[] Content { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public ICollection<ActaConsejo> ActaConsejos { get; set; }

        public ICollection<DebitoAdjunto> DebitoAdjuntos { get; set; }

        public ICollection<DeclaracionJuradaAdjunto> DeclaracionJuradaAdjuntos { get; set; }

        public ICollection<SolicitudBajaFileAttachment> SolicitudBajaFileAttachments { get; set; }

    }
}