using SimplePad.Core.Modularity;

namespace SimplePad.Fonts.TestApp;

public sealed class SimplePadFontsUWPTestAppModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFontsUWPModule>();
}
