using SimplePad.Core.Modularity;

namespace SimplePad.Fonts;

public sealed class SimplePadFontsSettingsUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFontsUWPModule>();
}
