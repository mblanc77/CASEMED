using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("ColegiadoDebitoBancarioAsociado", Schema = "dbo")]
    public partial class ColegiadoDebitoBancarioAsociado
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
        public int Id { get; set; }

        [ConcurrencyCheck]
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public long? NumeroCuenta { get; set; }

        [ConcurrencyCheck]
        public int? TipoMovimiento { get; set; }

        [ConcurrencyCheck]
        public int? AgenteDebito { get; set; }

        [ConcurrencyCheck]
        public int? Identificacion { get; set; }

        [ConcurrencyCheck]
        public string Observaciones { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public int? TipoLiquidacion { get; set; }

        [ConcurrencyCheck]
        public int? Ambito { get; set; }

        public AgenteCobranzaDebito AgenteCobranzaDebito { get; set; }

        public Colegiado Colegiado1 { get; set; }

    }
}