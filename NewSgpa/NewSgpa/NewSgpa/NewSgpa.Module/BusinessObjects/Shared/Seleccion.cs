using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NewSgpa.Module.BusinessObjects.Base;

namespace NewSgpa.Module.BusinessObjects.Shared;

[Table("Seleccion")]
public class Seleccion : BaseEntity
{
    public virtual int? IdSeleccion { get; set; }

    [StringLength(255)]
    public virtual string? Form { get; set; }

    [StringLength(255)]
    public virtual string? Nombre { get; set; }

    [Column(TypeName = "ntext")]
    public virtual string? Txt { get; set; }

    public virtual bool System { get; set; }
}
