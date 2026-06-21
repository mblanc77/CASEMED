using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("XPObjectModified", Schema = "dbo")]
    public partial class XpObjectModified
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ConcurrencyCheck]
        public int? XPObjectType { get; set; }

        [ConcurrencyCheck]
        public int? ObjectKey { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public XpObjectType XpobjectType { get; set; }

    }
}