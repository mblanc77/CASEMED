using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("RecetaDistancia", Schema = "dbo")]
    public partial class RecetaDistancium
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
        public string CodRecetaDistancia { get; set; }

        [ConcurrencyCheck]
        public string Descrip { get; set; }

        public ICollection<Recetum> Receta { get; set; }

    }
}