using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor.Settings;
using SimplePad.Editor.UWP.Settings;

namespace SimplePad.Editor.UWP;

public sealed class SimplePadEditorUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadEditorUWPModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IEditorSettings, UWPEditorSettings>();
    }
}
