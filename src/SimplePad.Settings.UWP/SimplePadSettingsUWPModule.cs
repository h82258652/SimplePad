using SimplePad.Core.Modularity;

namespace SimplePad.Settings;

public sealed class SimplePadSettingsUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadSettingsModule>();
}
