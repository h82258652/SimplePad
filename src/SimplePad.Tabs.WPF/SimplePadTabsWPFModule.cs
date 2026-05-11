using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core.Modularity;
using SimplePad.Editor;

namespace SimplePad.Tabs;

public sealed class SimplePadTabsWPFModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadTabsModule,
        SimplePadEditorWPFModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<ITabsSettings, WPFTabsSettings>();
        context.Services.AddTransient<IConfirmCloseService, WPFConfirmCloseService>();
    }
}
