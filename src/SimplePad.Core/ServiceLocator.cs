using System;
using System.Collections.Generic;

namespace SimplePad.Core;

/// <summary>
/// A static class that provides access to the <see cref="IServiceProvider"/> instance.
/// </summary>
public static class ServiceLocator
{
    private static readonly Dictionary<int, IServiceProvider> _providers = [];

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/> instance.
    /// </summary>
    public static IServiceProvider Current
    {
        get
        {
            if (_providers.TryGetValue(Environment.CurrentManagedThreadId, out IServiceProvider? serviceProvider))
            {
                return serviceProvider;
            }

            throw new InvalidOperationException(
                "The service provider has not been set. Please ensure that SetLocatorProvider is called during application view initialization."
            );
        }
    }

    /// <summary>
    /// Sets the <see cref="IServiceProvider"/> instance.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance.</param>
    public static void SetLocatorProvider(IServiceProvider serviceProvider)
    {
        int currentThreadId = Environment.CurrentManagedThreadId;
        if (!_providers.TryAdd(currentThreadId, serviceProvider))
        {
            throw new InvalidOperationException(
                "The service provider has already been set and cannot be changed."
            );
        }
    }
}
