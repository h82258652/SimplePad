using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.File;
using SimplePad.Search;
using SimplePad.StatusBar;

namespace SimplePad.Tabs;

public sealed partial class AppTabContentControl : UserControl
{
    private readonly Dispatcher _dispatcher;
    private readonly SearchViewState _searchViewState;
    private readonly IStatusBarSettings _statusBarSettings;

    public AppTabContentControl()
    {
        _dispatcher = Dispatcher;
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();

        InitializeComponent();
        AppMenuBar.TextBox = TextBox;
        StatusBar.TextBox = TextBox;

        UpdateTextBoxPadding();
        UpdateStatusBarDividerVisibility();
        UpdateStatusBarLineEndings();

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
        _statusBarSettings.IsStatusBarVisibleChanged += OnStatusBarSettingsIsStatusBarVisibleChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is Tab oldTab)
        {
            oldTab.ContentChanged -= OnTabContentChanged;
            oldTab.LineEndingsChanged -= OnTabLineEndingsChanged;
        }

        if (e.NewValue is Tab newTab)
        {
            newTab.ContentChanged += OnTabContentChanged;
            newTab.LineEndingsChanged += OnTabLineEndingsChanged;
        }

        UpdateTextBox();
        UpdateStatusBarLineEndings();
    }

    private void OnSearchViewStateIsReplaceModeChanged(object? sender, bool e)
    {
        UpdateTextBoxPadding();
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateTextBoxPadding();
    }

    private void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        UpdateStatusBarDividerVisibility();
    }

    private void OnTabContentChanged(object? sender, string e)
    {
        _dispatcher.Invoke(UpdateTextBox);
    }

    private void OnTabLineEndingsChanged(object? sender, LineEndings e)
    {
        UpdateStatusBarLineEndings();
    }

    private void OnTextBoxTextChanged(object sender, string e)
    {
        if (DataContext is Tab tab)
        {
            tab.Content = TextBox.Text;
        }
    }

    private void UpdateStatusBarDividerVisibility()
    {
        if (_statusBarSettings.IsStatusBarVisible)
        {
            StatusBarDivider.Visibility = Visibility.Visible;
        }
        else
        {
            StatusBarDivider.Visibility = Visibility.Collapsed;
        }
    }

    private void UpdateStatusBarLineEndings()
    {
        StatusBar.LineEndings = (DataContext as Tab)?.LineEndings ?? LineEndings.CRLF;
    }

    private void UpdateTextBox()
    {
        TextBox.Text = (DataContext as Tab)?.Content ?? string.Empty;
    }

    private void UpdateTextBoxPadding()
    {
        Thickness padding = new(16);
        if (_searchViewState.IsVisible)
        {
            if (_searchViewState.IsReplaceMode)
            {
                padding.Top = 120;
            }
            else
            {
                padding.Top = 80;
            }
        }

        TextBox.ContentPadding = padding;
    }
}