using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("TramiteCarne", Schema = "dbo")]
    public partial class TramiteCarne
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
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaInicio { get; set; }

        [ConcurrencyCheck]
        public Guid? FotoCarneIdentifier { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? LugarRetiro { get; set; }

        [ConcurrencyCheck]
        public bool? Importado { get; set; }

        [ConcurrencyCheck]
        public string EstadoImportado { get; set; }

        [ConcurrencyCheck]
        public int? UIDImportado { get; set; }

        [ConcurrencyCheck]
        public string QrImportado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaValidacionEntrega { get; set; }

        public ICollection<TramiteInfoadjuntabase> TramiteInfoadjuntabases { get; set; }

        public Colegiado Colegiado1 { get; set; }

        public LugarRetiroCarne LugarRetiroCarne { get; set; }

        public ICollection<TramiteCarneEstado> TramiteCarneEstados { get; set; }

    }
}