using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.File;

namespace SimplePad.Tabs;

public sealed class SimplePadTabsModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFileModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddTransient(serviceProvider => new TabManager(serviceProvider.GetRequiredService<IFilePickerService>(), serviceProvider.GetRequiredService<IConfirmCloseService>()));
    }
}
