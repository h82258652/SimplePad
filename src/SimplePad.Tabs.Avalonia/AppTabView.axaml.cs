using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SimplePad.Tabs;

public partial class AppTabView : UserControl
{
    public static readonly StyledProperty<TabRoot?> TabRootProperty = AvaloniaProperty.Register<AppTabView, TabRoot?>(nameof(TabRoot));

    public AppTabView()
    {
        InitializeComponent();
    }

    public TabRoot? TabRoot
    {
        get => GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }
}