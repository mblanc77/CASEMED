using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("Contacto", Schema = "dbo")]
    public partial class Contacto
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
        public string Nombres { get; set; }

        [ConcurrencyCheck]
        public string PrimerApellido { get; set; }

        [ConcurrencyCheck]
        public string SegundoApellido { get; set; }

        [ConcurrencyCheck]
        public string EMail { get; set; }

        [ConcurrencyCheck]
        public string Telefono { get; set; }

        [ConcurrencyCheck]
        public string Celular { get; set; }

        [ConcurrencyCheck]
        public string Denominacion { get; set; }

        [ConcurrencyCheck]
        public string NomComRazSoc { get; set; }

        [ConcurrencyCheck]
        public int? Cargo { get; set; }

        [ConcurrencyCheck]
        public int? Grupo { get; set; }

        [ConcurrencyCheck]
        public string Observaciones { get; set; }

        [ConcurrencyCheck]
        public int? Area { get; set; }

        [ConcurrencyCheck]
        public string Direccion { get; set; }

        [ConcurrencyCheck]
        public int? OptimisticLockField { get; set; }

        [ConcurrencyCheck]
        public int? GCRecord { get; set; }

        public AreaContacto AreaContacto { get; set; }

        public CargoContacto CargoContacto { get; set; }

        public GrupoContacto GrupoContacto { get; set; }

        public ICollection<ContactoInfoAdicional> ContactoInfoAdicionals { get; set; }

    }
}