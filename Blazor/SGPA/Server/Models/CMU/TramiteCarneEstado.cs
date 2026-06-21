using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("TramiteCarneEstado", Schema = "dbo")]
    public partial class TramiteCarneEstado
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
        public int? Tramite { get; set; }

        [ConcurrencyCheck]
        public int? Estado { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public string Comentario { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaEnvioNotificacion { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public TramiteCarneEstadoCodigo TramiteCarneEstadoCodigo { get; set; }

        public TramiteCarne TramiteCarne { get; set; }

    }
}