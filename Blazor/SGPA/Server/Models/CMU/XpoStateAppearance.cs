using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("XpoStateAppearance", Schema = "dbo")]
    public partial class XpoStateAppearance
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
        public Guid? State { get; set; }

        [ConcurrencyCheck]
        public string AppearanceItemType { get; set; }

        [ConcurrencyCheck]
        public string Context { get; set; }

        [ConcurrencyCheck]
        public string Criteria { get; set; }

        [ConcurrencyCheck]
        public string Method { get; set; }

        [ConcurrencyCheck]
        public string TargetItems { get; set; }

        [ConcurrencyCheck]
        public int? Priority { get; set; }

        [ConcurrencyCheck]
        public int? FontColor { get; set; }

        [ConcurrencyCheck]
        public int? BackColor { get; set; }

        [ConcurrencyCheck]
        public int? FontStyle { get; set; }

        [ConcurrencyCheck]
        public bool? Enabled { get; set; }

        [ConcurrencyCheck]
        public int? Visibility { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public XpoState XpoState { get; set; }

    }
}