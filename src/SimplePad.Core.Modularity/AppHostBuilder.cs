using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimplePad.Core.Modularity;

/// <summary>
/// Provides a builder for configuring and creating an application host with modular support.
/// </summary>
internal sealed class AppHostBuilder : IHostBuilder
{
    private readonly IHostBuilder _hostBuilder;
    private readonly IReadOnlyList<AppModuleBase> _modules;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppHostBuilder"/> class.
    /// </summary>
    /// <param name="hostBuilder">The <see cref="IHostBuilder"/> instance.</param>
    /// <param name="rootModuleDescriptor">The dependent descriptor for the root module.</param>
    internal AppHostBuilder(IHostBuilder hostBuilder, DependentDescriptor rootModuleDescriptor)
    {
        _hostBuilder = hostBuilder;

        ModuleResolver moduleResolver = new(rootModuleDescriptor);
        _modules = moduleResolver.Resolve();

        hostBuilder.ConfigureServices(
            (context, services) =>
            {
                ServiceConfigurationContext serviceConfigurationContext = new(
                    context.Configuration,
                    context.HostingEnvironment,
                    services
                );

                foreach (AppModuleBase module in _modules)
                {
                    module.ConfigureServices(serviceConfigurationContext);
                }
            }
        );
    }

    /// <inheritdoc/>
    public IDictionary<object, object> Properties => _hostBuilder.Properties;

    /// <inheritdoc/>
    public IHost Build()
    {
        return _hostBuilder.Build();
    }

    /// <inheritdoc/>
    public IHostBuilder ConfigureAppConfiguration(
        Action<HostBuilderContext, IConfigurationBuilder> configureDelegate
    )
    {
        return _hostBuilder.ConfigureAppConfiguration(configureDelegate);
    }

    /// <inheritdoc/>
    public IHostBuilder ConfigureContainer<TContainerBuilder>(
        Action<HostBuilderContext, TContainerBuilder> configureDelegate
    )
    {
        return _hostBuilder.ConfigureContainer(configureDelegate);
    }

    /// <inheritdoc/>
    public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
    {
        return _hostBuilder.ConfigureHostConfiguration(configureDelegate);
    }

    /// <inheritdoc/>
    public IHostBuilder ConfigureServices(
        Action<HostBuilderContext, IServiceCollection> configureDelegate
    )
    {
        return _hostBuilder.ConfigureServices(configureDelegate);
    }

    /// <inheritdoc/>
    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(
        IServiceProviderFactory<TContainerBuilder> factory
    )
        where TContainerBuilder : notnull
    {
        return _hostBuilder.UseServiceProviderFactory(factory);
    }

    /// <inheritdoc/>
    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(
        Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory
    )
        where TContainerBuilder : notnull
    {
        return _hostBuilder.UseServiceProviderFactory(factory);
    }
}
