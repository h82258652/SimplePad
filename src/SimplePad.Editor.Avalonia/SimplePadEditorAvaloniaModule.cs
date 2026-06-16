using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Fonts;

namespace SimplePad.Editor;

public sealed class SimplePadEditorAvaloniaModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadEditorModule,
        SimplePadFontsAvaloniaModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IEditorSettings, AvaloniaEditorSettings>();
    }
}
