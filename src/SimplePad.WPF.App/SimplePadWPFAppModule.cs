using SimplePad.Core.Modularity;
using SimplePad.File;
using SimplePad.Windowing;

namespace SimplePad.App;

public sealed class SimplePadWPFAppModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadWindowingWPFModule,
        SimplePadFileWPFModule>();
}
