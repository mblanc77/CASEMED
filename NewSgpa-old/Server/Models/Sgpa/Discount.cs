using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("DISCOUNT", Schema = "dbo")]
    public partial class Discount
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        public double? CI { get; set; }

        public double? FICHA { get; set; }

        [Column("NOMBRE Y APELLIDO")]
        public string NOMBREYAPELLIDO { get; set; }

        [Column("Nº CUENTA")]
        public double? NºCUENTA { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

    }
}