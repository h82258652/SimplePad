using SimplePad.Core.Modularity;
using SimplePad.Services.UWP;
using SimplePad.Settings.UWP;
using SimplePad.UWP.UI;

namespace SimplePad.UWP.App;

public sealed class SimplePadUWPAppModule : AppModuleBase
{
    public override DependsOn DependModules =>
        DependsOn.Create<
            SimplePadSettingsUWPModule,
            SimplePadServiceUWPModule,
            SimplePadUWPUIModule
        >();
}
