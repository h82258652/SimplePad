using SimplePad.Core.Modularity;
using SimplePad.Settings;

namespace SimplePad.UWP.UI;

public sealed class SimplePadUWPUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadSettingsModule>();
}
