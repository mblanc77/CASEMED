using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Regional", Schema = "dbo")]
    public partial class Regional
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
        public int? DNNRoleId { get; set; }

        [ConcurrencyCheck]
        public int? Region { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public ICollection<Colegiado> Colegiados { get; set; }

        public ICollection<Departamento> Departamentos { get; set; }

        public Region Region1 { get; set; }

        public ICollection<RegionalregionalesCuentabancariacuentabancaria> RegionalregionalesCuentabancariacuentabancaria { get; set; }

        public ICollection<UsuarioRegional> UsuarioRegionals { get; set; }

    }
}