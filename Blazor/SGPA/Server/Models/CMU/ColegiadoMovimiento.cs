using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("ColegiadoMovimiento", Schema = "dbo")]
    public partial class ColegiadoMovimiento
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
        public int? Concepto { get; set; }

        [ConcurrencyCheck]
        public int? Año { get; set; }

        [ConcurrencyCheck]
        public int? Mes { get; set; }

        [ConcurrencyCheck]
        public int? Cantidad { get; set; }

    }
}