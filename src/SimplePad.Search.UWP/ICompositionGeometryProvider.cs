using Windows.UI.Composition;

namespace SimplePad.Search;

/// <summary>
/// Provides a <see cref="CompositionGeometry"/> instance.
/// </summary>
internal interface ICompositionGeometryProvider
{
    /// <summary>
    /// Gets or sets the geometry info context.
    /// </summary>
    IGeometryInfoContext? GeometryInfoContext { get; set; }

    /// <summary>
    /// Gets the <see cref="CompositionGeometry"/> instance.
    /// </summary>
    /// <returns>The <see cref="CompositionGeometry"/> instance.</returns>
    CompositionGeometry GetGeometry();

    /// <summary>
    /// Updates the geometry.
    /// </summary>
    void UpdateGeometry();
}
