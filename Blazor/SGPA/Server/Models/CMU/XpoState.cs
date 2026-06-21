using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("XpoState", Schema = "dbo")]
    public partial class XpoState
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
        public string Caption { get; set; }

        [ConcurrencyCheck]
        public Guid? StateMachine { get; set; }

        [ConcurrencyCheck]
        public string MarkerValue { get; set; }

        [ConcurrencyCheck]
        public string TargetObjectCriteria { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public XpoStateMachine XpoStateMachine { get; set; }

        public ICollection<XpoStateAppearance> XpoStateAppearances { get; set; }

        public ICollection<XpoStateMachine> XpoStateMachines { get; set; }

        public ICollection<XpoTransition> XpoTransitions { get; set; }

        public ICollection<XpoTransition> XpoTransitions1 { get; set; }

    }
}