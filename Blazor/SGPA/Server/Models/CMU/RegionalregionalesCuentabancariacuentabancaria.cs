using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("RegionalRegionales_CuentaBancariaCuentaBancarias", Schema = "dbo")]
    public partial class RegionalregionalesCuentabancariacuentabancaria
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [ConcurrencyCheck]
        public int? CuentaBancarias { get; set; }

        [ConcurrencyCheck]
        public int? Regionales { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OID { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public CuentaBancarium CuentaBancarium { get; set; }

        public Regional Regional { get; set; }

    }
}