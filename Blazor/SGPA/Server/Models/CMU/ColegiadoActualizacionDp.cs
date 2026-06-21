using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("ColegiadoActualizacionDP", Schema = "dbo")]
    public partial class ColegiadoActualizacionDp
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
        public string FieldName { get; set; }

        [ConcurrencyCheck]
        public string OldValue { get; set; }

        [ConcurrencyCheck]
        public string NewValue { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaYHora { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public Colegiado Colegiado1 { get; set; }

    }
}