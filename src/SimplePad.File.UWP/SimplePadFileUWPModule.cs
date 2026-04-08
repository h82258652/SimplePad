using SimplePad.Core.Modularity;

namespace SimplePad.File;

public sealed class SimplePadFileUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFileModule>();
}
