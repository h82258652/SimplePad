using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SimplePad.MultiTab.UWP.Controls;

public sealed partial class AppTabViewItem : TabViewItem
{
    public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register(
        nameof(IsModified),
        typeof(bool),
        typeof(AppTabViewItem),
        new PropertyMetadata(false, OnIsModifiedChanged));

    public AppTabViewItem()
    {
        DefaultStyleKey = typeof(AppTabViewItem);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.MultiTab.UWP/Controls/AppTabViewItem.xaml");
    }

    private static void OnIsModifiedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public bool IsModified
    {
        get => (bool)GetValue(IsModifiedProperty); 
        set => SetValue(IsModifiedProperty, value);
    }
}
