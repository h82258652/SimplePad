using System;

namespace SimplePad.Search;

/// <summary>
/// Provides the geometry info based on the <see cref="AnimatedBackgroundContentControl"/>.
/// </summary>
internal sealed class AnimatedBackgroundContentControlGeometryInfoContext : IGeometryInfoContext
{
    private readonly AnimatedBackgroundContentControl _control;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnimatedBackgroundContentControlGeometryInfoContext"/> class.
    /// </summary>
    /// <param name="control">The <see cref="AnimatedBackgroundContentControl"/> instance.</param>
    internal AnimatedBackgroundContentControlGeometryInfoContext(AnimatedBackgroundContentControl control)
    {
        _control = control;
    }

    /// <inheritdoc/>
    public double Height => Math.Max(_control.ActualHeight - _control.StrokeThickness * 2, 0);

    /// <inheritdoc/>
    public double OffsetX => _control.StrokeThickness;

    /// <inheritdoc/>
    public double OffsetY => _control.StrokeThickness;

    /// <inheritdoc/>
    public TimeSpan ResizeAnimationDuration => _control.ResizeAnimationDuration;

    /// <inheritdoc/>
    public double Width => Math.Max(_control.ActualWidth - _control.StrokeThickness * 2, 0);
}
