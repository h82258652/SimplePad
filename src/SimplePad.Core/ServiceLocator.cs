using System;

namespace SimplePad.Core;

/// <summary>
/// A static class that provides access to the <see cref="IServiceProvider"/> instance.
/// </summary>
public static class ServiceLocator
{
    private static IServiceProvider? _currentProvider;

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/> instance.
    /// </summary>
    public static IServiceProvider Current
    {
        get
        {
            if (_currentProvider is null)
            {
                throw new InvalidOperationException(
                    "The service provider has not been set. Please ensure that SetLocatorProvider is called during application initialization."
                );
            }

            return _currentProvider;
        }
    }

    /// <summary>
    /// Sets the <see cref="IServiceProvider"/> instance.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance.</param>
    public static void SetLocatorProvider(IServiceProvider serviceProvider)
    {
        if (_currentProvider is not null)
        {
            throw new InvalidOperationException(
                "The service provider has already been set and cannot be changed."
            );
        }

        _currentProvider = serviceProvider;
    }
}
