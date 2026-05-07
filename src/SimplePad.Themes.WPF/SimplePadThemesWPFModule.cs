using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Themes;

public sealed class SimplePadThemesWPFModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadThemesModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IThemeSettings, WPFThemeSettings>();
    }
}
