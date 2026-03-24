using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Modularity;
using SimplePad.Settings.UWP;
using SimplePad.UWP.UI;

namespace SimplePad.UWP.App;

public sealed class SimplePadUWPAppModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadSettingsUWPModule, SimplePadUWPUIModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<AppState>();
    }
}
