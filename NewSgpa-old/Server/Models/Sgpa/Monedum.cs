using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Moneda", Schema = "dbo")]
    public partial class Monedum
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
        [Column("Moneda")]
        [Required]
        public string Moneda1 { get; set; }

        public ICollection<Prestacion> Prestacions { get; set; }

    }
}