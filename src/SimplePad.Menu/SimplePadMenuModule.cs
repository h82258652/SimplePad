using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public sealed class SimplePadMenuModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadEditorModule,
        SimplePadWindowingModule>();
}
