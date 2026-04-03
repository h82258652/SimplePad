using SimplePad.Core.Modularity;

namespace SimplePad.Search;

public sealed class SimplePadSearchUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadSearchModule>();
}