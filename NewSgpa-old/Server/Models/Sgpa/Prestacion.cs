using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Prestacion", Schema = "dbo")]
    public partial class Prestacion
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
        public DateTime Fecha { get; set; }

        [Required]
        [ConcurrencyCheck]
        public short CodPrestacionTipo { get; set; }

        [ConcurrencyCheck]
        public string Moneda { get; set; }

        [ConcurrencyCheck]
        public double? Importe { get; set; }

        [ConcurrencyCheck]
        public bool? Boleta { get; set; }

        [ConcurrencyCheck]
        public string Observaciones { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrestacionId { get; set; }

        public Afiliado Afiliado { get; set; }

        public PrestacionTipo PrestacionTipo { get; set; }

        public Monedum Monedum { get; set; }

        public ICollection<Recetum> Receta { get; set; }

    }
}