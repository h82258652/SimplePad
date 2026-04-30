using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Menu;
using SimplePad.Settings;
using SimplePad.Themes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace SimplePad.Windowing;

public sealed partial class ShellView : ThemeContainer
{
    private readonly IAppWindowManager _appWindowManager;
    private readonly SettingsState _settingsState;

    public ShellView(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
        TabView.TabRoot = appWindow.TabRoot;

        UpdateContentGridVisibility();
        UpdateTitleBar();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    private async void OnCloseWindowKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        if (_appWindowManager.CurrentWindow is { } currentWindow)
        {
            _ = await _appWindowManager.CloseAsync(currentWindow);
        }
    }

    private void OnFindKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        FindCommand findCommand = new();
        findCommand.Execute(null);
    }

    private void OnFindNextKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        FindNextCommand findNextCommand = new();
        findNextCommand.Execute(null);
    }

    private void OnFindPreviousKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        FindPreviousCommand findPreviousCommand = new();
        findPreviousCommand.Execute(null);
    }

    private async void OnNewWindowKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        IAppWindow newAppWindow = await _appWindowManager.ShowNewWindowAsync();
        newAppWindow.Execute(appWindow => appWindow.TabRoot.AddBlankTab());
    }

    private void OnOpenKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        OpenCommand openCommand = new();
        openCommand.Execute(null);
    }

    private void OnReplaceKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        ReplaceCommand replaceCommand = new();
        replaceCommand.Execute(null);
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