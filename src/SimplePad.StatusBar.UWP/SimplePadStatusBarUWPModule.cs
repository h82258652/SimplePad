using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.StatusBar.Settings;
using SimplePad.StatusBar.UWP.Settings;

namespace SimplePad.StatusBar.UWP;

public sealed class SimplePadStatusBarUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadStatusBarModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IStatusBarSettings, UWPStatusBarSettings>();
    }
}
