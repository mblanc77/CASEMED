using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("EmailEnvio", Schema = "dbo")]
    public partial class EmailEnvio
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
        public DateTime? FechaHoraEnvio { get; set; }

        [ConcurrencyCheck]
        public string EMailRemitente { get; set; }

        [ConcurrencyCheck]
        public string Titulo { get; set; }

        [ConcurrencyCheck]
        public string Contenido { get; set; }

        [ConcurrencyCheck]
        public string Criterio { get; set; }

        [ConcurrencyCheck]
        public string CantidadEMailsAEnviar { get; set; }

        [ConcurrencyCheck]
        public string CantidadEMailEnviados { get; set; }

        [ConcurrencyCheck]
        public int? Estado { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

    }
}