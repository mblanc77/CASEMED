using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Deposito", Schema = "dbo")]
    public partial class Deposito
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
        public string Comentario { get; set; }

        [ConcurrencyCheck]
        public int? Estado { get; set; }

        [ConcurrencyCheck]
        public string ComentarioCMU { get; set; }

        [ConcurrencyCheck]
        public decimal? MontoNomina { get; set; }

        [ConcurrencyCheck]
        public decimal? MontoAcreditado { get; set; }

        [ConcurrencyCheck]
        public decimal? MontoNOAcreditado { get; set; }

        [ConcurrencyCheck]
        public int? Resultado { get; set; }

        [ConcurrencyCheck]
        public string FileName { get; set; }

        [ConcurrencyCheck]
        public bool? IgnorarBajas { get; set; }

        public Cobro Cobro { get; set; }

        public ICollection<DepositoNomina> DepositoNominas { get; set; }

        public ICollection<DepositoNominaNoIdentificadum> DepositoNominaNoIdentificada { get; set; }

    }
}