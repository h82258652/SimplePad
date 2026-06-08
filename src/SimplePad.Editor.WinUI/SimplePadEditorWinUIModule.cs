using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Fonts;
using SimplePad.Themes;

namespace SimplePad.Editor;

public sealed class SimplePadEditorWinUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadEditorModule, 
        SimplePadFontsWinUIModule,
        SimplePadThemesWinUIModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IEditorSettings, WinUIEditorSettings>();
    }
}
