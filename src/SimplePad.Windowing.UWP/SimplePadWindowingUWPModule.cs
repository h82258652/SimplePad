using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Settings;
using SimplePad.Tabs;
using SimplePad.Themes;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadWindowingModule,
        SimplePadThemesUWPModule,
        SimplePadTabsUWPModule,
        SimplePadSettingsUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IAppWindowManager, UWPAppWindowManager>();
    }
}
