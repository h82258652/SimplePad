using System;
using Microsoft.Extensions.Hosting;

namespace SimplePad.Core.Modularity;

/// <summary>
/// The factory for creating application hosts with modular support.
/// </summary>
public static class ApplicationFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="IHostBuilder"/> with the specified root module.
    /// </summary>
    /// <typeparam name="TRootModule">The root module type.</typeparam>
    /// <param name="hostBuilderCreationDelegate">The delegate to create the <see cref="IHostBuilder"/> instance.</param>
    /// <returns>A <see cref="IHostBuilder"/> instance.</returns>
    public static IHostBuilder Create<TRootModule>(Func<IHostBuilder> hostBuilderCreationDelegate)
        where TRootModule : AppModuleBase, new()
    {
        return new AppHostBuilder(
            hostBuilderCreationDelegate(),
            new DependentDescriptor(typeof(TRootModule), () => new TRootModule())
        );
    }

    /// <summary>
    /// Creates a new instance of <see cref="HostApplicationBuilder"/> with the specified root module.
    /// </summary>
    /// <typeparam name="TRootModule">The root module type.</typeparam>
    /// <param name="hostBuilderCreationDelegate">The delegate to create the <see cref="HostApplicationBuilder"/> instance.</param>
    /// <returns>A <see cref="AppHostApplicationBuilder"/> instance.</returns>
    public static AppHostApplicationBuilder Create<TRootModule>(
        Func<HostApplicationBuilder> hostBuilderCreationDelegate
    )
        where TRootModule : AppModuleBase, new()
    {
        return new AppHostApplicationBuilder(
            hostBuilderCreationDelegate(),
            new DependentDescriptor(typeof(TRootModule), () => new TRootModule())
        );
    }
}
