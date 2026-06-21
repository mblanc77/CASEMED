using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("KpiScorecardScorecards_KpiInstanceIndicators", Schema = "dbo")]
    public partial class KpiscorecardscorecardsKpiinstanceindicator
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
        public Guid? Indicators { get; set; }

        [ConcurrencyCheck]
        public Guid? Scorecards { get; set; }

        [Key]
        [Required]
        public Guid OID { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public KpiInstance KpiInstance { get; set; }

        public KpiScorecard KpiScorecard { get; set; }

    }
}