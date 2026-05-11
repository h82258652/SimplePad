using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.Themes;

namespace SimplePad.Windowing;

public partial class ShellWindow : ThemeWindow
{
    private readonly SettingsState _settingsState;

    public ShellWindow(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
    }
}
