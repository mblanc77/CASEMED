using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("ColegiadoCambioCategoria", Schema = "dbo")]
    public partial class ColegiadoCambioCategorium
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
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public int? Categoria { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaDesde { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaHasta { get; set; }

        [ConcurrencyCheck]
        public string Comentarios { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public ICollection<AjusteRetroactivo> AjusteRetroactivos { get; set; }

        public CategoriaColegiado CategoriaColegiado { get; set; }

        public Colegiado Colegiado1 { get; set; }

    }
}