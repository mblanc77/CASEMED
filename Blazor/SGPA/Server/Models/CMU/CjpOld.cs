using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("CJP_old", Schema = "dbo")]
    public partial class CjpOld
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
        public int Documento { get; set; }

        [ConcurrencyCheck]
        public int? Matricula { get; set; }

        [ConcurrencyCheck]
        public string Estado { get; set; }

    }
}