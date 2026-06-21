using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("MovimientoCuenta", Schema = "dbo")]
    public partial class MovimientoCuentum
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
        public int Id { get; set; }

        [ConcurrencyCheck]
        public DateTime? Fecha { get; set; }

        [ConcurrencyCheck]
        public int? Estado { get; set; }

        [ConcurrencyCheck]
        public int? Colegiado { get; set; }

        [ConcurrencyCheck]
        public int? Origen { get; set; }

        [ConcurrencyCheck]
        public string Referencia { get; set; }

        [ConcurrencyCheck]
        public decimal? Importe { get; set; }

        [ConcurrencyCheck]
        public int? MovimientoTipo { get; set; }

        [ConcurrencyCheck]
        public int? Año { get; set; }

        [ConcurrencyCheck]
        public int? Mes { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? ObjectType { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        [ConcurrencyCheck]
        public int? Dummy { get; set; }

        [ConcurrencyCheck]
        public bool? ConvenioProcesado { get; set; }

        [ConcurrencyCheck]
        public int? MovimientoReferencia { get; set; }

        [ConcurrencyCheck]
        public int? Ajuste { get; set; }

        [ConcurrencyCheck]
        public int? MesCompleto { get; set; }

        public ICollection<CobroNomina> CobroNominas { get; set; }

        public AjusteRetroactivo AjusteRetroactivo { get; set; }

        public Colegiado Colegiado1 { get; set; }

        public MovimientoCuentum MovimientoCuentum1 { get; set; }

        public ICollection<MovimientoCuentum> MovimientoCuenta1 { get; set; }

        public MovimientoTipo MovimientoTipo1 { get; set; }

        public XpObjectType XpobjectType { get; set; }

        public OrigenMovimiento OrigenMovimiento { get; set; }

        public ICollection<MovimientoCuentaCuotum> MovimientoCuentaCuota { get; set; }

        public ICollection<MovimientoCuentaManual> MovimientoCuentaManuals { get; set; }

    }
}