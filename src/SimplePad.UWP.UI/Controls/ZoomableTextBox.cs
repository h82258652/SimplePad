using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace SimplePad.UWP.UI.Controls;

public sealed partial class ZoomableTextBox : TextBox
{
    public static readonly DependencyProperty MaxZoomFactorProperty = DependencyProperty.Register(
        nameof(MaxZoomFactor),
        typeof(double),
        typeof(ZoomableTextBox),
        new PropertyMetadata(5d, OnMaxZoomFactorChanged));

    public static readonly DependencyProperty MinZoomFactorProperty = DependencyProperty.Register(
        nameof(MinZoomFactor),
        typeof(double),
        typeof(ZoomableTextBox),
        new PropertyMetadata(0.1d, OnMinZoomFactorChanged));

    public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register(
        nameof(ZoomFactor),
        typeof(double),
        typeof(ZoomableTextBox),
        new PropertyMetadata(1d, OnZoomFactorChanged));

    private static readonly DependencyProperty ComputedFontSizeProperty = DependencyProperty.Register(
        nameof(ComputedFontSize),
        typeof(double),
        typeof(ZoomableTextBox),
        new PropertyMetadata(14d));

    public ZoomableTextBox()
    {
        DefaultStyleKey = typeof(ZoomableTextBox);

        RegisterPropertyChangedCallback(FontSizeProperty, OnFontSizeChanged);

        ComputedFontSize = FontSize * ZoomFactor;
    }

    public double MaxZoomFactor
    {
        get => (double)GetValue(MaxZoomFactorProperty);
        set => SetValue(MaxZoomFactorProperty, value);
    }

    public double MinZoomFactor
    {
        get => (double)GetValue(MinZoomFactorProperty);
        set => SetValue(MinZoomFactorProperty, value);
    }

    public double ZoomFactor
    {
        get => (double)GetValue(ZoomFactorProperty);
        set => SetValue(ZoomFactorProperty, value);
    }

    private double ComputedFontSize
    {
        get => (double)GetValue(ComputedFontSizeProperty);
        set => SetValue(ComputedFontSizeProperty, value);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ScrollViewer scrollViewer = (ScrollViewer)GetTemplateChild("ContentElement");
        scrollViewer.PointerWheelChanged -= OnContentElementPointerWheelChanged;
        scrollViewer.PointerWheelChanged += OnContentElementPointerWheelChanged;
    }

    private static void OnMaxZoomFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ZoomableTextBox self = (ZoomableTextBox)d;
        double maxZoomFactor = (double)e.NewValue;
        self.ZoomFactor = Math.Min(self.ZoomFactor, maxZoomFactor);
    }

    private static void OnMinZoomFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ZoomableTextBox self = (ZoomableTextBox)d;
        double minZoomFactor = (double)e.NewValue;
        self.ZoomFactor = Math.Max(self.ZoomFactor, minZoomFactor);
    }

    private static void OnZoomFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ZoomableTextBox self = (ZoomableTextBox)d;
        self.ComputedFontSize = self.FontSize * self.ZoomFactor;
    }

    private void OnContentElementPointerWheelChanged(object sender, PointerRoutedEventArgs e)
    {
        if (e.KeyModifiers.HasFlag(VirtualKeyModifiers.Control))
        {
            e.Handled = true;

            int mouseWheelDelta = e.GetCurrentPoint(null).Properties.MouseWheelDelta;
            if (mouseWheelDelta > 0)
            {
                ZoomFactor = Math.Min(MaxZoomFactor, ZoomFactor + 0.1);
            }
            else if (mouseWheelDelta < 0)
            {
                ZoomFactor = Math.Max(MinZoomFactor, ZoomFactor - 0.1);
            }
        }
    }

    private void OnFontSizeChanged(DependencyObject sender, DependencyProperty dp)
    {
        ComputedFontSize = FontSize * ZoomFactor;
    }
}
