using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("AfeccionTipo", Schema = "dbo")]
    public partial class AfeccionTipo
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
        public short CodAfeccionTipo { get; set; }

        [ConcurrencyCheck]
        public string Descrip { get; set; }

        [ConcurrencyCheck]
        public int? CodAfeccionGrupo { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [ConcurrencyCheck]
        public int? CodDiameg { get; set; }

        public AfeccionGrupo AfeccionGrupo { get; set; }

        public ICollection<Certificacion> Certificacions { get; set; }

    }
}