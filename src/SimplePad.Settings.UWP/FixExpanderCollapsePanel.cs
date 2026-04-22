using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Settings;

public sealed partial class FixExpanderCollapsePanel : Panel
{
    public static readonly DependencyProperty IsLayoutSpaceOccupiedProperty = DependencyProperty.Register(
        nameof(IsLayoutSpaceOccupied),
        typeof(bool),
        typeof(FixExpanderCollapsePanel),
        new PropertyMetadata(true, OnIsLayoutSpaceOccupiedChanged));

    public bool IsLayoutSpaceOccupied
    {
        get => (bool)GetValue(IsLayoutSpaceOccupiedProperty);
        set => SetValue(IsLayoutSpaceOccupiedProperty, value);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        foreach (UIElement child in Children)
        {
            child.Arrange(new Rect(0, 0, finalSize.Width, child.DesiredSize.Height));
        }

        return finalSize;
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        double desiredWidth = 0;
        double desiredHeight = 0;
        foreach (UIElement child in Children)
        {
            child.Measure(availableSize);
            desiredWidth = Math.Max(desiredWidth, child.DesiredSize.Width);
            desiredHeight = Math.Max(desiredHeight, child.DesiredSize.Height);
        }

        return IsLayoutSpaceOccupied ? new Size(desiredWidth, desiredHeight) : new Size(desiredWidth, 0);
    }

    private static void OnIsLayoutSpaceOccupiedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        FixExpanderCollapsePanel self = (FixExpanderCollapsePanel)d;
        self.InvalidateMeasure();
    }
}