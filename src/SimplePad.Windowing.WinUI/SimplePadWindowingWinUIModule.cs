using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Modularity;
using SimplePad.File;
using SimplePad.Search;
using SimplePad.Settings;
using SimplePad.Tabs;
using SimplePad.Themes;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingWinUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadWindowingModule,
        SimplePadThemesWinUIModule,
        SimplePadSearchWinUIModule,
        SimplePadTabsWinUIModule,
        SimplePadFileWinUIModule,
        SimplePadSettingsWinUIModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IAppWindowManager, WinUIAppWindowManager>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        base.OnApplicationInitialization(context);

        ServiceLocator.SetScopeIdProvider(new WinUIServiceScopeIdProvider());
    }
}
