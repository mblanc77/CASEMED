using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("SubsidioItemCod", Schema = "dbo")]
    public partial class SubsidioItemCod
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
        public short CodSubsidioItemCod { get; set; }

        [ConcurrencyCheck]
        public string Descrip { get; set; }

        [ConcurrencyCheck]
        public string Tipo { get; set; }

        [ConcurrencyCheck]
        public string ValorTipo { get; set; }

        [ConcurrencyCheck]
        public byte? Signo { get; set; }

        [ConcurrencyCheck]
        public bool? Comparar { get; set; }

        [ConcurrencyCheck]
        public byte? CompararContra { get; set; }

        [ConcurrencyCheck]
        public double? Valor { get; set; }

        [ConcurrencyCheck]
        public string TipoComp { get; set; }

        [ConcurrencyCheck]
        public string Operador { get; set; }

        [ConcurrencyCheck]
        public double? ValorMin { get; set; }

        [ConcurrencyCheck]
        public double? ValorMax { get; set; }

        [ConcurrencyCheck]
        public bool? Procesar { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVigencia { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaBaja { get; set; }

        [ConcurrencyCheck]
        public bool? AperturaXEmpresa { get; set; }

        [ConcurrencyCheck]
        public bool? ModificaNominal { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public ICollection<SubsidioItem> SubsidioItems { get; set; }

    }
}