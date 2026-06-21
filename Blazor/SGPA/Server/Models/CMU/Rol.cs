using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Rol", Schema = "dbo")]
    public partial class Rol
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

        public SecuritySystemRole SecuritySystemRole { get; set; }

        public ICollection<RolrolesMovimientotipomovimientostipo> RolrolesMovimientotipomovimientostipos { get; set; }

    }
}