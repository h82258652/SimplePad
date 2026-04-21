using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using SimplePad.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class FindPreviousMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(FindPreviousMenuFlyoutItem),
        null);

    private readonly SearchViewState _searchViewState;

    public FindPreviousMenuFlyoutItem()
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
        string searchText = _searchViewState.SearchText;
        if (string.IsNullOrEmpty(searchText))
        {
            _searchViewState.IsVisible = true;
            _searchViewState.IsReplaceMode = false;
            return;
        }

        if (TextBox is not { } textBox)
        {
            return;
        }

        if (textBox.Text.Contains(searchText))
        {
            // TODO jump selection
        }
        else
        {
            // TODO show not find dialog
        }
    }
}