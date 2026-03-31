using SimplePad.Core.Modularity;

namespace SimplePad.StatusBar.UWP;

public sealed class SimplePadStatusBarUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadStatusBarModule>();
}
