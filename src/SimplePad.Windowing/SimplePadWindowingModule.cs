using SimplePad.Core.Modularity;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadTabsModule>();
}
