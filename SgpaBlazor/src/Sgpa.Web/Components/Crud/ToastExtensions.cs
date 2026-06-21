using DevExpress.Blazor;

namespace Sgpa.Web.Components.Crud;

/// <summary>Helpers para mostrar toasts (errores, éxito, avisos) de forma consistente en toda la app.</summary>
public static class ToastExtensions
{
    public static void Error(this IToastNotificationService toasts, string text) =>
        toasts.ShowToast(new ToastOptions
        {
            RenderStyle = ToastRenderStyle.Danger,
            ThemeMode = ToastThemeMode.Pastel,
            Title = "Error",
            Text = text,
        });

    public static void Exito(this IToastNotificationService toasts, string text) =>
        toasts.ShowToast(new ToastOptions
        {
            RenderStyle = ToastRenderStyle.Success,
            ThemeMode = ToastThemeMode.Pastel,
            Title = "Listo",
            Text = text,
        });

    public static void Aviso(this IToastNotificationService toasts, string text) =>
        toasts.ShowToast(new ToastOptions
        {
            RenderStyle = ToastRenderStyle.Warning,
            ThemeMode = ToastThemeMode.Pastel,
            Title = "Atención",
            Text = text,
        });
}
