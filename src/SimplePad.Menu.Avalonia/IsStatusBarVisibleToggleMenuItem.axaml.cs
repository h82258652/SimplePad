using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using SimplePad.StatusBar;

namespace SimplePad.Menu;

public partial class IsStatusBarVisibleToggleMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<IsStatusBarVisibleToggleMenuItem, IAppTextBox?>(nameof(TextBox));

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
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnClick(object? sender, RoutedEventArgs e)
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