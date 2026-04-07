using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.StatusBar;

namespace SimplePad.Tabs;

public sealed class SimplePadTabsUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadTabsModule,
        SimplePadEditorUWPModule,
        SimplePadStatusBarUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ITabsSettings, UWPTabsSettings>();
    }
}
