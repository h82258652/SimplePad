using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SimplePad.MultiTab.UWP.Controls;

public sealed partial class AppTabViewItem : TabViewItem
{
    public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register(
        nameof(IsModified),
        typeof(bool),
        typeof(AppTabViewItem),
        new PropertyMetadata(false, OnIsModifiedChanged));

    private const string CommonStatesGroupName = "CommonStates";

    private const string LayoutRootTemplateName = "LayoutRoot";

    private const string PointerOverSelectedStateName = "PointerOverSelected";

    private const string PointerOverStateName = "PointerOver";

    private const string PressedSelectedStateName = "PressedSelected";

    private const string PressedStateName = "Pressed";

    private static readonly DependencyProperty ModifiedIndicatorVisibilityProperty = DependencyProperty.Register(
        nameof(ModifiedIndicatorVisibility),
        typeof(Visibility),
        typeof(AppTabViewItem),
        new PropertyMetadata(Visibility.Collapsed));

    private VisualStateGroup? _commonStates;

    public AppTabViewItem()
    {
        DefaultStyleKey = typeof(AppTabViewItem);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.MultiTab.UWP/Controls/AppTabViewItem.xaml");
    }

    public bool IsModified
    {
        get => (bool)GetValue(IsModifiedProperty);
        set => SetValue(IsModifiedProperty, value);
    }

    private Visibility ModifiedIndicatorVisibility
    {
        get => (Visibility)GetValue(ModifiedIndicatorVisibilityProperty);
        set => SetValue(ModifiedIndicatorVisibilityProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        FrameworkElement layoutRoot = (FrameworkElement)GetTemplateChild(LayoutRootTemplateName);
        IList<VisualStateGroup> visualStateGroups = VisualStateManager.GetVisualStateGroups(layoutRoot);
        _commonStates = visualStateGroups.FirstOrDefault(visualStateGroup => visualStateGroup.Name == CommonStatesGroupName);
        if (_commonStates is not null)
        {
            UpdateModifiedIndicatorVisibility();

            _commonStates.CurrentStateChanged -= OnCommonStatesCurrentStateChanged;
            _commonStates.CurrentStateChanged += OnCommonStatesCurrentStateChanged;
        }
    }

    private static void OnIsModifiedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTabViewItem self = (AppTabViewItem)d;
        self.UpdateModifiedIndicatorVisibility();
    }

    private void OnCommonStatesCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
    {
        UpdateModifiedIndicatorVisibility();
    }

    private void UpdateModifiedIndicatorVisibility()
    {
        if (_commonStates is null)
        {
            ModifiedIndicatorVisibility = Visibility.Collapsed;
            return;
        }

        if (_commonStates.CurrentState?.Name is PointerOverStateName or PressedStateName or PointerOverSelectedStateName or PressedSelectedStateName)
        {
            ModifiedIndicatorVisibility = Visibility.Collapsed;
            return;
        }

        if (IsModified)
        {
            ModifiedIndicatorVisibility = Visibility.Visible;
        }
        else
        {
            ModifiedIndicatorVisibility = Visibility.Collapsed;
        }
    }
}