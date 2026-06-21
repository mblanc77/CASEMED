using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("InformeEstadistico", Schema = "dbo")]
    public partial class InformeEstadistico
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
        public int IdRpt { get; set; }

        [ConcurrencyCheck]
        public string Grupo { get; set; }

        [ConcurrencyCheck]
        public int? Orden { get; set; }

        [ConcurrencyCheck]
        public string TituloPantalla { get; set; }

        [ConcurrencyCheck]
        public string TituloRpt { get; set; }

        [ConcurrencyCheck]
        public bool? MesAnio { get; set; }

        [ConcurrencyCheck]
        public bool? Periodo { get; set; }

        [ConcurrencyCheck]
        public bool? Empresa { get; set; }

        [ConcurrencyCheck]
        public bool? Fecha { get; set; }

        [ConcurrencyCheck]
        public bool? GrupoEtario { get; set; }

        [ConcurrencyCheck]
        public string Comentario { get; set; }

        [ConcurrencyCheck]
        public bool? Patologia { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

    }
}