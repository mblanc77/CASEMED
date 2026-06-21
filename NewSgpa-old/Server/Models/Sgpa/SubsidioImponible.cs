using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("SubsidioImponible", Schema = "dbo")]
    public partial class SubsidioImponible
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        public int? IdSubsidio { get; set; }

        public byte? Mes { get; set; }

        public short? Anio { get; set; }

        public short? CodEmpresa { get; set; }

        public short? Dias { get; set; }

        public double? Importe { get; set; }

        public string Usr { get; set; }

        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

    }
}