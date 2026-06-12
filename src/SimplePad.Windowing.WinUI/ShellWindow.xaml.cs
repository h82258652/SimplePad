using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SimplePad.Core;
using SimplePad.Settings;

namespace SimplePad.Windowing;

public sealed partial class ShellWindow : Window
{
    private readonly IAppWindowManager _appWindowManager;
    private readonly SettingsState _settingsState;

    public ShellWindow(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
        TabView.TabRoot = appWindow.TabRoot;

        UpdateContentGridVisibility();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateContentGridVisibility();
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
}