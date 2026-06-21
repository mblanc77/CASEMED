using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("AgenteCobranzaTipo", Schema = "dbo")]
    public partial class AgenteCobranzaTipo
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
        public string Descripcion { get; set; }

        [ConcurrencyCheck]
        public bool? UnDepositoPorMes { get; set; }

        [ConcurrencyCheck]
        public bool? CargaDepositos { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public ICollection<AgenteCobranza> AgenteCobranzas { get; set; }

    }
}