using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SecuritySystemUserUsers_SecuritySystemRoleRoles", Schema = "dbo")]
    public partial class SecuritysystemuserusersSecuritysystemrolerole
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [ConcurrencyCheck]
        public Guid? Roles { get; set; }

        [ConcurrencyCheck]
        public Guid? Users { get; set; }

        [Key]
        [Required]
        public Guid OID { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public SecuritySystemRole SecuritySystemRole { get; set; }

        public SecuritySystemUser SecuritySystemUser { get; set; }

    }
}