using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("SubsidioCabezal", Schema = "dbo")]
    public partial class SubsidioCabezal
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
        public int IdSubsidio { get; set; }

        [ConcurrencyCheck]
        public byte? Mes { get; set; }

        [ConcurrencyCheck]
        public short? Anio { get; set; }

        [ConcurrencyCheck]
        public int? CI { get; set; }

        [ConcurrencyCheck]
        public bool? Liquidar { get; set; }

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
        public int? NroRecibo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaPago { get; set; }

        [ConcurrencyCheck]
        public int? CodBanco { get; set; }

        [ConcurrencyCheck]
        public string NroCuenta { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public Afiliado Afiliado { get; set; }

        public ICollection<SubsidiocabezalBp> SubsidiocabezalBps { get; set; }

        public ICollection<SubsidioCabezalEmpresa> SubsidioCabezalEmpresas { get; set; }

        public ICollection<SubsidioEnfermedad> SubsidioEnfermedads { get; set; }

        public ICollection<SubsidioItem> SubsidioItems { get; set; }

    }
}