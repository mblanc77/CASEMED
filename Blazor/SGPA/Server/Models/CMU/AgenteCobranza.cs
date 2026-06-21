using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("AgenteCobranza", Schema = "dbo")]
    public partial class AgenteCobranza
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
        public int Id { get; set; }

        [ConcurrencyCheck]
        public int? CuentaBancaria { get; set; }

        [ConcurrencyCheck]
        public string ParseClass { get; set; }

        [ConcurrencyCheck]
        public int? AgenteTipo { get; set; }

        [ConcurrencyCheck]
        public string NombreContacto { get; set; }

        [ConcurrencyCheck]
        public string TelefonoContacto { get; set; }

        [ConcurrencyCheck]
        public string EMailContacto { get; set; }

        [ConcurrencyCheck]
        public bool? SobrescribeEnColegiadoDesdeDeposito { get; set; }

        [ConcurrencyCheck]
        public int? Departamento { get; set; }

        [ConcurrencyCheck]
        public int? IdExterno { get; set; }

        [ConcurrencyCheck]
        public int? Region { get; set; }

        [ConcurrencyCheck]
        public int? Grupo { get; set; }

        public AgenteCobranzaTipo AgenteCobranzaTipo { get; set; }

        public CuentaBancarium CuentaBancarium { get; set; }

        public Departamento Departamento1 { get; set; }

        public AgenteGrupo AgenteGrupo { get; set; }

        public OrigenMovimiento OrigenMovimiento { get; set; }

        public Region Region1 { get; set; }

        public ICollection<AgenteCobranzaDebito> AgenteCobranzaDebitos { get; set; }

        public ICollection<Cobro> Cobros { get; set; }

        public ICollection<Colegiado> Colegiados { get; set; }

        public ICollection<Colegiado> Colegiados1 { get; set; }

        public ICollection<Parametro> Parametros { get; set; }

        public ICollection<UsuarioInstitucion> UsuarioInstitucions { get; set; }

    }
}