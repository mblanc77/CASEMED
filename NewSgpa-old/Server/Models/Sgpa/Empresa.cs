using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Empresa", Schema = "dbo")]
    public partial class Empresa
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
        public short CodEmpresa { get; set; }

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
        public float? AporteCasemed { get; set; }

        [ConcurrencyCheck]
        public int? AporteAguinaldo { get; set; }

        [ConcurrencyCheck]
        public string PersonaContacto { get; set; }

        [ConcurrencyCheck]
        public string Autoridades { get; set; }

        [ConcurrencyCheck]
        public short? CodRegimenAporte { get; set; }

        [ConcurrencyCheck]
        public short? CodSituacionPago { get; set; }

        [ConcurrencyCheck]
        public bool? Liquidar { get; set; }

        [ConcurrencyCheck]
        public bool? Ficticia { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public RegimenAporte RegimenAporte { get; set; }

        public SituacionPago SituacionPago { get; set; }

        public ICollection<EmpresaPago> EmpresaPagos { get; set; }

        public ICollection<SubsidioCabezalEmpresa> SubsidioCabezalEmpresas { get; set; }

        public ICollection<SubsidioItemEmpresa> SubsidioItemEmpresas { get; set; }

        public ICollection<Trabaja> Trabajas { get; set; }

    }
}