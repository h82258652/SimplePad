using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.File;

public sealed class SimplePadFileWinUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFileModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddTransient<IFilePickerService, WinUIFilePickerService>();
    }
}
