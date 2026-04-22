using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace SimplePad.Search;

/// <summary>
/// Provides a <see cref="CompositionRoundedRectangleGeometry"/> instance.
/// </summary>
internal sealed class CompositionRoundedRectangleGeometryProvider : DependencyObject, ICompositionGeometryProvider
{
    /// <summary>
    /// Identifies the <see cref="CornerRadiusAnimationDuration"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusAnimationDurationProperty = DependencyProperty.Register(
        nameof(CornerRadiusAnimationDuration),
        typeof(TimeSpan),
        typeof(CompositionRoundedRectangleGeometryProvider),
        new PropertyMetadata(TimeSpan.FromSeconds(1)));

    /// <summary>
    /// Identifies the <see cref="CornerRadius"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
        nameof(CornerRadius),
        typeof(double),
        typeof(CompositionRoundedRectangleGeometryProvider),
        new PropertyMetadata(0d, OnCornerRadiusChanged));

    private readonly Compositor _compositor;
    private readonly CompositionRoundedRectangleGeometry _roundedRectangleGeometry;
    private bool _isFirstSetCornerRadius = true;
    private bool _isSetNonZeroSize;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositionRoundedRectangleGeometryProvider"/> class.
    /// </summary>
    public CompositionRoundedRectangleGeometryProvider()
    {
        _compositor = Window.Current.Compositor;
        _roundedRectangleGeometry = _compositor.CreateRoundedRectangleGeometry();
    }

    /// <summary>
    /// Gets or sets the corner radius.
    /// </summary>
    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the duration of the animation when the corner radius changes.
    /// </summary>
    public TimeSpan CornerRadiusAnimationDuration
    {
        get => (TimeSpan)GetValue(CornerRadiusAnimationDurationProperty);
        set => SetValue(CornerRadiusAnimationDurationProperty, value);
    }

    /// <inheritdoc/>
    public IGeometryInfoContext? GeometryInfoContext { get; set; }

    /// <inheritdoc/>
    public CompositionGeometry GetGeometry()
    {
        return _roundedRectangleGeometry;
    }

    /// <inheritdoc/>
    public void UpdateGeometry()
    {
        if (GeometryInfoContext is not { } geometryInfoContext)
        {
            return;
        }

        _roundedRectangleGeometry.Offset = new Vector2((float)geometryInfoContext.OffsetX, (float)geometryInfoContext.OffsetY);

        if (CompositionAnimationDurationHelper.IsValidAnimationDuration(geometryInfoContext.ResizeAnimationDuration) && _isSetNonZeroSize)
        {
            Vector2KeyFrameAnimation animation = _compositor.CreateVector2KeyFrameAnimation();
            animation.InsertKeyFrame(1f, new Vector2((float)geometryInfoContext.Width, (float)geometryInfoContext.Height));
            animation.Duration = geometryInfoContext.ResizeAnimationDuration;

            _roundedRectangleGeometry.StartAnimation(nameof(CompositionRoundedRectangleGeometry.Size), animation);
        }
        else
        {
            _roundedRectangleGeometry.Size = new Vector2((float)geometryInfoContext.Width, (float)geometryInfoContext.Height);
        }

        if (geometryInfoContext.Width > 0 && geometryInfoContext.Height > 0)
        {
            _isSetNonZeroSize = true;
        }
    }

    private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        CompositionRoundedRectangleGeometryProvider self = (CompositionRoundedRectangleGeometryProvider)d;
        double radius = (double)e.NewValue;

        if (CompositionAnimationDurationHelper.IsValidAnimationDuration(self.CornerRadiusAnimationDuration) && !self._isFirstSetCornerRadius)
        {
            Vector2KeyFrameAnimation animation = self._compositor.CreateVector2KeyFrameAnimation();
            animation.InsertKeyFrame(1f, new Vector2((float)radius));
            animation.Duration = self.CornerRadiusAnimationDuration;

            self._roundedRectangleGeometry.StartAnimation(nameof(self._roundedRectangleGeometry.CornerRadius), animation);
        }
        else
        {
            self._roundedRectangleGeometry.CornerRadius = new Vector2((float)radius);
            self._isFirstSetCornerRadius = false;
        }
    }
}
