using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("AgenteCobranzaDebito", Schema = "dbo")]
    public partial class AgenteCobranzaDebito
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
        public string CodigoComercio { get; set; }

        [ConcurrencyCheck]
        public string NumeroSucursal { get; set; }

        [ConcurrencyCheck]
        public string CodigoMoneda { get; set; }

        [ConcurrencyCheck]
        public string PlanVenta { get; set; }

        [ConcurrencyCheck]
        public string TipoTransaccion { get; set; }

        [ConcurrencyCheck]
        public string TipoLiquidacion { get; set; }

        [ConcurrencyCheck]
        public string CambioTarjetaClassName { get; set; }

        public AgenteCobranza AgenteCobranza { get; set; }

        public ICollection<ColegiadoDebitoBancarioAsociado> ColegiadoDebitoBancarioAsociados { get; set; }

        public ICollection<ColegiadoTarjetaDebitoAsociadum> ColegiadoTarjetaDebitoAsociada { get; set; }

        public ICollection<Debito> Debitos { get; set; }

        public ICollection<Parametro> Parametros { get; set; }

    }
}