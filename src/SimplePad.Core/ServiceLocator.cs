using System;
using System.Collections.Generic;
using System.Threading;

namespace SimplePad.Core;

/// <summary>
/// A static class that provides access to the <see cref="IServiceProvider"/> instance.
/// </summary>
public static class ServiceLocator
{
    //private static IServiceProvider? _currentProvider;

    private static readonly Dictionary<int, IServiceProvider> xx = new Dictionary<int, IServiceProvider>();

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/> instance.
    /// </summary>
    public static IServiceProvider Current
    {
        get
        {
            if (xx.TryGetValue(Environment.CurrentManagedThreadId, out var xxx))
            {
                return xxx;
            }

            throw new InvalidOperationException(
                "The service provider has not been set. Please ensure that SetLocatorProvider is called during application initialization."
            );



            //if (_currentProvider is null)
            //{
            //    throw new InvalidOperationException(
            //        "The service provider has not been set. Please ensure that SetLocatorProvider is called during application initialization."
            //    );
            //}

            //return _currentProvider;
        }
    }

    //public static void SetLocatorProvider(IServiceProvider serviceProvider)
    //{

    //}

    /// <summary>
    /// Sets the <see cref="IServiceProvider"/> instance.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance.</param>
    public static void SetLocatorProvider(IServiceProvider serviceProvider)
    {
        if (xx.ContainsKey(Environment.CurrentManagedThreadId))
        {
            throw new InvalidOperationException(
                "The service provider has already been set and cannot be changed."
            );

        }

        xx.Add(Environment.CurrentManagedThreadId, serviceProvider);

        //if (_currentProvider is not null)
        //{
        //    throw new InvalidOperationException(
        //        "The service provider has already been set and cannot be changed."
        //    );
        //}

        //_currentProvider = serviceProvider;
    }
}
