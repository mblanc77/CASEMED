using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("SubsidioEnfermedad", Schema = "dbo")]
    public partial class SubsidioEnfermedad
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [Required]
        [ConcurrencyCheck]
        public int IdSubsidio { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime FechaIni { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaFin { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIniSubsidio { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaFinSubsidio { get; set; }

        [ConcurrencyCheck]
        public int? NroLlamado { get; set; }

        [ConcurrencyCheck]
        public byte? Dias { get; set; }

        [ConcurrencyCheck]
        public double? Importe { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubsidioEnfermedadId { get; set; }

        public SubsidioCabezal SubsidioCabezal { get; set; }

    }
}