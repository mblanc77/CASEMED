using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("ColegiadoTarjetaDebitoAsociada", Schema = "dbo")]
    public partial class ColegiadoTarjetaDebitoAsociadum
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
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public string NumeroTarjeta { get; set; }

        [ConcurrencyCheck]
        public string Vencimiento { get; set; }

        [ConcurrencyCheck]
        public int? AgenteDebito { get; set; }

        [ConcurrencyCheck]
        public short? VtoMes { get; set; }

        [ConcurrencyCheck]
        public short? VtoAño { get; set; }

        [ConcurrencyCheck]
        public int? VtoAñoMes { get; set; }

        [ConcurrencyCheck]
        public bool? ExcluirFiltroVencimiento { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? DocumentoTitular { get; set; }

        [ConcurrencyCheck]
        public string NombreTitular { get; set; }

        [ConcurrencyCheck]
        public bool? AltaEnviada { get; set; }

        [ConcurrencyCheck]
        public int? TipoMovimiento { get; set; }

        [ConcurrencyCheck]
        public int? TipoLiquidacion { get; set; }

        [ConcurrencyCheck]
        public int? Origen { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaVerificacion { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaSolicitud { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIngreso { get; set; }

        [ConcurrencyCheck]
        public string NumeroTarjetaOrig { get; set; }

        [ConcurrencyCheck]
        public int? Ambito { get; set; }

        public AgenteCobranzaDebito AgenteCobranzaDebito { get; set; }

        public Colegiado Colegiado1 { get; set; }

    }
}