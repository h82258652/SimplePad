using SimplePad.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Windowing;

public sealed partial class ShellView : UserControl
{
    private readonly SettingsState _settingsState;

    public ShellView(IAppWindow appWindow)
    {
        _settingsState = appWindow.SettingsState;

        InitializeComponent();
        TabView.TabRoot = appWindow.TabRoot;

        Window.Current.SetTitleBar(TabView.TitleBar);

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

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