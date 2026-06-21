using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("AfiliadoEspecialidad", Schema = "dbo")]
    public partial class AfiliadoEspecialidad
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [Required]
        [ConcurrencyCheck]
        public int CI { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int CodEspecialidad { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AfiliadoEspecialidadId { get; set; }

        public Afiliado Afiliado { get; set; }

        public Especialidad Especialidad { get; set; }

    }
}