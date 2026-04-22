using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace SimplePad.Search;

/// <summary>
/// Provides a <see cref="DropShadow"/> instance.
/// </summary>
internal sealed class CompositionDropShadowProvider : DependencyObject, ICompositionShadowProvider
{
    /// <summary>
    /// Identifies the <see cref="BlurRadius"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BlurRadiusProperty = DependencyProperty.Register(
        nameof(BlurRadius),
        typeof(double),
        typeof(CompositionDropShadowProvider),
        new PropertyMetadata(9d, OnBlurRadiusChanged));

    /// <summary>
    /// Identifies the <see cref="Color"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
        nameof(Color),
        typeof(Color),
        typeof(CompositionDropShadowProvider),
        new PropertyMetadata(Colors.Black, OnColorChanged));

    /// <summary>
    /// Identifies the <see cref="Offset"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
        nameof(Offset),
        typeof(Point),
        typeof(CompositionDropShadowProvider),
        new PropertyMetadata(new Point(0, 0), OnOffsetChanged));

    /// <summary>
    /// Identifies the <see cref="Opacity"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OpacityProperty = DependencyProperty.Register(
        nameof(Opacity),
        typeof(double),
        typeof(CompositionDropShadowProvider),
        new PropertyMetadata(1d, OnOpacityChanged));

    private readonly DropShadow _dropShadow;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositionDropShadowProvider"/> class.
    /// </summary>
    public CompositionDropShadowProvider()
    {
        Compositor compositor = Window.Current.Compositor;
        _dropShadow = compositor.CreateDropShadow();
        _dropShadow.Opacity = (float)Opacity;
        _dropShadow.Offset = new Vector3(Offset.ToVector2(), 0);
        _dropShadow.Color = Color;
        _dropShadow.BlurRadius = (float)BlurRadius;
    }

    /// <summary>
    /// Gets or sets the Gaussian blur of the shadow.
    /// </summary>
    public double BlurRadius
    {
        get => (double)GetValue(BlurRadiusProperty);
        set => SetValue(BlurRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the color of the shadow.
    /// </summary>
    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the offset of the shadow.
    /// </summary>
    public Point Offset
    {
        get => (Point)GetValue(OffsetProperty);
        set => SetValue(OffsetProperty, value);
    }

    /// <summary>
    /// Gets or sets the opacity of the shadow.
    /// </summary>
    public double Opacity
    {
        get => (double)GetValue(OpacityProperty);
        set => SetValue(OpacityProperty, value);
    }

    /// <inheritdoc/>
    public CompositionShadow? GetShadow()
    {
        return _dropShadow;
    }

    /// <inheritdoc/>
    public void SetMask(CompositionBrush? mask)
    {
        _dropShadow.Mask = mask;
    }

    private static void OnBlurRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        CompositionDropShadowProvider self = (CompositionDropShadowProvider)d;
        double blurRadius = (double)e.NewValue;
        self._dropShadow.BlurRadius = (float)blurRadius;
    }

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        CompositionDropShadowProvider self = (CompositionDropShadowProvider)d;
        Color color = (Color)e.NewValue;
        self._dropShadow.Color = color;
    }

    private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        CompositionDropShadowProvider self = (CompositionDropShadowProvider)d;
        Point offset = (Point)e.NewValue;
        self._dropShadow.Offset = new Vector3(offset.ToVector2(), 0);
    }

    private static void OnOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        CompositionDropShadowProvider self = (CompositionDropShadowProvider)d;
        double opacity = (double)e.NewValue;
        self._dropShadow.Opacity = (float)opacity;
    }
}
