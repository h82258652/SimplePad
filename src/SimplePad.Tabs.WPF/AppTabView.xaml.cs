using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Dragablz;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Tabs;

public partial class AppTabView : TabablzControl
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(AppTabView),
        new PropertyMetadata(null, OnTabRootChanged));

    private readonly Dispatcher _dispatcher;
    private readonly TabManager _tabManager;

    public AppTabView()
    {
        _dispatcher = Dispatcher;
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();

        InitializeComponent();

        UpdateItemsSource();
    }

    public TabRoot? TabRoot
    {
        get => (TabRoot?)GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        Button defaultAddButton = (Button)GetTemplateChild("DefaultAddButton");
        defaultAddButton.Command = null;
        defaultAddButton.Click += OnDefaultAddButtonClick;
    }

    private static void OnTabRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTabView self = (AppTabView)d;
        TabRoot? oldTabRoot = (TabRoot?)e.OldValue;
        if (oldTabRoot is not null)
        {
            oldTabRoot.SelectedTabChanged -= self.OnTabRootSelectedTabChanged;
        }

        TabRoot? newTabRoot = (TabRoot?)e.NewValue;
        if (newTabRoot is not null)
        {
            newTabRoot.SelectedTabChanged += self.OnTabRootSelectedTabChanged;
        }

        self.UpdateItemsSource();
        self.UpdateSelectedItem();
    }

    private void AddBlankTab()
    {
        TabRoot?.AddBlankTab();
    }

    private void OnDefaultAddButtonClick(object sender, RoutedEventArgs e)
    {
        AddBlankTab();
    }

    private void OnTabRootSelectedTabChanged(object? sender, Tab? e)
    {
        _dispatcher.Invoke(UpdateSelectedItem);
    }

    private void UpdateItemsSource()
    {
        ItemsSource = TabRoot?.Tabs;
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = TabRoot?.SelectedTab;
    }
}