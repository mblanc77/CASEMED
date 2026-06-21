using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("AfeccionGrupo", Schema = "dbo")]
    public partial class AfeccionGrupo
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
        public int CodAfeccionGrupo { get; set; }

        [ConcurrencyCheck]
        public string Descrip { get; set; }

        [ConcurrencyCheck]
        public int? CodPatologia { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        public Patologium Patologium { get; set; }

        public ICollection<AfeccionTipo> AfeccionTipos { get; set; }

    }
}