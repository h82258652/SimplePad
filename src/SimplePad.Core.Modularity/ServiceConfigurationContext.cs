using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimplePad.Core.Modularity;

/// <summary>
/// Provides contextual information and services for configuring application modules.
/// </summary>
public sealed class ServiceConfigurationContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceConfigurationContext"/> class.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance.</param>
    /// <param name="hostEnvironment">The <see cref="IHostEnvironment"/> instance.</param>
    /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
    internal ServiceConfigurationContext(
        IConfiguration configuration,
        IHostEnvironment hostEnvironment,
        IServiceCollection services)
    {
        Configuration = configuration;
        HostEnvironment = hostEnvironment;
        Services = services;
    }

    /// <summary>
    /// Gets the <see cref="IConfiguration"/> instance.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Gets the <see cref="IHostEnvironment"/> instance.
    /// </summary>
    public IHostEnvironment HostEnvironment { get; }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> instance.
    /// </summary>
    public IServiceCollection Services { get; }
}
