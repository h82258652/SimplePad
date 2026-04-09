using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
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
    private VisualStateGroup? _commonStates;
    private UIElement? _modifiedIndicator;

    public AppTabViewItem()
    {
        InitializeComponent();
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

    private void OnTabContentChanged(object? sender, string e)
    {
        UpdateTextBox();
    }

    private void OnTabIsModifiedChanged(object? sender, bool e)
    {
        UpdateModifiedIndicatorVisibility();
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
}