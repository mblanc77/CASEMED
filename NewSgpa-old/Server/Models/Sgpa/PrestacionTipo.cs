using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("PrestacionTipo", Schema = "dbo")]
    public partial class PrestacionTipo
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
        public short CodPrestacionTipo { get; set; }

        [ConcurrencyCheck]
        public string Descrip { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVigencia { get; set; }

        [ConcurrencyCheck]
        public double? ImporteTopeDISSE { get; set; }

        [ConcurrencyCheck]
        public double? ImporteTopeCASEMED { get; set; }

        [ConcurrencyCheck]
        public short? PeriodoRenovacion { get; set; }

        [ConcurrencyCheck]
        public bool? Receta { get; set; }

        [ConcurrencyCheck]
        public string Obs { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public ICollection<Prestacion> Prestacions { get; set; }

    }
}