using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

internal sealed partial class SearchDownButton : Button
{
    public SearchDownButton()
    {
        DefaultStyleKey = typeof(SearchDownButton);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.UWP/SearchDownButton.xaml");

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
    }
}
