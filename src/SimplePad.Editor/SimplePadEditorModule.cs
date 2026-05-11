using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Editor;

public sealed class SimplePadEditorModule : AppModuleBase
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton(_ => new EditorZoomState());
    }
}
