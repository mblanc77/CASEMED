using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("MovimientoCuentaManual", Schema = "dbo")]
    public partial class MovimientoCuentaManual
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
        public string Comentario { get; set; }

        [ConcurrencyCheck]
        public string AutorizadoPor { get; set; }

        public MovimientoCuentum MovimientoCuentum { get; set; }

    }
}