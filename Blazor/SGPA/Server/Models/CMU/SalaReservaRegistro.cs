using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SalaReservaRegistro", Schema = "dbo")]
    public partial class SalaReservaRegistro
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
        public int? Reserva { get; set; }

        [ConcurrencyCheck]
        public int? Documento { get; set; }

        [ConcurrencyCheck]
        public string Telefono { get; set; }

        [ConcurrencyCheck]
        public string EMail { get; set; }

        [ConcurrencyCheck]
        public bool? Declaro1 { get; set; }

        [ConcurrencyCheck]
        public bool? Declaro2 { get; set; }

        [ConcurrencyCheck]
        public bool? Declaro3 { get; set; }

        [ConcurrencyCheck]
        public int? Estado { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaHoraIngreso { get; set; }

        [ConcurrencyCheck]
        public string Nombre { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaEnvioCorreo { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public SalaReserva SalaReserva { get; set; }

    }
}