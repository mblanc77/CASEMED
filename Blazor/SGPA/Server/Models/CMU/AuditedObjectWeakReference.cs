using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("AuditedObjectWeakReference", Schema = "dbo")]
    public partial class AuditedObjectWeakReference
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
        public Guid? GuidId { get; set; }

        [ConcurrencyCheck]
        public int? IntId { get; set; }

        [ConcurrencyCheck]
        public string DisplayName { get; set; }

        public ICollection<AuditDataItemPersistent> AuditDataItemPersistents { get; set; }

        public XpWeakReference XpweakReference { get; set; }

    }
}