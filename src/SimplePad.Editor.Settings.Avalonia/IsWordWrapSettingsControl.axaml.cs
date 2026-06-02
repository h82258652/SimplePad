using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SimplePad.Editor;

public sealed partial class IsWordWrapSettingsControl : UserControl
{
    public IsWordWrapSettingsControl()
    {
        InitializeComponent();
    }

    private void OnIsWordWrapToggleSwitchIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
    }
}