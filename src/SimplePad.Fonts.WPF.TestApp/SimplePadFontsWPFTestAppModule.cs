using SimplePad.Core.Modularity;

namespace SimplePad.Fonts.TestApp;

public sealed class SimplePadFontsWPFTestAppModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadFontsWPFModule>();
}