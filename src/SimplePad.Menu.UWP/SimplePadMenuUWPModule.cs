using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Settings;
using SimplePad.StatusBar;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public sealed class SimplePadMenuUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadMenuModule,
        SimplePadWindowingModule,
        SimplePadEditorModule,
        SimplePadSettingsModule,
        SimplePadStatusBarModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }
}
