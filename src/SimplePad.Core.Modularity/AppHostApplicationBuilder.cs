using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SimplePad.Core.Modularity;

/// <summary>
/// Provides a builder for configuring and creating an application host with modular support.
/// </summary>
public sealed class AppHostApplicationBuilder : IHostApplicationBuilder
{
    private readonly HostApplicationBuilder _hostApplicationBuilder;
    private readonly IReadOnlyList<AppModuleBase> _modules;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppHostApplicationBuilder"/> class.
    /// </summary>
    /// <param name="hostApplicationBuilder">The <see cref="HostApplicationBuilder"/> instance.</param>
    /// <param name="rootModuleDescriptor">The dependent descriptor for the root module.</param>
    internal AppHostApplicationBuilder(HostApplicationBuilder hostApplicationBuilder, DependentDescriptor rootModuleDescriptor)
    {
        _hostApplicationBuilder = hostApplicationBuilder;

        ModuleResolver moduleResolver = new(rootModuleDescriptor);
        _modules = moduleResolver.Resolve();

        ServiceConfigurationContext serviceConfigurationContext = new(_hostApplicationBuilder.Configuration, _hostApplicationBuilder.Environment, _hostApplicationBuilder.Services);
        foreach (AppModuleBase module in _modules)
        {
            module.ConfigureServices(serviceConfigurationContext);
        }
    }

    /// <inheritdoc/>
    public IConfigurationManager Configuration => _hostApplicationBuilder.Configuration;

    /// <inheritdoc/>
    public IHostEnvironment Environment => _hostApplicationBuilder.Environment;

    /// <inheritdoc/>
    public ILoggingBuilder Logging => _hostApplicationBuilder.Logging;

    /// <inheritdoc/>
    public IMetricsBuilder Metrics => _hostApplicationBuilder.Metrics;

    /// <inheritdoc/>
    public IDictionary<object, object> Properties => ((IHostApplicationBuilder)_hostApplicationBuilder).Properties;

    /// <inheritdoc/>
    public IServiceCollection Services => _hostApplicationBuilder.Services;

    /// <inheritdoc cref="HostApplicationBuilder.Build"/>
    public IHost Build()
    {
        return new AppHost(_hostApplicationBuilder.Build(), _modules);
    }

    /// <inheritdoc/>
    public void ConfigureContainer<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory, Action<TContainerBuilder>? configure = null) where TContainerBuilder : notnull
    {
        _hostApplicationBuilder.ConfigureContainer(factory, configure);
    }
}
