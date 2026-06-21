using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("SalaReserva", Schema = "dbo")]
    public partial class SalaReserva
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
        public int? Organizador { get; set; }

        [ConcurrencyCheck]
        public string NombreEvento { get; set; }

        [ConcurrencyCheck]
        public string NombreResponsable { get; set; }

        [ConcurrencyCheck]
        public short? Disponibilidad { get; set; }

        [ConcurrencyCheck]
        public string NroActa { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public DateTime? HoraDesde { get; set; }

        [ConcurrencyCheck]
        public DateTime? HoraHasta { get; set; }

        [ConcurrencyCheck]
        public int? Sala { get; set; }

        [ConcurrencyCheck]
        public string EMailNotificacion { get; set; }

        [ConcurrencyCheck]
        public Guid? Guid { get; set; }

        [ConcurrencyCheck]
        public Guid? Folleto { get; set; }

        [ConcurrencyCheck]
        public string Direccion { get; set; }

        [ConcurrencyCheck]
        public string Telefono { get; set; }

        [ConcurrencyCheck]
        public string Detalle { get; set; }

        [ConcurrencyCheck]
        public short? HorasPreviasRegistro { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaEnvioEMailConfirmacion { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public MyFileDatum MyFileDatum { get; set; }

        public SalaOrganizador SalaOrganizador { get; set; }

        public SalaCmu SalaCmu { get; set; }

        public ICollection<SalaReservaRegistro> SalaReservaRegistros { get; set; }

    }
}