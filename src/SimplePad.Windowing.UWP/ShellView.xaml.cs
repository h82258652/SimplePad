using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.Themes;
using Windows.UI.Xaml;

namespace SimplePad.Windowing;

public sealed partial class ShellView : ThemeContainer
{
    private readonly SettingsState _settingsState;

    public ShellView(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
        TabView.TabRoot = appWindow.TabRoot;

        UpdateTabViewVisibility();
        UpdateTitleBar();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateTabViewVisibility();
        UpdateTitleBar();
    }

    private void UpdateTabViewVisibility()
    {
        if (_settingsState.IsVisible)
        {
            TabView.Visibility = Visibility.Collapsed;
        }
        else
        {
            TabView.Visibility = Visibility.Visible;
        }
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
}