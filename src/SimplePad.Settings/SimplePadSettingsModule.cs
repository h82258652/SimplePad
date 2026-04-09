using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Settings;

public sealed class SimplePadSettingsModule : AppModuleBase 
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddScoped(_ => new SettingsState());
    }
}
