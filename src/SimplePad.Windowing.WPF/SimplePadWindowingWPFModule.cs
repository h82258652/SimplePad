using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Windowing;

public sealed class SimplePadWindowingWPFModule : AppModuleBase
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IAppWindowManager, WPFAppWindowManager>();
    }
}
