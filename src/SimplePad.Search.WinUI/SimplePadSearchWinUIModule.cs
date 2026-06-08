using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Themes;

namespace SimplePad.Search;

public sealed class SimplePadSearchWinUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadSearchModule,
        SimplePadThemesWinUIModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ISearchSettings, WinUISearchSettings>();

        context.Services.AddScoped<WinUISearchNotificationService>();
        context.Services.AddScoped<ISearchNotificationService, WinUISearchNotificationService>(serviceProvider => serviceProvider.GetRequiredService<WinUISearchNotificationService>());

        context.Services.AddScoped<ISearchDialogService, WinUISearchDialogService>();
    }
}
