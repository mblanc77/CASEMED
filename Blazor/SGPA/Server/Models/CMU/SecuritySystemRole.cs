using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SecuritySystemRole", Schema = "dbo")]
    public partial class SecuritySystemRole
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
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        [ConcurrencyCheck]
        public string Name { get; set; }

        [ConcurrencyCheck]
        public bool? IsAdministrative { get; set; }

        [ConcurrencyCheck]
        public bool? CanEditModel { get; set; }

        public ICollection<Rol> Rols { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public ICollection<SecuritysystemroleparentrolesSecuritysystemrolechildrole> SecuritysystemroleparentrolesSecuritysystemrolechildroles { get; set; }

        public ICollection<SecuritysystemroleparentrolesSecuritysystemrolechildrole> SecuritysystemroleparentrolesSecuritysystemrolechildroles1 { get; set; }

        public ICollection<SecuritySystemTypePermissionsObject> SecuritySystemTypePermissionsObjects { get; set; }

        public ICollection<SecuritysystemuserusersSecuritysystemrolerole> SecuritysystemuserusersSecuritysystemroleroles { get; set; }

    }
}