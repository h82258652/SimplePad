using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Settings;
using SimplePad.StatusBar;

namespace SimplePad.Menu;

public sealed class SimplePadMenuUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadMenuModule,
        SimplePadSettingsModule,
        SimplePadEditorModule,
        SimplePadStatusBarModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }
}
