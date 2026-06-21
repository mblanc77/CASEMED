using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SgpaNew.Server.Models.Sgpa
{
    [Table("Afiliado", Schema = "dbo")]
    public partial class Afiliado
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
        public int CI { get; set; }

        [ConcurrencyCheck]
        public string Nombres { get; set; }

        [ConcurrencyCheck]
        public string Apellido1 { get; set; }

        [ConcurrencyCheck]
        public string Apellido2 { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaNacimiento { get; set; }

        [ConcurrencyCheck]
        public string Sexo { get; set; }

        [ConcurrencyCheck]
        public string Direccion { get; set; }

        [ConcurrencyCheck]
        public string Telefono { get; set; }

        [ConcurrencyCheck]
        public string EMail { get; set; }

        [ConcurrencyCheck]
        public short? CodMutualista { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaIngMutualista { get; set; }

        [ConcurrencyCheck]
        public DateTime? FechaBajaMutualista { get; set; }

        [ConcurrencyCheck]
        public string NroSocioMutualista { get; set; }

        [ConcurrencyCheck]
        public byte? CodRegimenJubilatorio { get; set; }

        [ConcurrencyCheck]
        public string CodDepartamento { get; set; }

        [ConcurrencyCheck]
        public bool? PagaMutualista { get; set; }

        [ConcurrencyCheck]
        public string CodSituacionMutual { get; set; }

        [ConcurrencyCheck]
        public int? CodBanco { get; set; }

        [ConcurrencyCheck]
        public string NroCuenta { get; set; }

        [ConcurrencyCheck]
        public string NroFunCuenta { get; set; }

        [ConcurrencyCheck]
        public string Movil { get; set; }

        [ConcurrencyCheck]
        public string Usr { get; set; }

        [ConcurrencyCheck]
        public DateTime? Ts { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public ICollection<AdPreJub> AdPreJubs { get; set; }

        public Banco Banco { get; set; }

        public Mutualistum Mutualistum { get; set; }

        public RegimenJubilatorio RegimenJubilatorio { get; set; }

        public ICollection<AfiliadoApunte> AfiliadoApuntes { get; set; }

        public ICollection<AfiliadoEspecialidad> AfiliadoEspecialidads { get; set; }

        public ICollection<Certificacion> Certificacions { get; set; }

        public ICollection<CertificacionProrroga> CertificacionProrrogas { get; set; }

        public ICollection<Prestacion> Prestacions { get; set; }

        public ICollection<PrimaFallecimiento> PrimaFallecimientos { get; set; }

        public ICollection<ReintegroMutual> ReintegroMutuals { get; set; }

        public ICollection<SubsidioCabezal> SubsidioCabezals { get; set; }

        public ICollection<Trabaja> Trabajas { get; set; }

    }
}