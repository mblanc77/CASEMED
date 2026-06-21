using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("MaeFun", Schema = "dbo")]
    public partial class MaeFun
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
        public int NroFun { get; set; }

        [ConcurrencyCheck]
        public int? NroCuenta { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string Apellido1 { get; set; }

        [ConcurrencyCheck]
        public string Apellido2 { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string Nombre1 { get; set; }

        [ConcurrencyCheck]
        public string Nombre2 { get; set; }

        [ConcurrencyCheck]
        public int? Cedula { get; set; }

        [ConcurrencyCheck]
        public short? Nacionalidad { get; set; }

        [ConcurrencyCheck]
        public DateTime? FecNac { get; set; }

        [ConcurrencyCheck]
        public short? EstCivil { get; set; }

        [ConcurrencyCheck]
        public string DirCalle { get; set; }

        [ConcurrencyCheck]
        public string DirPuerta { get; set; }

        [ConcurrencyCheck]
        public string DirBis { get; set; }

        [ConcurrencyCheck]
        public string DirPiso { get; set; }

        [ConcurrencyCheck]
        public string DirApto { get; set; }

        [ConcurrencyCheck]
        public string DirBloque { get; set; }

        [ConcurrencyCheck]
        public string DirLocal { get; set; }

        [ConcurrencyCheck]
        public int? CodCargo { get; set; }

        [ConcurrencyCheck]
        public string DesCargo { get; set; }

        [ConcurrencyCheck]
        public string Telefono { get; set; }

        [ConcurrencyCheck]
        public DateTime? FecIngreso { get; set; }

        [ConcurrencyCheck]
        public bool? InfDbla { get; set; }

        [ConcurrencyCheck]
        public DateTime? FecInfDbla { get; set; }

        [ConcurrencyCheck]
        public short? AsigCuenta { get; set; }

        [ConcurrencyCheck]
        public DateTime? FecAsigCta { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

    }
}