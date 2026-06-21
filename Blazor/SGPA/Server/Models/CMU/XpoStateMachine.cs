using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("XpoStateMachine", Schema = "dbo")]
    public partial class XpoStateMachine
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
        public string Name { get; set; }

        [ConcurrencyCheck]
        public bool? Active { get; set; }

        [ConcurrencyCheck]
        public string TargetObjectType { get; set; }

        [ConcurrencyCheck]
        public string StatePropertyName { get; set; }

        [ConcurrencyCheck]
        public Guid? StartState { get; set; }

        [ConcurrencyCheck]
        public bool? ExpandActionsInDetailView { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public ICollection<XpoState> XpoStates { get; set; }

        public XpoState XpoState { get; set; }

    }
}