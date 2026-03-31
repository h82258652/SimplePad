using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Fonts;

namespace SimplePad.Editor;

public sealed class SimplePadEditorModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFontsModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<EditorZoomState>();
    }
}
