using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Menu;
using SimplePad.Settings;
using SimplePad.StatusBar;

namespace SimplePad.Tabs;

public sealed class SimplePadTabsUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadTabsModule,
        SimplePadMenuUWPModule,
        SimplePadSettingsUWPModule,
        SimplePadEditorUWPModule,
        SimplePadStatusBarUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ITabsSettings, UWPTabsSettings>();
        context.Services.AddTransient<IConfirmCloseService, UWPConfirmCloseService>();
    }
}
