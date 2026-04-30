using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Search;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public sealed class SimplePadMenuModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<
        SimplePadEditorModule,
        SimplePadSearchModule,
        SimplePadWindowingModule>();
}
