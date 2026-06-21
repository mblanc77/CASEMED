using DevExpress.ExpressApp.Validation;
using DevExpress.Persistent.Validation;
using NewSgpa.Module.BusinessObjects.Sgpa;

namespace NewSgpa.Module.Validation;

/// <summary>
/// Business validation rules for core SGPA entities.
/// Replaces VB6 DatosOk() validation functions.
/// </summary>

// === Afiliado Rules ===
[CodeRule]
public class AfiliadoCIRequired : RuleBase<Afiliado>
{
    public AfiliadoCIRequired() : base("AfiliadoCIRequired", "Save")
    {
    }

    protected override bool IsValidInternal(Afiliado target, out string errorMessageTemplate)
    {
        errorMessageTemplate = "La Cédula de Identidad es obligatoria.";
        return target.CI > 0;
    }
}

[CodeRule]
public class AfiliadoNombreRequired : RuleBase<Afiliado>
{
    public AfiliadoNombreRequired() : base("AfiliadoNombreRequired", "Save")
    {
    }

    protected override bool IsValidInternal(Afiliado target, out string errorMessageTemplate)
    {
        errorMessageTemplate = "El nombre y al menos un apellido son obligatorios.";
        return !string.IsNullOrWhiteSpace(target.Nombres)
               && !string.IsNullOrWhiteSpace(target.Apellido1);
    }
}

// === Certificacion Rules ===
[CodeRule]
public class CertificacionFechasRequired : RuleBase<Certificacion>
{
    public CertificacionFechasRequired() : base("CertificacionFechasRequired", "Save")
    {
    }

    protected override bool IsValidInternal(Certificacion target, out string errorMessageTemplate)
    {
        errorMessageTemplate = "Las fechas de inicio y fin de la certificación son obligatorias.";
        return target.FechaIni.HasValue && target.FechaFin.HasValue;
    }
}

[CodeRule]
public class CertificacionFechaFinPosterior : RuleBase<Certificacion>
{
    public CertificacionFechaFinPosterior() : base("CertificacionFechaFinPosterior", "Save")
    {
    }

    protected override bool IsValidInternal(Certificacion target, out string errorMessageTemplate)
    {
        errorMessageTemplate = "La fecha de fin debe ser posterior o igual a la fecha de inicio.";
        if (!target.FechaIni.HasValue || !target.FechaFin.HasValue) return true;
        return target.FechaFin.Value >= target.FechaIni.Value;
    }
}

[CodeRule]
public class CertificacionCIRequired : RuleBase<Certificacion>
{
    public CertificacionCIRequired() : base("CertificacionCIRequired", "Save")
    {
    }

    protected override bool IsValidInternal(Certificacion target, out string errorMessageTemplate)
    {
        errorMessageTemplate = "La Cédula del afiliado es obligatoria.";
        return target.CI.HasValue && target.CI.Value > 0;
    }
}
