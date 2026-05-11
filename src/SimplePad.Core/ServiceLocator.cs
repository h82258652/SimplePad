using System;
using System.Collections.Generic;

namespace SimplePad.Core;

/// <summary>
/// A static class that provides access to the <see cref="IServiceProvider"/> instance.
/// </summary>
public static class ServiceLocator
{
    private static readonly Dictionary<int, IServiceProvider> _providers = [];
    private static IServiceProvider? _globalProvider;
    private static IServiceProviderIdProvider? _idProvider;

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/> instance.
    /// </summary>
    public static IServiceProvider Current
    {
        get
        {
            if (_idProvider is not { } idProvider)
            {
                throw new InvalidOperationException("The service provider ID provider has not been set.");
            }

            int? providerId = idProvider.Get();
            if (providerId.HasValue)
            {
                if (_providers.TryGetValue(providerId.Value, out IServiceProvider? serviceProvider))
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

    public static void SetIdProvider(IServiceProviderIdProvider idProvider)
    {
        _idProvider = idProvider;
    }

    /// <summary>
    /// Sets the <see cref="IServiceProvider"/> instance.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance.</param>
    public static void SetLocatorProvider(IServiceProvider serviceProvider)
    {
        if (_idProvider is not { } idProvider)
        {
            throw new InvalidOperationException("The service provider ID provider has not been set.");
        }

        int? providerId = idProvider.Get();
        if (providerId.HasValue)
        {
            if (!_providers.TryAdd(providerId.Value, serviceProvider))
            {
                throw new InvalidOperationException(
                    "The service provider has already been set and cannot be changed."
                );
            }
        }
        else
        {
            if (_globalProvider is not null)
            {
                throw new InvalidOperationException(
                    "The service provider has already been set and cannot be changed."
                );
            }

            _globalProvider = serviceProvider;
        }
    }
}