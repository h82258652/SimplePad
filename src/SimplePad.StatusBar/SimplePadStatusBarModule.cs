using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.File;

namespace SimplePad.StatusBar;

public sealed class SimplePadStatusBarModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadEditorModule,
        SimplePadFileModule>();
}
