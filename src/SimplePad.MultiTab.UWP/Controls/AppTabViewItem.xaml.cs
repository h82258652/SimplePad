using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SimplePad.MultiTab.UWP.Controls;

public sealed partial class AppTabViewItem : TabViewItem
{
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

    private void OnCommonStatesCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
    {
        UpdateModifiedIndicatorVisibility();
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

        // TODO is modified to visible, otherwise to collapsed
    }
}