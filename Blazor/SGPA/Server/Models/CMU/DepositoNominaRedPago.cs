using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("DepositoNominaRedPagos", Schema = "dbo")]
    public partial class DepositoNominaRedPago
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
        public long? NroMovimiento { get; set; }

        [ConcurrencyCheck]
        public int? Caja { get; set; }

        [ConcurrencyCheck]
        public int? SubAgencia { get; set; }

        [ConcurrencyCheck]
        public int? Secuencial { get; set; }

        public DepositoNomina DepositoNomina { get; set; }

    }
}