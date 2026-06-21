using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("AdPreJub", Schema = "dbo")]
    public partial class AdPreJub
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
        public int CI { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaPresentacion { get; set; }

        [ConcurrencyCheck]
        public short? ImporteMensual { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaJubilacion { get; set; }

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

        public ICollection<AdPreJubPago> AdPreJubPagos { get; set; }

    }
}