using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

internal sealed partial class SearchUpButton : Button
{
    public SearchUpButton()
    {
        DefaultStyleKey = typeof(SearchUpButton);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.UWP/SearchUpButton.xaml");

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SearchUpCommand searchUpCommand = new();
        searchUpCommand.Execute(null);
    }
}