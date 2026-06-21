using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Parametros", Schema = "dbo")]
    public partial class Parametro
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        public double? SMN { get; set; }

        public float? TopeJubilatorio { get; set; }

        public float? TopePrima { get; set; }

        public float? UR { get; set; }

        public float? PctAdPreJub { get; set; }

        public double? BCP { get; set; }

        public double? TopeLiquidoBPS { get; set; }

        public double? pctBPS { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

    }
}