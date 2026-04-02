using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SimplePad.Core.Modularity;

/// <summary>
/// Represents the application host that manages the lifecycle of the application and its modules.
/// </summary>
internal sealed class AppHost : IHost
{
    private readonly IHost _host;
    private readonly IReadOnlyList<AppModuleBase> _modules;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppHost"/> class.
    /// </summary>
    /// <param name="host">The <see cref="IHost"/> instance.</param>
    /// <param name="modules">The list of application modules.</param>
    internal AppHost(IHost host, IReadOnlyList<AppModuleBase> modules)
    {
        _host = host;
        _modules = modules;
    }

    /// <inheritdoc/>
    public IServiceProvider Services => _host.Services;

    /// <inheritdoc/>
    public void Dispose()
    {
        _host.Dispose();
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        ApplicationInitializationContext context = new(Services);
        foreach (AppModuleBase module in _modules)
        {
            module.OnApplicationInitialization(context);
        }

        return _host.StartAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return _host.StopAsync(cancellationToken);
    }
}
