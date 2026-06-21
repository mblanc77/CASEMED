using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Liquidacion_BPS", Schema = "dbo")]
    public partial class LiquidacionBp
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
        public int LiquidacionBPSId { get; set; }

        [Required]
        [ConcurrencyCheck]
        public double CI { get; set; }

        [ConcurrencyCheck]
        public string NOMBRE { get; set; }

        [ConcurrencyCheck]
        public string APELLIDO { get; set; }

        [ConcurrencyCheck]
        public double? MONTO_TOTAL { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int MES_DE_CARGO { get; set; }

        [ConcurrencyCheck]
        public string NOM_EMPRESA { get; set; }

        [Required]
        [ConcurrencyCheck]
        public double PCT_POR_EMPRESA { get; set; }

        [ConcurrencyCheck]
        public DateTime? FECHA_PER_DESDE { get; set; }

        [ConcurrencyCheck]
        public DateTime? FECHA_PER_HASTA { get; set; }

        [Column("N_ ENTREGA")]
        [ConcurrencyCheck]
        public int? N_ENTREGA { get; set; }

        [ConcurrencyCheck]
        public DateTime? FECHA_DE_ENTREGA { get; set; }

        [ConcurrencyCheck]
        public short? MES { get; set; }

        [ConcurrencyCheck]
        public short? ANIO { get; set; }

        [ConcurrencyCheck]
        public double? LIQUIDO { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

    }
}