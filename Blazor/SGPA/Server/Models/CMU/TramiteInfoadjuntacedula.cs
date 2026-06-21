using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Tramite_InfoAdjuntaCedula", Schema = "dbo")]
    public partial class TramiteInfoadjuntacedula
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
        public string IdentificadorFrente { get; set; }

        [ConcurrencyCheck]
        public string IdentificadorReverso { get; set; }

        public TramiteInfoadjuntabase TramiteInfoAdjuntaBase { get; set; }

    }
}