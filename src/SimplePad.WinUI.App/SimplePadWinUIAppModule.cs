using SimplePad.Core.Modularity;
using SimplePad.File;
using SimplePad.Windowing;

namespace SimplePad.App;

public sealed class SimplePadWinUIAppModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadWindowingWinUIModule,
        SimplePadFileWinUIModule>();
}
