using System;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SimplePad.UWP.UI.Controls;

public sealed partial class AppTabViewItem : TabViewItem
{
    public static readonly DependencyProperty IsModifiedProperty = DependencyProperty.Register(
        nameof(IsModified),
        typeof(bool),
        typeof(AppTabViewItem),
        new PropertyMetadata(false, OnIsModifiedChanged)
    );

    public static readonly DependencyProperty ModifiedIndicatorVisibilityProperty = DependencyProperty.Register(
        nameof(ModifiedIndicatorVisibility),
        typeof(Visibility),
        typeof(AppTabViewItem),
        new PropertyMetadata(Visibility.Collapsed));

    private FrameworkElement? _layoutRoot;

    public AppTabViewItem()
    {
        DefaultStyleKey = typeof(AppTabViewItem);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.UWP.UI/Controls/AppTabViewItem.xaml"
        );
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

        _layoutRoot = (FrameworkElement)GetTemplateChild("LayoutRoot");

        var groups = VisualStateManager.GetVisualStateGroups(_layoutRoot);
        var g = groups.FirstOrDefault(temp => temp.Name == "CommonStates");
        if (g != null)
        {
            UpdateModifiedIndicatorVisibility();
            g.CurrentStateChanged += G_CurrentStateChanged;
        }
    }

    private static void OnIsModifiedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTabViewItem self = (AppTabViewItem)d;
        self.UpdateModifiedIndicatorVisibility();
    }

    private void G_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
    {
        UpdateModifiedIndicatorVisibility();
    }

    private void UpdateModifiedIndicatorVisibility()
    {
        var groups = VisualStateManager.GetVisualStateGroups(_layoutRoot);
        var g = groups.FirstOrDefault(temp => temp.Name == "CommonStates");
        if (g != null)
        {
            if (g.CurrentState.Name == "PointerOver" || g.CurrentState.Name == "Pressed" || g.CurrentState.Name == "PointerOverSelected" || g.CurrentState.Name == "PressedSelected")
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
}