using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Modularity;
using SimplePad.File;
using SimplePad.Search;
using SimplePad.Settings;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingAvaloniaModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadWindowingModule,
        SimplePadSearchAvaloniaModule,
        SimplePadTabsAvaloniaModule,
        SimplePadFileAvaloniaModule,
        SimplePadSettingsAvaloniaModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IAppWindowManager, AvaloniaAppWindowManager>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        base.OnApplicationInitialization(context);

        ServiceLocator.SetScopeIdProvider(new AvaloniaServiceScopeIdProvider());
    }
}
