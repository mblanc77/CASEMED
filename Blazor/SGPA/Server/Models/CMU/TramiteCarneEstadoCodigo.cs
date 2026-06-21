using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("TramiteCarneEstadoCodigo", Schema = "dbo")]
    public partial class TramiteCarneEstadoCodigo
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
        public string Descripcion { get; set; }

        [ConcurrencyCheck]
        public string Template { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        public ICollection<TramiteCarneEstado> TramiteCarneEstados { get; set; }

        public ICollection<TramiteCarneEstadoWorkFlow> TramiteCarneEstadoWorkFlows { get; set; }

        public ICollection<TramiteCarneEstadoWorkFlow> TramiteCarneEstadoWorkFlows1 { get; set; }

    }
}