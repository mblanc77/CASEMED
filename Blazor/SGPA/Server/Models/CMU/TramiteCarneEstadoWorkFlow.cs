using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("TramiteCarneEstadoWorkFlow", Schema = "dbo")]
    public partial class TramiteCarneEstadoWorkFlow
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
        public int OID { get; set; }

        [ConcurrencyCheck]
        public int? EstadoActual { get; set; }

        [ConcurrencyCheck]
        public int? EstadoSiguiente { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public bool? Reverso { get; set; }

        public TramiteCarneEstadoCodigo TramiteCarneEstadoCodigo { get; set; }

        public TramiteCarneEstadoCodigo TramiteCarneEstadoCodigo1 { get; set; }

    }
}