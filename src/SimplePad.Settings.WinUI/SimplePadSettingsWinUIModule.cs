using SimplePad.Core.Modularity;

namespace SimplePad.Settings;

public sealed class SimplePadSettingsWinUIModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadSettingsModule>();
}
