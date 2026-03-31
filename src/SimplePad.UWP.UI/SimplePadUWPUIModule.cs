using SimplePad.Core.Modularity;
using SimplePad.Editor.UWP;
using SimplePad.Fonts.UWP;
using SimplePad.MultiTab.UWP;
using SimplePad.Settings;
using SimplePad.StatusBar.UWP;
using SimplePad.Themes.UWP;

namespace SimplePad.UWP.UI;

public sealed class SimplePadUWPUIModule : AppModuleBase
{
    public override DependsOn DependModules =>
        DependsOn.Create<
            SimplePadSettingsModule,
            SimplePadThemesUWPModule,
            SimplePadFontsUWPModule,
            SimplePadStatusBarUWPModule,
            SimplePadEditorUWPModule,
            SimplePadMultiTabUWPModule
        >();
}
