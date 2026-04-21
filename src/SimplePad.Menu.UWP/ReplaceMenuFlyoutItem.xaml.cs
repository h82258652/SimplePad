using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using SimplePad.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class ReplaceMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(ReplaceMenuFlyoutItem),
        null);

    private readonly SearchViewState _searchViewState;

    public ReplaceMenuFlyoutItem()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsVisible = true;
        _searchViewState.IsReplaceMode = true;

        if (TextBox is { } textBox)
        {
            string selectedText = textBox.SelectedText;
            if (!string.IsNullOrEmpty(selectedText))
            {
                _searchViewState.SearchText = selectedText;
            }
        }
    }
}