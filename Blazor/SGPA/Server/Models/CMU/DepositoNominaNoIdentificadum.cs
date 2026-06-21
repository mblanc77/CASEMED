using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("DepositoNominaNoIdentificada", Schema = "dbo")]
    public partial class DepositoNominaNoIdentificadum
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
        public int? Documento { get; set; }

        [ConcurrencyCheck]
        public int? Deposito { get; set; }

        [ConcurrencyCheck]
        public string Nombre { get; set; }

        [ConcurrencyCheck]
        public decimal? Importe { get; set; }

        [ConcurrencyCheck]
        public string Apellido { get; set; }

        [ConcurrencyCheck]
        public bool? ImporteDevuelto { get; set; }

        [ConcurrencyCheck]
        public string Referencia { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public Deposito Deposito1 { get; set; }

    }
}