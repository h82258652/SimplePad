using System;

namespace SimplePad.Core.Modularity;

/// <summary>
/// Provides context information during the application initialization phase.
/// </summary>
public sealed class ApplicationInitializationContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInitializationContext"/> class.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> instance.</param>
    internal ApplicationInitializationContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance.
    /// </summary>
    public IServiceProvider ServiceProvider { get; }
}
