using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Fonts;

public sealed class SimplePadFontsAvaloniaModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFontsModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IFontSettings, AvaloniaFontSettings>();
    }
}
