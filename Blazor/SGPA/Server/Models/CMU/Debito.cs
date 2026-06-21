using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Debito", Schema = "dbo")]
    public partial class Debito
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
        public int? NumeroCabezal { get; set; }

        [ConcurrencyCheck]
        public string CodigoComercio { get; set; }

        [ConcurrencyCheck]
        public string NumeroSucursal { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaCargaResultado { get; set; }

        [ConcurrencyCheck]
        public string Identificacion { get; set; }

        [ConcurrencyCheck]
        public int? AgenteDebito { get; set; }

        public AgenteCobranzaDebito AgenteCobranzaDebito { get; set; }

        public Cobro Cobro { get; set; }

        public ICollection<DebitoAdjunto> DebitoAdjuntos { get; set; }

        public ICollection<DebitoNomina> DebitoNominas { get; set; }

    }
}