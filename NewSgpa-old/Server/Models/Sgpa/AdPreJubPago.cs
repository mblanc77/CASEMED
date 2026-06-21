using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("AdPreJubPago", Schema = "dbo")]
    public partial class AdPreJubPago
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [Required]
        [ConcurrencyCheck]
        public int CI { get; set; }

        [ConcurrencyCheck]
        public short Mes { get; set; }

        [ConcurrencyCheck]
        public short Anio { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdPreJubPagoId { get; set; }

        public AdPreJub AdPreJub { get; set; }

    }
}