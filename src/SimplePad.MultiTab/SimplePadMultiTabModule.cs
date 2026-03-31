using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Menu;

namespace SimplePad.MultiTab;

public sealed class SimplePadMultiTabModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadMenuModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.Configure<MenuBarOptions>(options => 
        {
            // TODO insert New tab and Clsoe tab
        });
    }
}
