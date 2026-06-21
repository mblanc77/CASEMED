using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("AuditDataItemPersistent", Schema = "dbo")]
    public partial class AuditDataItemPersistent
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
        public string UserName { get; set; }

        [ConcurrencyCheck]
        public DateTime? ModifiedOn { get; set; }

        [ConcurrencyCheck]
        public string OperationType { get; set; }

        [ConcurrencyCheck]
        public string Description { get; set; }

        [ConcurrencyCheck]
        public Guid? AuditedObject { get; set; }

        [ConcurrencyCheck]
        public Guid? OldObject { get; set; }

        [ConcurrencyCheck]
        public Guid? NewObject { get; set; }

        [ConcurrencyCheck]
        public string OldValue { get; set; }

        [ConcurrencyCheck]
        public string NewValue { get; set; }

        [ConcurrencyCheck]
        public string PropertyName { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public AuditedObjectWeakReference AuditedObjectWeakReference { get; set; }

        public XpWeakReference XpweakReference { get; set; }

        public XpWeakReference XpweakReference1 { get; set; }

    }
}