using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Convenio", Schema = "dbo")]
    public partial class Convenio
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
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaAnulado { get; set; }

        [ConcurrencyCheck]
        public int? Tipo { get; set; }

        [ConcurrencyCheck]
        public decimal? NroFormulario { get; set; }

        [ConcurrencyCheck]
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaCancelacion { get; set; }

        [ConcurrencyCheck]
        public string MotivoAnulado { get; set; }

        public ICollection<CobroNomina> CobroNominas { get; set; }

        public Colegiado Colegiado1 { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public ICollection<ConvenioFinanciacion> ConvenioFinanciacions { get; set; }

    }
}