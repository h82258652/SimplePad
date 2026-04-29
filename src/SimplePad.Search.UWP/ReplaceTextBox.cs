using System;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

internal sealed partial class ReplaceTextBox : TextBox
{
    private readonly SearchViewState _searchViewState;

    internal ReplaceTextBox()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        DefaultStyleKey = typeof(ReplaceTextBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.UWP/ReplaceTextBox.xaml");

        UpdateText();

        TextChanged += OnTextChanged;
        _searchViewState.SearchTextChanged += OnSearchViewStateSearchTextChanged;
    }

    private void OnSearchViewStateSearchTextChanged(object? sender, string e)
    {
        UpdateText();
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        _searchViewState.ReplaceText = Text;
    }

    private void UpdateText()
    {
        Text = _searchViewState.ReplaceText;
    }
}