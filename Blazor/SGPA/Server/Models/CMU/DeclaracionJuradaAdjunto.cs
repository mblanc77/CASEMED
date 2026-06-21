using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("DeclaracionJuradaAdjunto", Schema = "dbo")]
    public partial class DeclaracionJuradaAdjunto
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
        public Guid? FileData { get; set; }

        [ConcurrencyCheck]
        public string Comentario { get; set; }

        [ConcurrencyCheck]
        public int? Declaracion { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public string Link { get; set; }

        [ConcurrencyCheck]
        public string NombreArchivo { get; set; }

        public ColegiadoDeclaracionJuradum ColegiadoDeclaracionJuradum { get; set; }

        public FileDatum FileDatum { get; set; }

    }
}