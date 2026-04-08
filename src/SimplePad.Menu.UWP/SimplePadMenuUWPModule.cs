using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.File;
using SimplePad.Settings;
using SimplePad.StatusBar;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public sealed class SimplePadMenuUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadMenuModule,
        SimplePadWindowingModule,
        SimplePadFileUWPModule,
        SimplePadEditorModule,
        SimplePadSettingsModule,
        SimplePadStatusBarModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }
}
