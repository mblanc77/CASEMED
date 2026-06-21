using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("AjusteDetalle", Schema = "dbo")]
    public partial class AjusteDetalle
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
        public int? Ajuste { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaOrigen { get; set; }

        [ConcurrencyCheck]
        public decimal? Importe { get; set; }

        [ConcurrencyCheck]
        public string Observaciones { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public AjusteRetroactivo AjusteRetroactivo { get; set; }

    }
}