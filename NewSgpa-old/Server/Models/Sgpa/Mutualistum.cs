using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Mutualista", Schema = "dbo")]
    public partial class Mutualistum
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
        public short CodMutualista { get; set; }

        [ConcurrencyCheck]
        public string Nombre { get; set; }

        [ConcurrencyCheck]
        public string Direccion { get; set; }

        [ConcurrencyCheck]
        public string Telefono { get; set; }

        [ConcurrencyCheck]
        public string Fax { get; set; }

        [ConcurrencyCheck]
        public string EMail { get; set; }

        [ConcurrencyCheck]
        public byte? DiaPago { get; set; }

        [ConcurrencyCheck]
        public short? CodFormaPago { get; set; }

        [ConcurrencyCheck]
        public string PersonaContacto { get; set; }

        [ConcurrencyCheck]
        public double? Cuota { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [ConcurrencyCheck]
        public bool? Ficticia { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public ICollection<Afiliado> Afiliados { get; set; }

        public FormaPago FormaPago { get; set; }

        public ICollection<ReintegroMutual> ReintegroMutuals { get; set; }

    }
}