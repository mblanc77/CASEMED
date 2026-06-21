using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("OrigenMovimiento", Schema = "dbo")]
    public partial class OrigenMovimiento
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
        public string Descripcion { get; set; }

        [ConcurrencyCheck]
        public bool? Activo { get; set; }

        [ConcurrencyCheck]
        public bool? EsCMU { get; set; }

        [ConcurrencyCheck]
        public bool? HabilitadoEnManual { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        public ICollection<AgenteCobranza> AgenteCobranzas { get; set; }

        public ICollection<MovimientoCuentum> MovimientoCuenta { get; set; }

        public XpObjectType XpobjectType { get; set; }

    }
}