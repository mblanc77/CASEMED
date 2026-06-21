using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Tramite_InfoAdjuntaTitulo", Schema = "dbo")]
    public partial class TramiteInfoadjuntatitulo
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
        public int OID { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaTitulo { get; set; }

        [ConcurrencyCheck]
        public string IdentificadorFrente { get; set; }

        [ConcurrencyCheck]
        public string IdentificadorReverso { get; set; }

        [ConcurrencyCheck]
        public int? Universidad { get; set; }

        [ConcurrencyCheck]
        public string IdentificadorConstanciaMSP { get; set; }

        public ICollection<TramiteInfoadjuntaespecialidad> TramiteInfoadjuntaespecialidads { get; set; }

        public TramiteInfoadjuntabase TramiteInfoAdjuntaBase { get; set; }

        public UniversidadTituloGrado UniversidadTituloGrado { get; set; }

    }
}