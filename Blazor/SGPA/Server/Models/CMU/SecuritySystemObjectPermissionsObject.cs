using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SecuritySystemObjectPermissionsObject", Schema = "dbo")]
    public partial class SecuritySystemObjectPermissionsObject
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
        public string Criteria { get; set; }

        [ConcurrencyCheck]
        public bool? AllowRead { get; set; }

        [ConcurrencyCheck]
        public bool? AllowWrite { get; set; }

        [ConcurrencyCheck]
        public bool? AllowDelete { get; set; }

        [ConcurrencyCheck]
        public bool? AllowNavigate { get; set; }

        [ConcurrencyCheck]
        public Guid? Owner { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public SecuritySystemTypePermissionsObject SecuritySystemTypePermissionsObject { get; set; }

    }
}