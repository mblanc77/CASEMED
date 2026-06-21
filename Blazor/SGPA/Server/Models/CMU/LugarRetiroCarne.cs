using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("LugarRetiroCarne", Schema = "dbo")]
    public partial class LugarRetiroCarne
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
        public int? Departamento { get; set; }

        [ConcurrencyCheck]
        public int? Grupo { get; set; }

        [ConcurrencyCheck]
        public string Comentarios { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public Departamento Departamento1 { get; set; }

        public GrupoLugarRetiroCarne GrupoLugarRetiroCarne { get; set; }

        public ICollection<TramiteCarne> TramiteCarnes { get; set; }

    }
}