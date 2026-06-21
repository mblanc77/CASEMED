using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("UniversidadTituloGrado", Schema = "dbo")]
    public partial class UniversidadTituloGrado
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
        public bool? Extranjera { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public ICollection<Colegiado> Colegiados { get; set; }

        public ICollection<RegistroColegiado> RegistroColegiados { get; set; }

        public ICollection<TramiteInfoadjuntatitulo> TramiteInfoadjuntatitulos { get; set; }

    }
}