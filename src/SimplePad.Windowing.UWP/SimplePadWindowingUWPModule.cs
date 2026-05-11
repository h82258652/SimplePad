using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Modularity;
using SimplePad.File;
using SimplePad.Search;
using SimplePad.Settings;
using SimplePad.Tabs;
using SimplePad.Themes;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadWindowingModule,
        SimplePadThemesUWPModule,
        SimplePadSearchUWPModule,
        SimplePadTabsUWPModule,
        SimplePadFileUWPModule,
        SimplePadSettingsUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IAppWindowManager, UWPAppWindowManager>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        base.OnApplicationInitialization(context);

        ServiceLocator.SetScopeIdProvider(new UWPServiceScopeIdProvider());
    }
}
