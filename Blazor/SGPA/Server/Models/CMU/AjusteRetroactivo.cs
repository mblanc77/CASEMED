using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("AjusteRetroactivo", Schema = "dbo")]
    public partial class AjusteRetroactivo
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
        public int? Tipo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaYHora { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaAnulado { get; set; }

        [ConcurrencyCheck]
        public int? AnulaA { get; set; }

        [ConcurrencyCheck]
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public int? DeclaracionJurada { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? CambioCategoria { get; set; }

        public ICollection<AjusteDetalle> AjusteDetalles { get; set; }

        public AjusteRetroactivo AjusteRetroactivo1 { get; set; }

        public ICollection<AjusteRetroactivo> AjusteRetroactivos1 { get; set; }

        public ColegiadoCambioCategorium ColegiadoCambioCategorium { get; set; }

        public Colegiado Colegiado1 { get; set; }

        public ColegiadoDeclaracionJuradum ColegiadoDeclaracionJuradum { get; set; }

        public ICollection<MovimientoCuentum> MovimientoCuenta { get; set; }

    }
}