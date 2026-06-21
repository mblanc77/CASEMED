using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Cobro", Schema = "dbo")]
    public partial class Cobro
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
        public short? MesCargo { get; set; }

        [ConcurrencyCheck]
        public short? AñoCargo { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public decimal? MontoDepositado { get; set; }

        [ConcurrencyCheck]
        public int? AgenteCobranza { get; set; }

        [ConcurrencyCheck]
        public int? CuentaBancaria { get; set; }

        [ConcurrencyCheck]
        public bool? PreCargado { get; set; }

        [ConcurrencyCheck]
        public int? IdExterno { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        [ConcurrencyCheck]
        public string Comentario { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIniCargo { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaFinCargo { get; set; }

        public AgenteCobranza AgenteCobranza1 { get; set; }

        public CuentaBancarium CuentaBancarium { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public ICollection<CobroNomina> CobroNominas { get; set; }

        public ICollection<Debito> Debitos { get; set; }

        public ICollection<Deposito> Depositos { get; set; }

    }
}