using SimplePad.Core.Modularity;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed class SimplePadMenuModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadEditorModule>();
}
