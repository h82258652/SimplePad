using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

internal sealed partial class ExitFindAndReplaceButton : Button
{
    private readonly SearchViewState _searchViewState;

    public ExitFindAndReplaceButton()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        DefaultStyleKey = typeof(ExitFindAndReplaceButton);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.UWP/ExitFindAndReplaceButton.xaml");

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsVisible = false;
    }
}