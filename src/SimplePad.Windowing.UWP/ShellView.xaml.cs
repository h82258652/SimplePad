using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Windowing;

public sealed partial class ShellView : UserControl
{
    private readonly SettingsState _settingsState;

    public ShellView(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
        TabView.TabRoot = appWindow.TabRoot;

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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _ = ServiceLocator.Current.GetRequiredService<IAppWindowManager>().ShowNewWindowAsync();
    }
}