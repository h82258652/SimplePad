using SimplePad.Core.Modularity;

namespace SimplePad.Settings;

public sealed class SimplePadSettingsWPFModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadSettingsModule>();
}
