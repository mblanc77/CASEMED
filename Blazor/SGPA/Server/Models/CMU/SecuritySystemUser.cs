using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SecuritySystemUser", Schema = "dbo")]
    public partial class SecuritySystemUser
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
        public string StoredPassword { get; set; }

        [ConcurrencyCheck]
        public bool? ChangePasswordOnFirstLogon { get; set; }

        [ConcurrencyCheck]
        public string UserName { get; set; }

        [ConcurrencyCheck]
        public bool? IsActive { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public ICollection<SecuritysystemuserusersSecuritysystemrolerole> SecuritysystemuserusersSecuritysystemroleroles { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }

    }
}