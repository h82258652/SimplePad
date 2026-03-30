using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Themes.Settings;
using SimplePad.Themes.UWP.Settings;

namespace SimplePad.Themes.UWP;

public sealed class SimplePadThemesUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadThemesModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IThemeSettings, UWPThemeSettings>();
    }
}
