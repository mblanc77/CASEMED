using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("RolRoles_MovimientoTipoMovimientosTipo", Schema = "dbo")]
    public partial class RolrolesMovimientotipomovimientostipo
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
        public int? MovimientosTipo { get; set; }

        [ConcurrencyCheck]
        public Guid? Roles { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OID { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public MovimientoTipo MovimientoTipo { get; set; }

        public Rol Rol { get; set; }

    }
}