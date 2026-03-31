using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Settings.UWP;

public sealed class SimplePadSettingsUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadSettingsModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }
}
