using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("SubsidioCabezal_BPS", Schema = "dbo")]
    public partial class SubsidiocabezalBp
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
        public int IdSubsidio { get; set; }

        [ConcurrencyCheck]
        public int? DiasBPS { get; set; }

        [ConcurrencyCheck]
        public double? LiquidoBPS { get; set; }

        [ConcurrencyCheck]
        public double? AguinaldoBPS { get; set; }

        [ConcurrencyCheck]
        public double? LiquidoPagar { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public SubsidioCabezal SubsidioCabezal { get; set; }

    }
}