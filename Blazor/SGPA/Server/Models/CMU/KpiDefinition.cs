using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("KpiDefinition", Schema = "dbo")]
    public partial class KpiDefinition
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
        public string TargetObjectType { get; set; }

        [ConcurrencyCheck]
        public DateTime? Changed { get; set; }

        [ConcurrencyCheck]
        public Guid? KpiInstance { get; set; }

        [ConcurrencyCheck]
        public string Name { get; set; }

        [ConcurrencyCheck]
        public bool? Active { get; set; }

        [ConcurrencyCheck]
        public string Criteria { get; set; }

        [ConcurrencyCheck]
        public string Expression { get; set; }

        [ConcurrencyCheck]
        public double? GreenZone { get; set; }

        [ConcurrencyCheck]
        public double? RedZone { get; set; }

        [ConcurrencyCheck]
        public string Range { get; set; }

        [ConcurrencyCheck]
        public bool? Compare { get; set; }

        [ConcurrencyCheck]
        public string RangeToCompare { get; set; }

        [ConcurrencyCheck]
        public int? MeasurementFrequency { get; set; }

        [ConcurrencyCheck]
        public int? MeasurementMode { get; set; }

        [ConcurrencyCheck]
        public int? Direction { get; set; }

        [ConcurrencyCheck]
        public DateTime? ChangedOn { get; set; }

        [ConcurrencyCheck]
        public string SuppressedSeries { get; set; }

        [ConcurrencyCheck]
        public bool? EnableCustomizeRepresentation { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public KpiInstance KpiInstance1 { get; set; }

        public ICollection<KpiInstance> KpiInstances { get; set; }

    }
}