using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Usuario", Schema = "dbo")]
    public partial class Usuario
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

        public SecuritySystemUser SecuritySystemUser { get; set; }

        public ICollection<UsuarioAcceso> UsuarioAccesos { get; set; }

        public ICollection<UsuarioInstitucion> UsuarioInstitucions { get; set; }

        public ICollection<UsuarioRegional> UsuarioRegionals { get; set; }

    }
}