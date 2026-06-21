using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Tramite_InfoAdjuntaEspecialidad", Schema = "dbo")]
    public partial class TramiteInfoadjuntaespecialidad
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
        public string Facultad { get; set; }

        [ConcurrencyCheck]
        public int? TipoReconocimiento { get; set; }

        [ConcurrencyCheck]
        public int? Especialidad { get; set; }

        [ConcurrencyCheck]
        public string Institucion { get; set; }

        public Especialidad Especialidad1 { get; set; }

        public TramiteInfoadjuntatitulo TramiteInfoAdjuntaTitulo { get; set; }

    }
}