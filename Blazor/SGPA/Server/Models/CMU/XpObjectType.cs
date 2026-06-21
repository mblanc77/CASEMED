using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGPA.Server.Models.CMU
{
    [Table("XPObjectType", Schema = "dbo")]
    public partial class XpObjectType
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
        public int OID { get; set; }

        [ConcurrencyCheck]
        public string TypeName { get; set; }

        [ConcurrencyCheck]
        public string AssemblyName { get; set; }

        public ICollection<Cobro> Cobros { get; set; }

        public ICollection<CobroNomina> CobroNominas { get; set; }

        public ICollection<ColegiadoBitacora> ColegiadoBitacoras { get; set; }

        public ICollection<ColegiadoDeclaracionJuradum> ColegiadoDeclaracionJurada { get; set; }

        public ICollection<Convenio> Convenios { get; set; }

        public ICollection<MovimientoCuentum> MovimientoCuenta { get; set; }

        public ICollection<OrigenMovimiento> OrigenMovimientos { get; set; }

        public ICollection<SecuritySystemRole> SecuritySystemRoles { get; set; }

        public ICollection<SecuritySystemTypePermissionsObject> SecuritySystemTypePermissionsObjects { get; set; }

        public ICollection<SecuritySystemUser> SecuritySystemUsers { get; set; }

        public ICollection<TramiteInfoadjuntabase> TramiteInfoadjuntabases { get; set; }

        public ICollection<XpObjectModified> XpObjectModifieds { get; set; }

        public ICollection<XpWeakReference> XpWeakReferences { get; set; }

        public ICollection<XpWeakReference> XpWeakReferences1 { get; set; }

    }
}