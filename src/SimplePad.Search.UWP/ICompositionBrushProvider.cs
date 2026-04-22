using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace SimplePad.Search;

/// <summary>
/// Provides a <see cref="CompositionBrush"/> instance.
/// </summary>
internal interface ICompositionBrushProvider
{
    /// <summary>
    /// Gets the <see cref="CompositionBrush"/> instance.
    /// </summary>
    /// <returns>The <see cref="CompositionBrush"/> instance.</returns>
    CompositionBrush? GetBrush();

    /// <summary>
    /// Executes when this brush is connected to an element.
    /// </summary>
    /// <param name="element">The element.</param>
    void OnConnected(FrameworkElement element);

    /// <summary>
    /// Executes when this brush is disconnected from an element.
    /// </summary>
    /// <param name="element">The element.</param>
    void OnDisconnected(FrameworkElement element);
}
