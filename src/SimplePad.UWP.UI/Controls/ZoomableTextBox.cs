using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Controls;

public sealed partial class ZoomableTextBox : TextBox
{
    public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register(
        nameof(ZoomFactor),
        typeof(double),
        typeof(ZoomableTextBox),
        new PropertyMetadata(1d));

    public ZoomableTextBox()
    {
        DefaultStyleKey = typeof(ZoomableTextBox);
    }

    public double ZoomFactor
    {
        get => (double)GetValue(ZoomFactorProperty);
        set => SetValue(ZoomFactorProperty, value);
    }
}
