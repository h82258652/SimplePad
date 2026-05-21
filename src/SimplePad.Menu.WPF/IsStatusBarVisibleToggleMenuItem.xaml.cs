using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using SimplePad.StatusBar;

namespace SimplePad.Menu;

public sealed partial class IsStatusBarVisibleToggleMenuItem : MenuItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(IsStatusBarVisibleToggleMenuItem),
        null);

    private readonly Dispatcher _dispatcher;
    private readonly IStatusBarSettings _statusBarSettings;

    public IsStatusBarVisibleToggleMenuItem()
    {
        _dispatcher = Dispatcher;
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();

        InitializeComponent();

        UpdateIsChecked();

        _statusBarSettings.IsStatusBarVisibleChanged += OnStatusBarSettingsIsStatusBarVisibleChanged;
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _statusBarSettings.IsStatusBarVisible = IsChecked;
        TextBox?.Focus();
    }

    private void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateIsChecked);
    }

    private void UpdateIsChecked()
    {
        IsChecked = _statusBarSettings.IsStatusBarVisible;
    }
}