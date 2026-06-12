using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace SimplePad.Tabs;

public sealed partial class AppTabViewItem : TabViewItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(AppTabViewItem),
        new PropertyMetadata(null, OnTabChanged));

    private static void OnTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public Tab? Tab
    {
        get => (Tab?)GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    public AppTabViewItem()
    {
        InitializeComponent();
    }
}
