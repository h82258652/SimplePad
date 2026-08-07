using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace SimplePad.Search;

internal sealed partial class SearchUpButton : Button
{
    public SearchUpButton()
    {
        DefaultStyleKey = typeof(SearchUpButton);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.WinUI/SearchUpButton.xaml");

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SearchUpCommand searchUpCommand = new();
        searchUpCommand.Execute(null);
    }
}