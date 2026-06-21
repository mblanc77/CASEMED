using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Departamento", Schema = "dbo")]
    public partial class Departamento
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
        public string Nombre { get; set; }

        [ConcurrencyCheck]
        public int? Regional { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public ICollection<AgenteCobranza> AgenteCobranzas { get; set; }

        public ICollection<Colegiado> Colegiados { get; set; }

        public Regional Regional1 { get; set; }

        public ICollection<LugarRetiroCarne> LugarRetiroCarnes { get; set; }

        public ICollection<RegistroColegiado> RegistroColegiados { get; set; }

    }
}