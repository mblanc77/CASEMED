using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Tramite_InfoAdjuntaFotoCarne", Schema = "dbo")]
    public partial class TramiteInfoadjuntafotocarne
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
        public int OID { get; set; }

        [ConcurrencyCheck]
        public string IdentificadorFoto { get; set; }

        public TramiteInfoadjuntabase TramiteInfoAdjuntaBase { get; set; }

    }
}