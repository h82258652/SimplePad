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

    public AppTabViewItem()
    {
        InitializeComponent();
    }

    public Tab? Tab
    {
        get => (Tab?)GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    private static void OnTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTabViewItem self = (AppTabViewItem)d;
        Tab? oldTab = (Tab?)e.OldValue;
        if (oldTab is not null)
        {
        }

        Tab? newTab = (Tab?)e.NewValue;
        if (newTab is not null)
        {

        }

        self.UpdateTextBox();
    }

    private void UpdateTextBox()
    {
        TextBox.Text = Tab?.Content ?? string.Empty;
    }
}