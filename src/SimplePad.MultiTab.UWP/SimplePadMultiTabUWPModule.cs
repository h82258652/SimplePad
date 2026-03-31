using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.MultiTab.Settings;
using SimplePad.MultiTab.UWP.Settings;

namespace SimplePad.MultiTab.UWP;

public sealed class SimplePadMultiTabUWPModule : AppModuleBase 
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IMultiTabSettings, UWPMultiTabSettings>();
    }
}
