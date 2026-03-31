using SimplePad.Core.Modularity;
using SimplePad.Editor;

namespace SimplePad.StatusBar;

public sealed class SimplePadStatusBarModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadEditorModule>();
}
