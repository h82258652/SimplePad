using System.Collections.Specialized;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using SimplePad.Menu;
using SimplePad.Settings;
using SimplePad.Windowing;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SimplePad.Tabs;

[TemplatePart(Name = TitleBarContainerTemplateName, Type = typeof(Border))]
public sealed partial class AppTabView : TabView
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(AppTabView),
        new PropertyMetadata(null, OnTabRootChanged));

    public static readonly DependencyProperty TitleBarProperty = DependencyProperty.Register(
        nameof(TitleBar),
        typeof(UIElement),
        typeof(AppTabView),
        new PropertyMetadata(null, OnTitleBarChanged));

    private const string TitleBarContainerTemplateName = "PART_TitleBarContainer";

    private readonly IAppWindowManager _appWindowManager;
    private readonly CoreDispatcher _dispatcher;
    private readonly SettingsState _settingsState;
    private readonly TabManager _tabManager;
    private Border? _titleBarContainer;

    public AppTabView()
    {
        _dispatcher = Dispatcher;
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
    }

    public TabRoot? TabRoot
    {
        get => (TabRoot?)GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    public UIElement? TitleBar
    {
        get => (UIElement?)GetValue(TitleBarProperty);
        set => SetValue(TitleBarProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _titleBarContainer = (Border?)GetTemplateChild(TitleBarContainerTemplateName);
        UpdateTitleBar();
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

    private static void OnTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTabView self = (AppTabView)d;
        self.UpdateTitleBar();
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

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // TODO add comment for this
        CloseButtonOverlayMode = TabViewCloseButtonOverlayMode.OnPointerOver;

        if (TabRoot is { } tabRoot)
        {
            Tab? tab = (Tab?)SelectedItem;
            tabRoot.SelectedTab = tab;

            if (tab is not null)
            {
                // TODO check should reload
            }
        }
    }

    private async void OnTabDroppedOutside(TabView sender, TabViewTabDroppedOutsideEventArgs args)
    {
        if (TabRoot is { Tabs.Count: > 1 } tabRoot && args.Item is Tab tab)
        {
            tabRoot.Tabs.Remove(tab);
            IAppWindow newWindow = await _appWindowManager.ShowNewWindowAsync();
            newWindow.Execute(window =>
            {
                // We can't just move the tab, because they have different TabRoot
                window.TabRoot.AddCloneOfTab(tab);
            });
        }
    }

    private async void OnTabRootSelectedTabChanged(object? sender, Tab? e)
    {
        await _dispatcher.SafeRunAsync(UpdateSelectedItem);
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

    private void OnTimeDateKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        args.Handled = true;
        if (SelectedItem is { } selectedItem
            && ContainerFromItem(selectedItem) is AppTabViewItem tabViewItem)
        {
            TimeDateCommand timeDateCommand = new()
            {
                TextBox = tabViewItem.TextBox
            };
            timeDateCommand.Execute(null);
        }
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = TabRoot?.SelectedTab;
    }

    private void UpdateTitleBar()
    {
        if (_titleBarContainer is { } titleBarContainer)
        {
            titleBarContainer.Child = TitleBar;
        }
    }
}