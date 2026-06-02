using SimplePad.Core.Modularity;

namespace SimplePad.StatusBar;

public sealed class SimplePadStatusBarAvaloniaModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadStatusBarModule>();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        context.Services.AddSingleton<IStatusBarSettings, AvaloniaStatusBarSettings>();
    }
}
