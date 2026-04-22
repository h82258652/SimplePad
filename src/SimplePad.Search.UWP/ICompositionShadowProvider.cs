using Windows.UI.Composition;

namespace SimplePad.Search;

/// <summary>
/// Provides a <see cref="CompositionShadow"/> instance.
/// </summary>
internal interface ICompositionShadowProvider
{
    /// <summary>
    /// Gets the <see cref="CompositionShadow"/> instance.
    /// </summary>
    /// <returns>The <see cref="CompositionBrush"/> instance.</returns>
    CompositionShadow? GetShadow();

    /// <summary>
    /// Sets the mask of the shadow.
    /// </summary>
    /// <param name="mask">The <see cref="CompositionBrush"/> instance.</param>
    void SetMask(CompositionBrush? mask);
}
