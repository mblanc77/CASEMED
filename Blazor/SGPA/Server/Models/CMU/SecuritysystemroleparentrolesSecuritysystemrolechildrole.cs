using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SecuritySystemRoleParentRoles_SecuritySystemRoleChildRoles", Schema = "dbo")]
    public partial class SecuritysystemroleparentrolesSecuritysystemrolechildrole
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
        public Guid? ChildRoles { get; set; }

        [ConcurrencyCheck]
        public Guid? ParentRoles { get; set; }

        [Key]
        [Required]
        public Guid OID { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public SecuritySystemRole SecuritySystemRole { get; set; }

        public SecuritySystemRole SecuritySystemRole1 { get; set; }

    }
}