using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Parametro", Schema = "dbo")]
    public partial class Parametro
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
        public int? MovimientoTipoCuota { get; set; }

        [ConcurrencyCheck]
        public int? MovimientoTipoDeposito { get; set; }

        [Column("250")]
        [ConcurrencyCheck]
        public string __250 { get; set; }

        [ConcurrencyCheck]
        public string ServidorNewsLetters { get; set; }

        [ConcurrencyCheck]
        public string CuentaNewsLetters { get; set; }

        [ConcurrencyCheck]
        public string ContraseñaCuentaNewsLetters { get; set; }

        [ConcurrencyCheck]
        public int? CuentaNewsLettersTipo { get; set; }

        [ConcurrencyCheck]
        public int? CategoriaColegiadoDefecto { get; set; }

        [ConcurrencyCheck]
        public int? AgenteVisaNET { get; set; }

        [ConcurrencyCheck]
        public int? MovimientoTipoDebito { get; set; }

        [ConcurrencyCheck]
        public int? MovimientoTipoCobroManual { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public string CriteriaRedPagos05 { get; set; }

        [ConcurrencyCheck]
        public decimal? SaldoRedPagos05 { get; set; }

        [ConcurrencyCheck]
        public string CriteriaColegiado05 { get; set; }

        [ConcurrencyCheck]
        public int? DJ05 { get; set; }

        [ConcurrencyCheck]
        public string PlantillaEMailCuentaCorriente { get; set; }

        [ConcurrencyCheck]
        public string TituloEMailCuentaCorriente { get; set; }

        [ConcurrencyCheck]
        public int? NroLoteAbitab { get; set; }

        [ConcurrencyCheck]
        public int? AgenteIMM { get; set; }

        [ConcurrencyCheck]
        public string FiltroEnvio { get; set; }

        [ConcurrencyCheck]
        public decimal? SMN { get; set; }

        [ConcurrencyCheck]
        public decimal? MontoSMN { get; set; }

        [ConcurrencyCheck]
        public int? CategoriaColegiadoMora { get; set; }

        [ConcurrencyCheck]
        public int? MesesPaternidad { get; set; }

        [ConcurrencyCheck]
        public int? DeclaracionJuradaTipoPaternidad { get; set; }

        [ConcurrencyCheck]
        public string CodEntidadMultiBROU { get; set; }

        [ConcurrencyCheck]
        public string UrlServicioMail { get; set; }

        [ConcurrencyCheck]
        public string MensajeRegistroFinalizado { get; set; }

        [ConcurrencyCheck]
        public string MensajeRegistroRechazado { get; set; }

        [ConcurrencyCheck]
        public string MailBodyRegistroFinalizado { get; set; }

        [ConcurrencyCheck]
        public string MailBodyRegistroRechazado { get; set; }

        [ConcurrencyCheck]
        public string MailBodyRegistroObservado { get; set; }

        [ConcurrencyCheck]
        public string UrlConfirmacionRegistro { get; set; }

        [ConcurrencyCheck]
        public string DestinatarioAvisosRegistros { get; set; }

        [ConcurrencyCheck]
        public string MailBodyRegistroDeudaADM { get; set; }

        [ConcurrencyCheck]
        public string MailBodyRegistroDJI { get; set; }

        [ConcurrencyCheck]
        public string MailBodyConfirmacionDJI { get; set; }

        [ConcurrencyCheck]
        public string UsuarioDNN { get; set; }

        [ConcurrencyCheck]
        public string PassWordDNN { get; set; }

        [ConcurrencyCheck]
        public string UrlDNN { get; set; }

        [ConcurrencyCheck]
        public int? PortalDNN { get; set; }

        [ConcurrencyCheck]
        public string MailBoodyRegistroAsistenciaSala { get; set; }

        [ConcurrencyCheck]
        public string MailBoodyIngresoReservaSala { get; set; }

        [ConcurrencyCheck]
        public string MailBoodyAlertaRegistroPendiente { get; set; }

        [ConcurrencyCheck]
        public string RecurrenciaAlertaRegistroPendiente { get; set; }

        [ConcurrencyCheck]
        public int? ExpiringPasswordHours { get; set; }

        [ConcurrencyCheck]
        public string MailBodyResetPasswordRequest { get; set; }

        [ConcurrencyCheck]
        public int? MesesPlanillaNomina { get; set; }

        public AgenteCobranza AgenteCobranza { get; set; }

        public AgenteCobranzaDebito AgenteCobranzaDebito { get; set; }

        public CategoriaColegiado CategoriaColegiado { get; set; }

        public CategoriaColegiado CategoriaColegiado1 { get; set; }

        public DeclaracionJuradaTipo DeclaracionJuradaTipo { get; set; }

        public DeclaracionJuradaTipo DeclaracionJuradaTipo1 { get; set; }

        public MovimientoTipo MovimientoTipo { get; set; }

        public MovimientoTipo MovimientoTipo1 { get; set; }

        public MovimientoTipo MovimientoTipo2 { get; set; }

        public MovimientoTipo MovimientoTipo3 { get; set; }

    }
}