using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("ColegiadoBitacoraNota", Schema = "dbo")]
    public partial class ColegiadoBitacoraNotum
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
        public string Contenido { get; set; }

        public ICollection<ColegiadoBitacoraEMailEnvio> ColegiadoBitacoraEMailEnvios { get; set; }

        public ICollection<ColegiadoBitacoraEMailRecepcion> ColegiadoBitacoraEMailRecepcions { get; set; }

        public ColegiadoBitacora ColegiadoBitacora { get; set; }

    }
}