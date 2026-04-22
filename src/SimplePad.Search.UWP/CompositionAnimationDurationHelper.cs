using System;

namespace SimplePad.Search;

/// <summary>
/// A helper class for validating a <see cref="TimeSpan"/> can be set to <see cref="KeyFrameAnimation.Duration"/>.
/// </summary>
internal static class CompositionAnimationDurationHelper
{
    /// <summary>
    /// Validates the specified <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="animationDuration">The value.</param>
    /// <returns>True if the duration is valid; otherwise, false.</returns>
    public static bool IsValidAnimationDuration(TimeSpan animationDuration)
    {
        return animationDuration >= TimeSpan.FromMilliseconds(1) && animationDuration <= TimeSpan.FromDays(24);
    }
}
