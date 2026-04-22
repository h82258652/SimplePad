using System;
using CommunityToolkit.WinUI.Helpers;
using SimplePad.Core.Extensions;
using SimplePad.Tabs;
using SimplePad.Themes;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SimplePad.Windowing;

public sealed class UWPAppWindow : IAppWindow
{
    private readonly ThemeListener _themeListener;
    private readonly IThemeSettings _themeSettings;

    public UWPAppWindow(CoreDispatcher dispatcher, IThemeSettings themeSettings)
    {
        _themeListener = new ThemeListener();
        Dispatcher = dispatcher;
        _themeSettings = themeSettings;

        UpdateTitleBarButtons();

        _themeSettings.AppThemeChanged += OnThemeSettingsAppThemeChanged;
        _themeListener.ThemeChanged += OnThemeListenerThemeChanged;
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