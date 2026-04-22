using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace SimplePad.Search;

/// <summary>
/// Provides a <see cref="CompositionColorBrush"/> instance.
/// </summary>
internal sealed class CompositionColorBrushProvider : DependencyObject, ICompositionBrushProvider
{
    /// <summary>
    /// Identifies the <see cref="Color"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
        nameof(Color),
        typeof(Color),
        typeof(CompositionColorBrushProvider),
        new PropertyMetadata(default(Color), OnColorChanged));

    private readonly CompositionColorBrush _colorBrush;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositionColorBrushProvider"/> class.
    /// </summary>
    public CompositionColorBrushProvider()
    {
        Compositor compositor = Window.Current.Compositor;
        _colorBrush = compositor.CreateColorBrush(Color);
    }

    /// <summary>
    /// Gets or sets the color.
    /// </summary>
    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <inheritdoc/>
    public CompositionBrush? GetBrush()
    {
        return _colorBrush;
    }

    /// <inheritdoc/>
    public void OnConnected(FrameworkElement element)
    {
    }

    /// <inheritdoc/>
    public void OnDisconnected(FrameworkElement element)
    {
    }

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        CompositionColorBrushProvider self = (CompositionColorBrushProvider)d;
        Color color = (Color)e.NewValue;
        self._colorBrush.Color = color;
    }
}
