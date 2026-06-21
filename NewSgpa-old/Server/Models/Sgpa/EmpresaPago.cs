using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("EmpresaPago", Schema = "dbo")]
    public partial class EmpresaPago
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
        public short CodEmpresa { get; set; }

        [Required]
        [ConcurrencyCheck]
        public short Mes { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int Anio { get; set; }

        [ConcurrencyCheck]
        public int? Importe { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpresaPagoId { get; set; }

        public Empresa Empresa { get; set; }

    }
}