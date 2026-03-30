using SimplePad.Core.Modularity;

namespace SimplePad.Fonts.UWP.App;

public sealed class SimplePadFontsUWPAppModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFontsUWPModule>();
}
