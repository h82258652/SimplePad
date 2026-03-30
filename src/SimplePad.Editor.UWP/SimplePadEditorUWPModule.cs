using SimplePad.Core.Modularity;

namespace SimplePad.Editor.UWP;

public sealed class SimplePadEditorUWPModule : AppModuleBase
{
    public override DependsOn DependModules => DependsOn.Create<SimplePadEditorUWPModule>();
}
