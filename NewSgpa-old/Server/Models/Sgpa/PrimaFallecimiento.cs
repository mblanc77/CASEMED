using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("PrimaFallecimiento", Schema = "dbo")]
    public partial class PrimaFallecimiento
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
        public DateTime? FechaFirma { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaFallecimiento { get; set; }

        [ConcurrencyCheck]
        public double? Importe { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaPago { get; set; }

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

    }
}