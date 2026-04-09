using SimplePad.Tabs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class SaveAllMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(SaveAllMenuFlyoutItem),
        null);

    public SaveAllMenuFlyoutItem()
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
    }
}