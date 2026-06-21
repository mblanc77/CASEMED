using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SecuritySystemMemberPermissionsObject", Schema = "dbo")]
    public partial class SecuritySystemMemberPermissionsObject
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
        public string Members { get; set; }

        [ConcurrencyCheck]
        public bool? AllowRead { get; set; }

        [ConcurrencyCheck]
        public bool? AllowWrite { get; set; }

        [ConcurrencyCheck]
        public Guid? Owner { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public string Criteria { get; set; }

        public SecuritySystemTypePermissionsObject SecuritySystemTypePermissionsObject { get; set; }

    }
}