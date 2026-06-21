using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("NoCargadoHL", Schema = "dbo")]
    public partial class NoCargadoHl
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
        public int CI { get; set; }

        [ConcurrencyCheck]
        public string Apellido1 { get; set; }

        [ConcurrencyCheck]
        public string Apellido2 { get; set; }

        [ConcurrencyCheck]
        public string Nombres { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int CodEmpresa { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int Mes { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int Anio { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoCargadoHLId { get; set; }

    }
}