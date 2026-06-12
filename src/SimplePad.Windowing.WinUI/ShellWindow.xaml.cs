using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SimplePad.Core;
using SimplePad.Settings;
using System;

namespace SimplePad.Windowing;

public sealed partial class ShellWindow : Window
{
    private readonly IAppWindowManager _appWindowManager;
    private readonly SettingsState _settingsState;

    internal ShellWindow(WinUIAppWindow appWindow, IServiceProvider scopeServiceProvider)
    {
        appWindow.ShellWindow = this;
        ServiceLocator.SetScopedLocatorProvider(AppWindow.Id, scopeServiceProvider);

        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();
        AppWindowInstance = appWindow;

        InitializeComponent();
        TabView.TabRoot = appWindow.TabRoot;

        UpdateContentGridVisibility();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    internal IAppWindow AppWindowInstance { get; }

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