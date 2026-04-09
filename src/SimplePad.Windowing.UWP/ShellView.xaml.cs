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

        UpdateContentGridVisibility();
        UpdateTitleBar();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateContentGridVisibility();
        UpdateTitleBar();
    }

    private void UpdateContentGridVisibility()
    {
        if (_settingsState.IsVisible)
        {
            ContentGrid.Visibility = Visibility.Collapsed;
        }
        else
        {
            ContentGrid.Visibility = Visibility.Visible;
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