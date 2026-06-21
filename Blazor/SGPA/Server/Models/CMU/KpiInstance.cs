using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("KpiInstance", Schema = "dbo")]
    public partial class KpiInstance
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
        public DateTime? ForceMeasurementDateTime { get; set; }

        [ConcurrencyCheck]
        public Guid? KpiDefinition { get; set; }

        [ConcurrencyCheck]
        public string Settings { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public ICollection<KpiDefinition> KpiDefinitions { get; set; }

        public ICollection<KpiHistoryItem> KpiHistoryItems { get; set; }

        public KpiDefinition KpiDefinition1 { get; set; }

        public ICollection<KpiscorecardscorecardsKpiinstanceindicator> KpiscorecardscorecardsKpiinstanceindicators { get; set; }

    }
}