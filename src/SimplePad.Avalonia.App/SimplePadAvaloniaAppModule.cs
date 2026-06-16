using SimplePad.Core.Modularity;
using SimplePad.File;
using SimplePad.Windowing;

namespace SimplePad.App;

public sealed class SimplePadAvaloniaAppModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadWindowingAvaloniaModule,
        SimplePadFileAvaloniaModule>();
}
