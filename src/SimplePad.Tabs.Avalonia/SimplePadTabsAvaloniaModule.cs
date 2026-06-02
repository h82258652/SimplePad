using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Tabs;

public sealed class SimplePadTabsAvaloniaModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadTabsModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ITabsSettings, AvaloniaTabsSettings>();
        context.Services.AddTransient<IConfirmCloseService, AvaloniaConfirmCloseService>();
    }
}
