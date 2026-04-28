using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.File;

namespace SimplePad.StatusBar;

public sealed class SimplePadStatusBarUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadStatusBarModule,
        SimplePadFileModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IStatusBarSettings, UWPStatusBarSettings>();
    }
}
