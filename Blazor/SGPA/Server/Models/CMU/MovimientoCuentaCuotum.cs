using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("MovimientoCuentaCuota", Schema = "dbo")]
    public partial class MovimientoCuentaCuotum
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
        public DateTime? FechaPago { get; set; }

        [ConcurrencyCheck]
        public int? Categoria { get; set; }

        public CategoriaColegiado CategoriaColegiado { get; set; }

        public MovimientoCuentum MovimientoCuentum { get; set; }

    }
}