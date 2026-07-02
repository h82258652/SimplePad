using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using SimplePad.Core;
using SimplePad.Menu;
using SimplePad.Settings;
using System.Collections.Specialized;

namespace SimplePad.Tabs;

public sealed partial class AppTabView : TabView
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(AppTabView),
        new PropertyMetadata(null, OnTabRootChanged));

    private readonly SettingsState _settingsState;
    private readonly TabManager _tabManager;

    public AppTabView()
    {
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

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

    private void AddBlankTab()
    {
        TabRoot?.AddBlankTab();
    }

    private void OnAddTabButtonClick(TabView sender, object args)
    {
        AddBlankTab();
    }

    private async void OnCloseTabKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        if (TabRoot?.SelectedTab is { } selectedTab)
        {
            await _tabManager.CloseAsync(selectedTab);
        }
    }

    private void OnGoToLineKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        if (SelectedItem is { } selectedItem
            && ContainerFromItem(selectedItem) is AppTabViewItem tabViewItem)
        {
            GoToLineCommand goToLineCommand = new()
            {
                TextBox = tabViewItem.TextBox
            };
            goToLineCommand.Execute(null);
        }
    }

    private void OnNewTabKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        AddBlankTab();
    }

    private void OnSaveAllKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        SaveAllCommand saveAllCommand = new();
        saveAllCommand.Execute(null);
    }

    private async void OnSaveAsKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        if (TabRoot?.SelectedTab is { } selectedTab)
        {
            _ = await _tabManager.SaveToAnotherFileAsync(selectedTab);
        }
    }

    private async void OnSaveKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        if (TabRoot?.SelectedTab is { } selectedTab)
        {
            _ = await _tabManager.SaveAsync(selectedTab);
        }
    }

    private void OnTabRootSelectedTabChanged(object? sender, Tab? e)
    {
        UpdateSelectedItem();
    }

    private void OnTabRootTabsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // If the Tabs collection is changed (eg. Open a file), close the settings view to ensure the new tab is visible
        _settingsState.IsVisible = false;
    }

    private async void OnTabViewTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        if (args.Item is Tab tab)
        {
            await _tabManager.CloseAsync(tab);
        }
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = TabRoot?.SelectedTab;
    }
}