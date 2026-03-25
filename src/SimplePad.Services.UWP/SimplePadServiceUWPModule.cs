using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Services.UWP;

public sealed class SimplePadServiceUWPModule : AppModuleBase
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddTransient<IFileService, UWPFileService>();
    }
}
