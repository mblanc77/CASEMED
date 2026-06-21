using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("XpoTransition", Schema = "dbo")]
    public partial class XpoTransition
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
        public Guid? SourceState { get; set; }

        [ConcurrencyCheck]
        public Guid? TargetState { get; set; }

        [ConcurrencyCheck]
        public int? Index { get; set; }

        [ConcurrencyCheck]
        public bool? SaveAndCloseView { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public XpoState XpoState { get; set; }

        public XpoState XpoState1 { get; set; }

    }
}