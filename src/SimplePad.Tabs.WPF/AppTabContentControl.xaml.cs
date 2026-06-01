using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.StatusBar;

namespace SimplePad.Tabs;

public sealed partial class AppTabContentControl : UserControl
{
    private readonly IStatusBarSettings _statusBarSettings;

    public AppTabContentControl()
    {
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();

        InitializeComponent();
        AppMenuBar.TextBox = TextBox;
        StatusBar.TextBox = TextBox;

        UpdateStatusBarDividerVisibility();

        _statusBarSettings.IsStatusBarVisibleChanged += OnStatusBarSettingsIsStatusBarVisibleChanged;
    }

    private void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        UpdateStatusBarDividerVisibility();
    }

    private void OnTextBoxTextChanged(object sender, string e)
    {
        if (DataContext is Tab tab)
        {
            tab.Content = TextBox.Text;
        }
    }

    private void UpdateStatusBarDividerVisibility()
    {
        if (_statusBarSettings.IsStatusBarVisible)
        {
            StatusBarDivider.Visibility = Visibility.Visible;
        }
        else
        {
            StatusBarDivider.Visibility = Visibility.Collapsed;
        }
    }
}