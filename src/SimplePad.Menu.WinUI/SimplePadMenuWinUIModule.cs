using SimplePad.Core.Modularity;
using SimplePad.File;
using SimplePad.Settings;
using SimplePad.StatusBar;

namespace SimplePad.Menu;

public sealed class SimplePadMenuWinUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadMenuModule,
        SimplePadSettingsModule,
        SimplePadStatusBarModule,
        SimplePadFileModule>();
}
