using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.StatusBar;

public sealed class SimplePadStatusBarWinUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadStatusBarModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IStatusBarSettings, WinUIStatusBarSettings>();
    }
}
