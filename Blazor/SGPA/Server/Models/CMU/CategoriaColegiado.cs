using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("CategoriaColegiado", Schema = "dbo")]
    public partial class CategoriaColegiado
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
        public int? ValorTipo { get; set; }

        [ConcurrencyCheck]
        public decimal? ValorActual { get; set; }

        [ConcurrencyCheck]
        public bool? UsoInterno { get; set; }

        [ConcurrencyCheck]
        public bool? AceptaCobros { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? CategoriaDependiente { get; set; }

        [ConcurrencyCheck]
        public string FormulaDependencia { get; set; }

        [ConcurrencyCheck]
        public bool? Moroso { get; set; }

        [ConcurrencyCheck]
        public bool? Suspendido { get; set; }

        public CategoriaColegiado CategoriaColegiado1 { get; set; }

        public ICollection<CategoriaColegiado> CategoriaColegiados1 { get; set; }

        public ICollection<CategoriaColegiadoValor> CategoriaColegiadoValors { get; set; }

        public ICollection<Colegiado> Colegiados { get; set; }

        public ICollection<ColegiadoCambioCategorium> ColegiadoCambioCategoria { get; set; }

        public ICollection<DeclaracionJuradaTipo> DeclaracionJuradaTipos { get; set; }

        public ICollection<MovimientoCuentaCuotum> MovimientoCuentaCuota { get; set; }

        public ICollection<Parametro> Parametros { get; set; }

        public ICollection<Parametro> Parametros1 { get; set; }

    }
}