using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("KpiHistoryItem", Schema = "dbo")]
    public partial class KpiHistoryItem
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
        public Guid Oid { get; set; }

        [ConcurrencyCheck]
        public Guid? KpiInstance { get; set; }

        [ConcurrencyCheck]
        public DateTime? RangeStart { get; set; }

        [ConcurrencyCheck]
        public DateTime? RangeEnd { get; set; }

        [ConcurrencyCheck]
        public double? Value { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public KpiInstance KpiInstance1 { get; set; }

    }
}