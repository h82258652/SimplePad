using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace SimplePad.Settings;

[ContentProperty(Name = nameof(Child))]
[TemplatePart(Name = ContentContainerTemplateName, Type = typeof(Border))]
public sealed partial class FixExpanderCollapseControl : Control
{
    public static readonly DependencyProperty ChildProperty = DependencyProperty.Register(
        nameof(Child),
        typeof(UIElement),
        typeof(FixExpanderCollapseControl),
        null);

    public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(
        nameof(IsVisible),
        typeof(bool),
        typeof(FixExpanderCollapseControl),
        new PropertyMetadata(true, OnIsVisibleChanged));

    private const string ContentContainerTemplateName = "PART_ContentContainer";

    private Border? _contentContainer;

    public FixExpanderCollapseControl()
    {
        DefaultStyleKey = typeof(FixExpanderCollapseControl);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Settings.UWP/FixExpanderCollapseControl.xaml");
    }

    public UIElement? Child
    {
        get => (UIElement?)GetValue(ChildProperty);
        set => SetValue(ChildProperty, value);
    }

    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        if (_contentContainer is { } contentContainer)
        {
            contentContainer.Arrange(new Rect(0, 0, finalSize.Width, contentContainer.DesiredSize.Height));
        }

        return finalSize;
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        if (_contentContainer is { } contentContainer)
        {
            contentContainer.Measure(new Size(availableSize.Width, double.PositiveInfinity));
            return IsVisible ? contentContainer.DesiredSize : new Size(contentContainer.DesiredSize.Width, 0);
        }

        return new Size(0, 0);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _contentContainer = (Border)GetTemplateChild(ContentContainerTemplateName);

        InvalidateMeasure();
    }

    private static void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        FixExpanderCollapseControl self = (FixExpanderCollapseControl)d;
        self.InvalidateMeasure();
    }
}