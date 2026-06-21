using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SecuritySystemTypePermissionsObject", Schema = "dbo")]
    public partial class SecuritySystemTypePermissionsObject
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
        public string TargetType { get; set; }

        [ConcurrencyCheck]
        public bool? AllowRead { get; set; }

        [ConcurrencyCheck]
        public bool? AllowWrite { get; set; }

        [ConcurrencyCheck]
        public bool? AllowCreate { get; set; }

        [ConcurrencyCheck]
        public bool? AllowDelete { get; set; }

        [ConcurrencyCheck]
        public bool? AllowNavigate { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        [ConcurrencyCheck]
        public Guid? Owner { get; set; }

        public ICollection<SecuritySystemMemberPermissionsObject> SecuritySystemMemberPermissionsObjects { get; set; }

        public ICollection<SecuritySystemObjectPermissionsObject> SecuritySystemObjectPermissionsObjects { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public SecuritySystemRole SecuritySystemRole { get; set; }

    }
}