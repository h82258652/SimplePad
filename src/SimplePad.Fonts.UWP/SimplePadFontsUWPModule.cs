using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Fonts.Settings;
using SimplePad.Fonts.UWP.Settings;

namespace SimplePad.Fonts.UWP;

public sealed class SimplePadFontsUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFontsModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IFontSettings, UWPFontSettings>();
    }
}
