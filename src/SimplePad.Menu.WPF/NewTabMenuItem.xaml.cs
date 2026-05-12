using SimplePad.Tabs;
using System.Windows;
using System.Windows.Controls;

namespace SimplePad.Menu;

public partial class NewTabMenuItem : MenuItem
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(NewTabMenuItem),
        null);

    public TabRoot? TabRoot
    {
        get => (TabRoot?)GetValue(TabRootProperty);
        set=> SetValue(TabRootProperty, value);
    }

    public NewTabMenuItem()
    {
        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        TabRoot?.AddBlankTab();
    }
}
