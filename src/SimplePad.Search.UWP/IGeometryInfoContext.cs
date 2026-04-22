using System;

namespace SimplePad.Search;

/// <summary>
/// Provides the geometry info.
/// </summary>
internal interface IGeometryInfoContext
{
    /// <summary>
    /// Gets the height of the geometry.
    /// </summary>
    double Height { get; }

    /// <summary>
    /// Gets the left offset of the geometry.
    /// </summary>
    double OffsetX { get; }

    /// <summary>
    /// Gets the top offset of the geometry.
    /// </summary>
    double OffsetY { get; }

    /// <summary>
    /// Gets the duration of the resize animation.
    /// </summary>
    TimeSpan ResizeAnimationDuration { get; }

    /// <summary>
    /// Gets the width of the geometry.
    /// </summary>
    double Width { get; }
}
