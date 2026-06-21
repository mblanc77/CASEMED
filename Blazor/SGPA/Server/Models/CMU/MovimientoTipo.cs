using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("MovimientoTipo", Schema = "dbo")]
    public partial class MovimientoTipo
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
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string Descripcion { get; set; }

        [ConcurrencyCheck]
        public decimal? Signo { get; set; }

        [ConcurrencyCheck]
        public bool? HabilitadoEnManual { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public ICollection<MovimientoCuentum> MovimientoCuenta { get; set; }

        public ICollection<Parametro> Parametros { get; set; }

        public ICollection<Parametro> Parametros1 { get; set; }

        public ICollection<Parametro> Parametros2 { get; set; }

        public ICollection<Parametro> Parametros3 { get; set; }

        public ICollection<RolrolesMovimientotipomovimientostipo> RolrolesMovimientotipomovimientostipos { get; set; }

    }
}