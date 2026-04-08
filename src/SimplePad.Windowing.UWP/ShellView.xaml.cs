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
        SettingsView.SettingsState = _settingsState;

        UpdateTitleBar();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    private void UpdateTitleBar()
    {
        if (_settingsState.IsVisible)
        {
            Window.Current.SetTitleBar(SettingsView.TitleBar);
        }
        else
        {
            Window.Current.SetTitleBar(TabView.TitleBar);
        }
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateTitleBar();
    }
}