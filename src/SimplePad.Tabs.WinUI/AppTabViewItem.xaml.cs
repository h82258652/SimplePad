using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
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
        }

        Tab? newTab = (Tab?)e.NewValue;
        if (newTab is not null)
        {
            newTab.TitleChanged += self.OnTabTitleChanged;
            newTab.IsModifiedChanged += self.OnTabIsModifiedChanged;
        }

        self.UpdateHeader();
        self.UpdateTextBox();
        self.UpdateStatusBarLineEndings();
    }

    private void OnTabIsModifiedChanged(object? sender, bool e)
    {
        UpdateModifiedIndicatorVisibility();
    }

    private void UpdateModifiedIndicatorVisibility()
    {
        // TODO
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
        throw new NotImplementedException();
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateTextBoxPadding();
    }

    private void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        throw new NotImplementedException();
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

    private void UpdateStatusBarDividerVisibility()
    {
        // TODO
    }

    private void UpdateStatusBarLineEndings()
    {
        // TODO
    }

    private void UpdateTextBox()
    {
        TextBox.Text = Tab?.Content ?? string.Empty;
    }

    private void UpdateTextBoxPadding()
    {
        // TODO
    }
}