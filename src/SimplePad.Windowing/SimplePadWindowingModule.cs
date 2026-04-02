using SimplePad.Core.Modularity;
using SimplePad.MultiTab;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadMultiTabModule>();
}
