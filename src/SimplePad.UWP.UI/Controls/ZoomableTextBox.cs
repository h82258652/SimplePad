using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Controls;

public sealed partial class ZoomableTextBox : TextBox
{
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

    private static void OnZoomFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ZoomableTextBox self = (ZoomableTextBox)d;
        self.ComputedFontSize = self.FontSize * self.ZoomFactor;
    }

    private void OnFontSizeChanged(DependencyObject sender, DependencyProperty dp)
    {
        ComputedFontSize = FontSize * ZoomFactor;
    }
}
