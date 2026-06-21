using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("XPWeakReference", Schema = "dbo")]
    public partial class XpWeakReference
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
        public int? TargetType { get; set; }

        [ConcurrencyCheck]
        public string TargetKey { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        public ICollection<AuditDataItemPersistent> AuditDataItemPersistents { get; set; }

        public ICollection<AuditDataItemPersistent> AuditDataItemPersistents1 { get; set; }

        public ICollection<AuditedObjectWeakReference> AuditedObjectWeakReferences { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public XpObjectType XpobjectType1 { get; set; }

    }
}