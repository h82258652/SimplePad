using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using SimplePad.Search;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Tabs;

public sealed partial class AppTabViewItem : TabViewItem
{
    public static readonly DependencyProperty TabProperty = DependencyProperty.Register(
        nameof(Tab),
        typeof(Tab),
        typeof(AppTabViewItem),
        new PropertyMetadata(null, OnTabChanged));

    private const string CommonStatesGroupName = "CommonStates";
    private const string LayoutRootTemplateName = "LayoutRoot";
    private const string ModifiedIndicatorTemplateName = "PART_ModifiedIndicator";
    private const string PointerOverSelectedStateName = "PointerOverSelected";
    private const string PointerOverStateName = "PointerOver";
    private const string PressedSelectedStateName = "PressedSelected";
    private const string PressedStateName = "Pressed";
    private readonly CoreDispatcher _dispatcher;
    private readonly SearchViewState _searchViewState;
    private VisualStateGroup? _commonStates;
    private UIElement? _modifiedIndicator;

    public AppTabViewItem()
    {
        _dispatcher = Dispatcher;
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();

        UpdateTextBoxPadding();

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
        RegisterPropertyChangedCallback(IsSelectedProperty, OnIsSelectedChanged);
    }

    public Tab? Tab
    {
        get => (Tab?)GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        FrameworkElement layoutRoot = (FrameworkElement)GetTemplateChild(LayoutRootTemplateName);
        _modifiedIndicator = (UIElement)GetTemplateChild(ModifiedIndicatorTemplateName);

        IList<VisualStateGroup> visualStateGroups = VisualStateManager.GetVisualStateGroups(layoutRoot);
        _commonStates = visualStateGroups.FirstOrDefault(visualStateGroup => visualStateGroup.Name == CommonStatesGroupName);
        if (_commonStates is not null)
        {
            UpdateModifiedIndicatorVisibility();

            _commonStates.CurrentStateChanged -= OnCommonStatesCurrentStateChanged;
            _commonStates.CurrentStateChanged += OnCommonStatesCurrentStateChanged;
        }

        Button closeButton = (Button)GetTemplateChild("CloseButton");
        ToolTipService.SetToolTip(closeButton, "Close tab (Ctrl+W)");
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
        }

        Tab? newTab = (Tab?)e.NewValue;
        if (newTab is not null)
        {
            newTab.TitleChanged += self.OnTabTitleChanged;
            newTab.IsModifiedChanged += self.OnTabIsModifiedChanged;
            newTab.ContentChanged += self.OnTabContentChanged;
        }

        self.UpdateHeader();
        self.UpdateModifiedIndicatorVisibility();
        self.UpdateTextBox();
    }

    private void OnCommonStatesCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
    {
        UpdateModifiedIndicatorVisibility();
    }

    private void OnIsSelectedChanged(DependencyObject sender, DependencyProperty dp)
    {
        if (IsSelected)
        {
            _searchViewState.TextBox = TextBox;
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

    private async void OnTabContentChanged(object? sender, string e)
    {
        await _dispatcher.SafeRunAsync(UpdateTextBox);
    }

    private async void OnTabIsModifiedChanged(object? sender, bool e)
    {
        await _dispatcher.SafeRunAsync(UpdateModifiedIndicatorVisibility);
    }

    private async void OnTabTitleChanged(object? sender, string e)
    {
        await _dispatcher.SafeRunAsync(UpdateHeader);
    }

    private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        if (Tab is { } tab)
        {
            // UWP's TextBox uses \r for new lines, for content, uses \r\n for new lines
            tab.Content = TextBox.Text.Replace("\r", "\r\n");
        }
    }

    private void UpdateHeader()
    {
        Header = Tab?.Title ?? TabConstants.DefaultTabTitle;
    }

    private void UpdateModifiedIndicatorVisibility()
    {
        if (_modifiedIndicator is not { } modifiedIndicator)
        {
            return;
        }

        if (_commonStates is not { } commonStates)
        {
            modifiedIndicator.Visibility = Visibility.Collapsed;
            return;
        }

        if (commonStates.CurrentState?.Name is PointerOverStateName or PressedStateName or PointerOverSelectedStateName or PressedSelectedStateName)
        {
            modifiedIndicator.Visibility = Visibility.Collapsed;
            return;
        }

        if (Tab is { IsModified: true })
        {
            modifiedIndicator.Visibility = Visibility.Visible;
        }
        else
        {
            modifiedIndicator.Visibility = Visibility.Collapsed;
        }
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