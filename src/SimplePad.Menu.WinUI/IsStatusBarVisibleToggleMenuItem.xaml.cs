using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Editor;
using SimplePad.StatusBar;

namespace SimplePad.Menu;

public sealed partial class IsStatusBarVisibleToggleMenuItem : ToggleMenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(IsStatusBarVisibleToggleMenuItem),
        null);

    private readonly IStatusBarSettings _statusBarSettings;

    public IsStatusBarVisibleToggleMenuItem()
    {
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
        UpdateIsChecked();
    }

    private void UpdateIsChecked()
    {
        IsChecked = _statusBarSettings.IsStatusBarVisible;
    }
}