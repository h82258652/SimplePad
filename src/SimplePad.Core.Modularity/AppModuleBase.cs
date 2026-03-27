namespace SimplePad.Core.Modularity;

/// <summary>
/// Represents the base class for the application module.
/// </summary>
public abstract class AppModuleBase
{
    /// <summary>
    /// Gets the dependent modules of the current module.
    /// </summary>
    public virtual DependsOn DependModules => DependsOn.Empty;

    /// <summary>
    /// Adds services to the application's service collection.
    /// </summary>
    /// <param name="context">The <see cref="ServiceConfigurationContext"/> instance.</param>
    public virtual void ConfigureServices(ServiceConfigurationContext context) { }

    /// <summary>
    /// Executed during the application initialization phase.
    /// </summary>
    /// <param name="context">The <see cref="ApplicationInitializationContext"/> instance.</param>
    public virtual void OnApplicationInitialization(ApplicationInitializationContext context) { }
}
