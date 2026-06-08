using SimplePad.Core.Modularity;
using SimplePad.File;
using SimplePad.Settings;

namespace SimplePad.Menu;

public sealed class SimplePadMenuAvaloniaModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadMenuModule,
        SimplePadSettingsModule,
        SimplePadStatusBarModule,
        SimplePadFileModule>();
}
