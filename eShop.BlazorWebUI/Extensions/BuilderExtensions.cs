using Blazored.Toast;
using eShop.Application.Extensions;
using MudBlazor.Services;
using MudExtensions.Services;

namespace eShop.BlazorWebUI.Extensions;

public static class BuilderExtensions
{
    public static void AddAppServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Configuration:Logging"));

        builder.Services.AddCascadingAuthenticationState();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.AddValidation();
        builder.AddInfrastructureLayer();
        builder.Services.AddMudExtensions();

        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 10000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });
        builder.Services.AddBlazoredToast();
    }
}