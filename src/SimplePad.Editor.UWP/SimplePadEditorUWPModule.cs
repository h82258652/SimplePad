using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Fonts;
using SimplePad.Themes;

namespace SimplePad.Editor;

public sealed class SimplePadEditorUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadEditorModule,
        SimplePadFontsUWPModule,
        SimplePadThemesUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IEditorSettings, UWPEditorSettings>();
    }
}
