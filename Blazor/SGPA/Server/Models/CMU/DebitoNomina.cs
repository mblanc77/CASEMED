using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("DebitoNomina", Schema = "dbo")]
    public partial class DebitoNomina
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
        public int Id { get; set; }

        [ConcurrencyCheck]
        public int? NumeroTransaccion { get; set; }

        [ConcurrencyCheck]
        public string NumeroTarjeta { get; set; }

        [ConcurrencyCheck]
        public int? FechaVtoTarjeta { get; set; }

        [ConcurrencyCheck]
        public bool? Aprobado { get; set; }

        [ConcurrencyCheck]
        public string MotivoRechazo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaTransaccion { get; set; }

        [ConcurrencyCheck]
        public int? Debito { get; set; }

        [ConcurrencyCheck]
        public bool? Observado { get; set; }

        [ConcurrencyCheck]
        public string MotivoObservado { get; set; }

        [ConcurrencyCheck]
        public decimal? ImporteOriginal { get; set; }

        public Debito Debito1 { get; set; }

        public CobroNomina CobroNomina { get; set; }

    }
}