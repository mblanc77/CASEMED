using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("DepositoNominaMultiBROU", Schema = "dbo")]
    public partial class DepositoNominaMultiBrou
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
        public string CodAutorizacion { get; set; }

        [ConcurrencyCheck]
        public string IdBanco { get; set; }

        [ConcurrencyCheck]
        public string NroTRNEmpresa { get; set; }

        [ConcurrencyCheck]
        public string IdFactura { get; set; }

        public DepositoNomina DepositoNomina { get; set; }

    }
}