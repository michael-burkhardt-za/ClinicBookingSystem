using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using MudBlazor;



namespace Client.Shared
{
    

    public class AppErrorBoundary : ErrorBoundary
    {
        [Inject] ILogger<AppErrorBoundary> Logger { get; set; } = default!;
        [Inject] ISnackbar Snackbar { get; set; } = default!;

        protected override Task OnErrorAsync(Exception exception)
        {
            Logger.LogError(exception, "Unhandled UI exception");

            Snackbar.Add(
                "An unexpected error occurred.",
                Severity.Error,
                config =>
                {
                    config.ShowCloseIcon = true;
                    config.VisibleStateDuration = 5000;
                });

            return Task.CompletedTask;
        }
    }

}
