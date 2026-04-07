using SimplePad.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Windowing;

public sealed partial class ShellView : UserControl
{
    public ShellView(IAppWindow appWindow)
    {
        _settingsState = appWindow.SettingsState;

        InitializeComponent();

        Window.Current.SetTitleBar(TabView.TitleBar);

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    private readonly SettingsState _settingsState;

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        if (_settingsState.IsVisible)
        {

        }
        else
        {
            Window.Current.SetTitleBar(TabView.TitleBar);
        }
    }
}