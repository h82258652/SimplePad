using SimplePad.Tabs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class SaveAsMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(SaveAsMenuFlyoutItem),
        null);

    public SaveAsMenuFlyoutItem()
    {
        InitializeComponent();
    }

    public Tab? Tab
    {
        get => (Tab?)GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        if (Tab is { } tab)
        {
            // TODO
        }
    }
}