using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Search;

internal sealed class ReplaceTextBox : TextBox
{
    private readonly SearchViewState _searchViewState;

    static ReplaceTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ReplaceTextBox), new FrameworkPropertyMetadata(typeof(ReplaceTextBox)));
    }

    internal ReplaceTextBox()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

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
