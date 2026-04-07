using SimplePad.Tabs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class NewTabMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(NewTabMenuFlyoutItem),
        null);

    public NewTabMenuFlyoutItem()
    {
        InitializeComponent();
    }

    public TabRoot? TabRoot
    {
        get => (TabRoot?)GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        TabRoot?.Add();
    }
}