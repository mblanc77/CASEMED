using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Receta", Schema = "dbo")]
    public partial class Recetum
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecetaId { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int CI { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime Fecha { get; set; }

        [Required]
        [ConcurrencyCheck]
        public short CodPrestacionTipo { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string CodRecetaDistancia { get; set; }

        [ConcurrencyCheck]
        public float? Esf_I { get; set; }

        [ConcurrencyCheck]
        public float? Esf_D { get; set; }

        [ConcurrencyCheck]
        public float? Cil_I { get; set; }

        [ConcurrencyCheck]
        public float? Cil_D { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        [ConcurrencyCheck]
        public int? PrestacionId { get; set; }

        public RecetaDistancium RecetaDistancium { get; set; }

        public Prestacion Prestacion { get; set; }

    }
}