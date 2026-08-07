using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace SimplePad.Search;

internal sealed partial class SearchDownButton : Button
{
    public SearchDownButton()
    {
        DefaultStyleKey = typeof(SearchDownButton);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.WinUI/SearchDownButton.xaml");

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SearchDownCommand searchDownCommand = new();
        searchDownCommand.Execute(null);
    }
}