using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.StatusBar;

namespace SimplePad.Menu;

public sealed class SimplePadMenuUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadMenuModule,
        SimplePadEditorModule,
        SimplePadStatusBarModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }
}
