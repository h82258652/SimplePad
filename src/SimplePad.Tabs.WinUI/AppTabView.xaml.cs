using System;
using System.Collections.Specialized;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SimplePad.Tabs;

public sealed partial class AppTabView : TabView
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(AppTabView),
        new PropertyMetadata(null, OnTabRootChanged));

    public AppTabView()
    {
        InitializeComponent();
    }

    public TabRoot? TabRoot
    {
        get => (TabRoot?)GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    private static void OnTabRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTabView self = (AppTabView)d;
        TabRoot? oldTabRoot = (TabRoot?)e.OldValue;
        if (oldTabRoot is not null)
        {
            oldTabRoot.Tabs.CollectionChanged -= self.OnTabRootTabsCollectionChanged;
            oldTabRoot.SelectedTabChanged -= self.OnTabRootSelectedTabChanged;
        }

        TabRoot? newTabRoot = (TabRoot?)e.NewValue;
        if (newTabRoot is not null)
        {
            newTabRoot.Tabs.CollectionChanged += self.OnTabRootTabsCollectionChanged;
            newTabRoot.SelectedTabChanged += self.OnTabRootSelectedTabChanged;
        }

        self.UpdateSelectedItem();
    }

    private void OnTabRootSelectedTabChanged(object? sender, Tab? e)
    {
        throw new NotImplementedException();
    }

    private void OnTabRootTabsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void UpdateSelectedItem()
    {
        throw new NotImplementedException();
    }
}