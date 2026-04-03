using SimplePad.Core.Modularity;
using SimplePad.Windowing;

namespace SimplePad.App;

public sealed class SimplePadUWPAppModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadWindowingUWPModule>();
}
