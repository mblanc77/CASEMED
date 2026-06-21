using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Certificacion", Schema = "dbo")]
    public partial class Certificacion
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
        public int NroLlamado { get; set; }

        [ConcurrencyCheck]
        public int? CI { get; set; }

        [ConcurrencyCheck]
        public int? NroRecibo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaRecibido { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaCertificacion { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIni { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaFin { get; set; }

        [ConcurrencyCheck]
        public short? CodAfeccionTipo { get; set; }

        [ConcurrencyCheck]
        public short? CodCertificador { get; set; }

        [ConcurrencyCheck]
        public short? CodSalidaTipo { get; set; }

        [ConcurrencyCheck]
        public bool? Efectiva { get; set; }

        [ConcurrencyCheck]
        public string Indicaciones { get; set; }

        [ConcurrencyCheck]
        public double? ImporteDeducible { get; set; }

        [ConcurrencyCheck]
        public bool? Trabaja { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public Afiliado Afiliado { get; set; }

        public AfeccionTipo AfeccionTipo { get; set; }

        public Certificador Certificador { get; set; }

        public SalidaTipo SalidaTipo { get; set; }

    }
}