using System;
using Microsoft.UI.Xaml.Controls;

namespace SimplePad.MultiTab.UWP.Controls;

public sealed partial class AppTabView : TabView
{
    public AppTabView()
    {
        DefaultStyleKey = typeof(AppTabView);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.MultiTab.UWP/Controls/AppTabView.xaml"
        );
    }
}
