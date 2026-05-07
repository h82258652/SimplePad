using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Themes;

namespace SimplePad.Search;

public sealed class SimplePadSearchUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadSearchModule,
        SimplePadThemesUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ISearchSettings, UWPSearchSettings>();

        context.Services.AddScoped<UWPSearchNotificationService>();
        context.Services.AddScoped<ISearchNotificationService, UWPSearchNotificationService>(serviceProvider => serviceProvider.GetRequiredService<UWPSearchNotificationService>());

        context.Services.AddScoped<ISearchDialogService, UWPSearchDialogService>();
    }
}