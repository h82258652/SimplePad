using System;
using System.Collections.Generic;

namespace SimplePad.Core;

/// <summary>
/// A static class that provides access to the <see cref="IServiceProvider"/> instance.
/// </summary>
public static class ServiceLocator
{
    private static readonly Dictionary<object, IServiceProvider> _scopedProviders = [];
    private static IServiceProvider? _globalProvider;
    private static IServiceScopeIdProvider? _scopeIdProvider;

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/> instance.
    /// </summary>
    public static IServiceProvider Current
    {
        get
        {
            if (_scopeIdProvider is not { } idProvider)
            {
                throw new InvalidOperationException("The service provider ID provider has not been set.");
            }

            object? providerId = idProvider.Get();
            if (providerId is not null)
            {
                if (_scopedProviders.TryGetValue(providerId, out IServiceProvider? serviceProvider))
                {
                    return serviceProvider;
                }
            }
            else
            {
                if (_globalProvider is not null)
                {
                    return _globalProvider;
                }
            }

            throw new InvalidOperationException(
                "The service provider has not been set. Please ensure that SetLocatorProvider is called during application view initialization."
            );
        }
    }

    public static void SetGlobalLocatorProvider(IServiceProvider serviceProvider)
    {
        if (_globalProvider is not null)
        {
            throw new InvalidOperationException(
                "The service provider has already been set and cannot be changed."
            );
        }

        _globalProvider = serviceProvider;
    }

    /// <summary>
    /// Sets the <see cref="IServiceProvider"/> instance.
    /// </summary>
    /// <param name="scopeId">TODO</param>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance.</param>
    public static void SetScopedLocatorProvider(object scopeId, IServiceProvider serviceProvider)
    {
        if (!_scopedProviders.TryAdd(scopeId, serviceProvider))
        {
            throw new InvalidOperationException(
                "The service provider has already been set and cannot be changed."
            );
        }
    }

    public static void SetScopeIdProvider(IServiceScopeIdProvider idProvider)
    {
        if (_scopeIdProvider is not null)
        {
            throw new InvalidOperationException("The scope id provider has been set");
        }

        _scopeIdProvider = idProvider;
    }
}