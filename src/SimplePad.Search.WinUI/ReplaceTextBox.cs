using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using System;

namespace SimplePad.Search;

internal sealed partial class ReplaceTextBox : TextBox
{
    private readonly SearchViewState _searchViewState;

    internal ReplaceTextBox()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        DefaultStyleKey = typeof(ReplaceTextBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.WinUI/ReplaceTextBox.xaml");

        UpdateText();

        TextChanged += OnTextChanged;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _searchViewState.SearchTextChanged += OnSearchViewStateSearchTextChanged;

        UpdateText();
    }

    private void OnSearchViewStateSearchTextChanged(object? sender, string e)
    {
        UpdateText();
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        _searchViewState.ReplaceText = Text;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _searchViewState.SearchTextChanged -= OnSearchViewStateSearchTextChanged;
    }

    private void UpdateText()
    {
        Text = _searchViewState.ReplaceText;
    }
}