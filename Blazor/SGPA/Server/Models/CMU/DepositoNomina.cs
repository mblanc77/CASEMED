using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("DepositoNomina", Schema = "dbo")]
    public partial class DepositoNomina
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
        public int? Deposito { get; set; }

        [ConcurrencyCheck]
        public int? TipoAporte { get; set; }

        [ConcurrencyCheck]
        public int? TipoAporteANT { get; set; }

        public Deposito Deposito1 { get; set; }

        public CobroNomina CobroNomina { get; set; }

        public ICollection<DepositoNominaMultiBrou> DepositoNominaMultiBrous { get; set; }

        public ICollection<DepositoNominaRedPago> DepositoNominaRedPagos { get; set; }

    }
}