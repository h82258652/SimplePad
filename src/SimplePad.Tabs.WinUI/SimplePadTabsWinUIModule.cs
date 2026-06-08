using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Menu;
using SimplePad.Settings;
using SimplePad.StatusBar;

namespace SimplePad.Tabs;

public sealed class SimplePadTabsWinUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadTabsModule,
        SimplePadMenuWinUIModule,
        SimplePadSettingsWinUIModule,
        SimplePadEditorWinUIModule,
        SimplePadStatusBarWinUIModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ITabsSettings, WinUITabsSettings>();
        context.Services.AddTransient<IConfirmCloseService, WinUIConfirmCloseService>();
    }
}
