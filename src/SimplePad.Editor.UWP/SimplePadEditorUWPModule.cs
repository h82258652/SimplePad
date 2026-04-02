using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;

namespace SimplePad.Editor;

public sealed class SimplePadEditorUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadEditorUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IEditorSettings, UWPEditorSettings>();
    }
}
