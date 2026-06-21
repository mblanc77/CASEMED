using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("DebitoAdjunto", Schema = "dbo")]
    public partial class DebitoAdjunto
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
        public int? Debito { get; set; }

        [ConcurrencyCheck]
        public Guid? FileData { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public bool? EsResultado { get; set; }

        [ConcurrencyCheck]
        public bool? EsCambioTarjeta { get; set; }

        [ConcurrencyCheck]
        public bool? EsMontosEspeciales { get; set; }

        [ConcurrencyCheck]
        public bool? EsNominalEspecial { get; set; }

        public Debito Debito1 { get; set; }

        public FileDatum FileDatum { get; set; }

    }
}