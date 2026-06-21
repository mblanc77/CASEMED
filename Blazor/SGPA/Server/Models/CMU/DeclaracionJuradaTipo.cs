using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("DeclaracionJuradaTipo", Schema = "dbo")]
    public partial class DeclaracionJuradaTipo
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
        public int? Categoria { get; set; }

        [ConcurrencyCheck]
        public string FechaVtoFormula { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public bool? Anulable { get; set; }

        public ICollection<ColegiadoDeclaracionJuradum> ColegiadoDeclaracionJurada { get; set; }

        public CategoriaColegiado CategoriaColegiado { get; set; }

        public ICollection<Parametro> Parametros { get; set; }

        public ICollection<Parametro> Parametros1 { get; set; }

    }
}