using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("SubsidioItem", Schema = "dbo")]
    public partial class SubsidioItem
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
        public int IdSubsidio { get; set; }

        [Required]
        [ConcurrencyCheck]
        public short CodSubsidioItemCod { get; set; }

        [ConcurrencyCheck]
        public double? Importe { get; set; }

        [ConcurrencyCheck]
        public bool? AbiEmp { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubsidioItemId { get; set; }

        public SubsidioItemCod SubsidioItemCod { get; set; }

        public SubsidioCabezal SubsidioCabezal { get; set; }

    }
}