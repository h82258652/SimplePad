using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.File;
using SimplePad.Search;
using SimplePad.StatusBar;
using System;
using System.Threading.Tasks;

namespace SimplePad.Tabs;

public sealed partial class AppTabViewItem : TabViewItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(AppTabViewItem),
        new PropertyMetadata(null, OnTabChanged));

    private readonly SearchViewState _searchViewState;
    private readonly IStatusBarSettings _statusBarSettings;

    public AppTabViewItem()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();

        InitializeComponent();

        UpdateTextBoxPadding();
        UpdateStatusBarDividerVisibility();
        UpdateStatusBarLineEndings();

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
        _statusBarSettings.IsStatusBarVisibleChanged += OnStatusBarSettingsIsStatusBarVisibleChanged;
        RegisterPropertyChangedCallback(IsSelectedProperty, OnIsSelectedChanged);
    }

    public Tab? Tab
    {
        get => (Tab?)GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    private static void OnTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTabViewItem self = (AppTabViewItem)d;
        Tab? oldTab = (Tab?)e.OldValue;
        if (oldTab is not null)
        {
            oldTab.TitleChanged -= self.OnTabTitleChanged;
            oldTab.IsModifiedChanged -= self.OnTabIsModifiedChanged;
            oldTab.ContentChanged -= self.OnTabContentChanged;
            oldTab.LineEndingsChanged -= self.OnTabLineEndingsChanged;
        }

        Tab? newTab = (Tab?)e.NewValue;
        if (newTab is not null)
        {
            newTab.TitleChanged += self.OnTabTitleChanged;
            newTab.IsModifiedChanged += self.OnTabIsModifiedChanged;
            newTab.ContentChanged += self.OnTabContentChanged;
            newTab.LineEndingsChanged += self.OnTabLineEndingsChanged;
        }

        self.UpdateHeader();
        self.UpdateModifiedIndicatorVisibility();
        self.UpdateTextBox();
        self.UpdateStatusBarLineEndings();
    }

    private async void OnIsSelectedChanged(DependencyObject sender, DependencyProperty dp)
    {
        if (IsSelected)
        {
            _searchViewState.TextBox = TextBox;
            await Task.Yield();
            TextBox.Focus();
        }
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
        UpdateTextBox();
    }

    private void OnTabIsModifiedChanged(object? sender, bool e)
    {
        UpdateModifiedIndicatorVisibility();
    }

    private void OnTabLineEndingsChanged(object? sender, LineEndings e)
    {
        UpdateStatusBarLineEndings();
    }

    private void OnTabTitleChanged(object? sender, string e)
    {
        UpdateHeader();
    }

    private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        if (Tab is { } tab)
        {
            tab.Content = TextBox.Text;
        }
    }

    private void UpdateHeader()
    {
        Header = Tab?.Title ?? TabConstants.DefaultTabTitle;
    }

    private void UpdateModifiedIndicatorVisibility()
    {
        // TODO
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
        StatusBar.LineEndings = Tab?.LineEndings ?? LineEndings.CRLF;
    }

    private void UpdateTextBox()
    {
        TextBox.Text = Tab?.Content ?? string.Empty;
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

        TextBox.Padding = padding;
    }
}