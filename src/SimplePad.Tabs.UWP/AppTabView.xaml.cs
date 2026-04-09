using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

    private Border? _titleBarContainer;

    public AppTabView()
    {
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
            oldTabRoot.SelectedTabChanged -= self.OnTabRootSelectedTabChanged;
        }

        TabRoot? newTabRoot = (TabRoot?)e.NewValue;
        if (newTabRoot is not null)
        {
            newTabRoot.SelectedTabChanged += self.OnTabRootSelectedTabChanged;
        }

        self.UpdateSelectedItem();
    }

    private static void OnTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTabView self = (AppTabView)d;
        self.UpdateTitleBar();
    }

    private void OnAddTabButtonClick(TabView sender, object args)
    {
        TabRoot?.AddBlankTab();
    }

    private void OnTabRootSelectedTabChanged(object? sender, Tab? e)
    {
        UpdateSelectedItem();
    }

    private void OnTabViewTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        if (args.Item is Tab tab)
        {
            // TODO
            //tab.Close();
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