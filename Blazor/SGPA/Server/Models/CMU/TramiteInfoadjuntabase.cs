using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Tramite_InfoAdjuntaBase", Schema = "dbo")]
    public partial class TramiteInfoadjuntabase
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
        public int OID { get; set; }

        [ConcurrencyCheck]
        public int? Tramite { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        [ConcurrencyCheck]
        public int? Tipo { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public TramiteCarne TramiteCarne { get; set; }

        public ICollection<TramiteInfoadjuntacedula> TramiteInfoadjuntacedulas { get; set; }

        public ICollection<TramiteInfoadjuntafotocarne> TramiteInfoadjuntafotocarnes { get; set; }

        public ICollection<TramiteInfoadjuntatitulo> TramiteInfoadjuntatitulos { get; set; }

    }
}