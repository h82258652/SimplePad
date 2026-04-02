using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Themes;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadThemesUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IAppWindowManager, UWPAppWindowManager>();
    }
}
