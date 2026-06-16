using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Menu;
using SimplePad.Settings;
using SimplePad.StatusBar;

namespace SimplePad.Tabs;

public sealed class SimplePadTabsAvaloniaModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadTabsModule,
        SimplePadMenuAvaloniaModule,
        SimplePadSettingsAvaloniaModule,
        SimplePadEditorAvaloniaModule,
        SimplePadStatusBarAvaloniaModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ITabsSettings, AvaloniaTabsSettings>();
        context.Services.AddTransient<IConfirmCloseService, AvaloniaConfirmCloseService>();
    }
}
