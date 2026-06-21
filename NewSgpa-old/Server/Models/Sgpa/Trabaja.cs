using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Trabaja", Schema = "dbo")]
    public partial class Trabaja
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

        [Required]
        [ConcurrencyCheck]
        public short CodEmpresa { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime FechaIngreso { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaBaja { get; set; }

        [ConcurrencyCheck]
        public int? CodBajaMotivo { get; set; }

        [ConcurrencyCheck]
        public string NroFichaEmpresa { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTrabaja { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIngCasemed { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        public ICollection<Imponible> Imponibles { get; set; }

        public Afiliado Afiliado { get; set; }

        public BajaMotivo BajaMotivo { get; set; }

        public Empresa Empresa { get; set; }

    }
}