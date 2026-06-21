using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("xUsrParam", Schema = "dbo")]
    public partial class XUsrParam
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
        public string login { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string clave { get; set; }

        [Required]
        [ConcurrencyCheck]
        public short orden { get; set; }

        [ConcurrencyCheck]
        public string value1 { get; set; }

        [ConcurrencyCheck]
        public string value2 { get; set; }

        [ConcurrencyCheck]
        public string value3 { get; set; }

        [ConcurrencyCheck]
        public string value4 { get; set; }

        [ConcurrencyCheck]
        public string value5 { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

    }
}