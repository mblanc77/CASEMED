using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("SubsidioItemCod_Afiliado", Schema = "dbo")]
    public partial class SubsidioitemcodAfiliado
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
        public int SubItmCodAfiId { get; set; }

        [ConcurrencyCheck]
        public short? CodSubsidioItemCod { get; set; }

        [ConcurrencyCheck]
        public int? CI { get; set; }

        [ConcurrencyCheck]
        public double? Valor { get; set; }

        [ConcurrencyCheck]
        public DateTime? Vigencia { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

    }
}