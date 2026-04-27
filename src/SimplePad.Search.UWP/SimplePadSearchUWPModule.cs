using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Search;

public sealed class SimplePadSearchUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadSearchModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ISearchSettings, UWPSearchSettings>();
        context.Services.AddScoped<ISearchNotificationService, UWPSearchNotificationService>();
        context.Services.AddScoped<ISearchDialogService, UWPSearchDialogService>();
    }
}