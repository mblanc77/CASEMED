using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("CobroNomina", Schema = "dbo")]
    public partial class CobroNomina
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
        public int? Cobro { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public int? Movimiento { get; set; }

        [ConcurrencyCheck]
        public decimal? Importe { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        [ConcurrencyCheck]
        public int? IdExterno { get; set; }

        [ConcurrencyCheck]
        public bool? Efectivo { get; set; }

        [ConcurrencyCheck]
        public int? Convenio { get; set; }

        [ConcurrencyCheck]
        public string Obs { get; set; }

        [ConcurrencyCheck]
        public decimal? ImpOrig { get; set; }

        [ConcurrencyCheck]
        public bool? AnulaConvenio { get; set; }

        public Cobro Cobro1 { get; set; }

        public Colegiado Colegiado1 { get; set; }

        public Convenio Convenio1 { get; set; }

        public MovimientoCuentum MovimientoCuentum { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public ICollection<DebitoNomina> DebitoNominas { get; set; }

        public ICollection<DepositoNomina> DepositoNominas { get; set; }

    }
}