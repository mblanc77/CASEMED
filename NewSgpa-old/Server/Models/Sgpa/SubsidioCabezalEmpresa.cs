using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("SubsidioCabezalEmpresa", Schema = "dbo")]
    public partial class SubsidioCabezalEmpresa
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
        public short CodEmpresa { get; set; }

        [ConcurrencyCheck]
        public float? ValorJornal { get; set; }

        [ConcurrencyCheck]
        public short? Dias { get; set; }

        [ConcurrencyCheck]
        public double? ImpNominal { get; set; }

        [ConcurrencyCheck]
        public double? ImpAguinaldo { get; set; }

        [ConcurrencyCheck]
        public double? ImpLiquido { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubsidioCabezalempresaId { get; set; }

        public Empresa Empresa { get; set; }

        public SubsidioCabezal SubsidioCabezal { get; set; }

    }
}