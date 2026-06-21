using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sgpa.Domain.Metadata;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Validador de Blazor dirigido por metadata (análogo a <see cref="DataAnnotationsValidator"/>).
/// Se coloca dentro del EditForm del DetailView; al validar el EditContext (lo que el grid hace
/// antes de guardar) puebla el <see cref="ValidationMessageStore"/> con los errores de
/// <see cref="MetadataValidation"/>, bloqueando el guardado y mostrando los mensajes.
/// </summary>
public sealed class SgpaMetadataValidator : ComponentBase
{
    private ValidationMessageStore _messages = default!;
    private EditContext _previous = default!;

    [CascadingParameter] private EditContext CurrentEditContext { get; set; } = default!;

    [Inject] private IServiceProvider Services { get; set; } = default!;

    [Parameter, EditorRequired] public EntityMetadata Meta { get; set; } = default!;

    /// <summary>True si es un alta (habilita validar la PK manual).</summary>
    [Parameter] public bool IsNew { get; set; }

    // Validador de negocio de la entidad (FluentValidation), si está registrado. Sus reglas SÍNCRONAS se
    // muestran por campo en vivo; las async (consultas a base) las corre el gate de guardado del CRUD.
    private IValidator? _fluent;

    protected override void OnInitialized()
    {
        if (CurrentEditContext is null)
            throw new InvalidOperationException(
                $"{nameof(SgpaMetadataValidator)} requiere un EditContext en cascada (usar dentro de un EditForm).");

        _fluent = Services.GetService(typeof(IValidator<>).MakeGenericType(Meta.EntityType)) as IValidator;
        _messages = new ValidationMessageStore(CurrentEditContext);
        CurrentEditContext.OnValidationRequested += OnValidationRequested;
        CurrentEditContext.OnFieldChanged += OnFieldChanged;
        _previous = CurrentEditContext;
    }

    protected override void OnParametersSet()
    {
        if (CurrentEditContext != _previous)
        {
            _previous.OnValidationRequested -= OnValidationRequested;
            _previous.OnFieldChanged -= OnFieldChanged;
            _messages = new ValidationMessageStore(CurrentEditContext);
            CurrentEditContext.OnValidationRequested += OnValidationRequested;
            CurrentEditContext.OnFieldChanged += OnFieldChanged;
            _previous = CurrentEditContext;
        }
    }

    private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
    {
        _messages.Clear();
        foreach (var (field, msg) in CollectErrors())
            _messages.Add(new FieldIdentifier(CurrentEditContext.Model, field), msg);
        CurrentEditContext.NotifyValidationStateChanged();
    }

    // Revalida sólo el campo editado para feedback inmediato (sin re-evaluar todo el modelo).
    private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        _messages.Clear(e.FieldIdentifier);
        foreach (var (field, msg) in CollectErrors())
            if (field == e.FieldIdentifier.FieldName)
                _messages.Add(e.FieldIdentifier, msg);
        CurrentEditContext.NotifyValidationStateChanged();
    }

    // Reúne los errores por campo: validación por metadata (obligatorio/largo) + reglas SÍNCRONAS del
    // validador de negocio (FluentValidation). Las reglas async se corren en el gate de guardado del CRUD.
    private IEnumerable<(string Field, string Message)> CollectErrors()
    {
        var model = CurrentEditContext.Model;
        foreach (var error in MetadataValidation.Validate(Meta, model, IsNew))
            yield return (error.Column.Property.Name, error.Message);

        if (_fluent is null) yield break;
        FluentValidation.Results.ValidationResult? result = null;
        try { result = _fluent.Validate(new ValidationContext<object>(model)); }
        catch (AsyncValidatorInvokedSynchronouslyException) { /* reglas async → gate de guardado */ }
        if (result is not null)
            foreach (var f in result.Errors)
                yield return (f.PropertyName, f.ErrorMessage);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        // No renderiza nada.
    }
}
