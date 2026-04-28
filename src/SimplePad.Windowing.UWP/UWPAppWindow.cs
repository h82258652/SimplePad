using System;
using System.Linq;
using CommunityToolkit.WinUI.Helpers;
using SimplePad.Core.Extensions;
using SimplePad.Tabs;
using SimplePad.Themes;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Core.Preview;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SimplePad.Windowing;

internal sealed class UWPAppWindow : IAppWindow
{
    private readonly IAppWindowManager _appWindowManager;
    private readonly TabManager _tabManager;
    private readonly ThemeListener _themeListener;
    private readonly IThemeSettings _themeSettings;

    internal UWPAppWindow(IAppWindowManager appWindowManager, CoreDispatcher dispatcher, IThemeSettings themeSettings, TabManager tabManager)
    {
        _themeListener = new ThemeListener();
        _appWindowManager = appWindowManager;
        Dispatcher = dispatcher;
        _themeSettings = themeSettings;
        _tabManager = tabManager;

        UpdateTitleBarButtons();

        ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 195));

        _themeSettings.AppThemeChanged += OnThemeSettingsAppThemeChanged;
        _themeListener.ThemeChanged += OnThemeListenerThemeChanged;

        TabRoot.Tabs.CollectionChanged += OnTabsCollectionChanged;
        SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += OnAppWindowCloseRequested;
    }

    public TabRoot TabRoot { get; } = new TabRoot();

    internal CoreDispatcher Dispatcher { get; }

    public async void Execute(Action<IAppWindow> action)
    {
        await Dispatcher.SafeRunAsync(() => action(this));
    }

    private static void UpdateTitleBarButtonsForDarkTheme()
    {
        ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
        titleBar.ButtonForegroundColor = Colors.White;
        titleBar.ButtonHoverForegroundColor = Colors.White;
        titleBar.ButtonHoverBackgroundColor = Color.FromArgb(24, 255, 255, 255);
    }

    private static void UpdateTitleBarButtonsForLightTheme()
    {
        ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
        titleBar.ButtonForegroundColor = Colors.Black;
        titleBar.ButtonHoverForegroundColor = Colors.Black;
        titleBar.ButtonHoverBackgroundColor = Color.FromArgb(24, 0, 0, 0);
    }

    private async void OnAppWindowCloseRequested(object? sender, SystemNavigationCloseRequestedPreviewEventArgs e)
    {
        foreach (Tab tab in TabRoot.Tabs.ToList())
        {
            if (!await _tabManager.CloseAsync(tab))
            {
                e.Handled = true;
                return;
            }
        }
    }

    private async void OnTabsCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (TabRoot.Tabs.Count == 0)
        {
            await _appWindowManager.CloseAsync(this);
        }
    }

    private async void OnThemeListenerThemeChanged(ThemeListener sender)
    {
        await Dispatcher.SafeRunAsync(UpdateTitleBarButtons);
    }

    private async void OnThemeSettingsAppThemeChanged(object? sender, AppTheme e)
    {
        await Dispatcher.SafeRunAsync(UpdateTitleBarButtons);
    }

    private void UpdateTitleBarButtons()
    {
        switch (_themeSettings.AppTheme)
        {
            case AppTheme.Default:
                switch (_themeListener.CurrentTheme)
                {
                    case ApplicationTheme.Light:
                        UpdateTitleBarButtonsForLightTheme();
                        break;

                    case ApplicationTheme.Dark:
                        UpdateTitleBarButtonsForDarkTheme();
                        break;
                }

                break;

            case AppTheme.Light:
                UpdateTitleBarButtonsForLightTheme();
                break;

            case AppTheme.Dark:
                UpdateTitleBarButtonsForDarkTheme();
                break;
        }
    }
}