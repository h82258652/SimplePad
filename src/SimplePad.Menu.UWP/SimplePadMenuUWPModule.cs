using SimplePad.Core.Modularity;

namespace SimplePad.Menu;

public sealed class SimplePadMenuUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadMenuModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }
}
