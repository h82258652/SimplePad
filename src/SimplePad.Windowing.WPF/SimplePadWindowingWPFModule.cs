using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Modularity;
using SimplePad.Search;
using SimplePad.Settings;
using SimplePad.Tabs;
using SimplePad.Themes;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingWPFModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadWindowingModule,
        SimplePadThemesWPFModule,
        SimplePadSearchWPFModule,
        SimplePadTabsWPFModule,
        SimplePadSettingsModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IAppWindowManager, WPFAppWindowManager>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        base.OnApplicationInitialization(context);

        ServiceLocator.SetScopeIdProvider(new WPFServiceScopeIdProvider(context.ServiceProvider.GetRequiredService<IAppWindowManager>()));
    }
}
