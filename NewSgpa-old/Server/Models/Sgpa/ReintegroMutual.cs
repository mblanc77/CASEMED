using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("ReintegroMutual", Schema = "dbo")]
    public partial class ReintegroMutual
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
        public int ReintegroMutualId { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int CI { get; set; }

        [Required]
        [ConcurrencyCheck]
        public short Mes { get; set; }

        [Required]
        [ConcurrencyCheck]
        public short Anio { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public short? CodMutualista { get; set; }

        [ConcurrencyCheck]
        public float? Importe { get; set; }

        [ConcurrencyCheck]
        public string Observaciones { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public Afiliado Afiliado { get; set; }

        public Mutualistum Mutualistum { get; set; }

    }
}