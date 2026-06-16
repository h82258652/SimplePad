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
        }

        Tab? newTab = (Tab?)e.NewValue;
        if (newTab is not null)
        {
            newTab.TitleChanged += self.OnTabTitleChanged;
        }

        self.UpdateHeader();
        self.UpdateTextBox();
    }

    private void OnTabTitleChanged(object? sender, string e)
    {
        UpdateHeader();
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

    private void UpdateTextBox()
    {
        TextBox.Text = Tab?.Content ?? string.Empty;
    }
}