using System;

namespace SimplePad.Core.Modularity;

/// <summary>
/// Describes the dependency of a module.
/// </summary>
public sealed class DependentDescriptor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DependentDescriptor"/> class.
    /// </summary>
    /// <param name="moduleType">The type of the dependent module.</param>
    /// <param name="creationDelegate">The delegate for creating an instance of the dependent module.</param>
    public DependentDescriptor(Type moduleType, Func<AppModuleBase> creationDelegate)
    {
        ModuleType = moduleType;
        CreationDelegate = creationDelegate;
    }

    /// <summary>
    /// Gets the delegate for creating an instance of the dependent module.
    /// </summary>
    public Func<AppModuleBase> CreationDelegate { get; }

    /// <summary>
    /// Gets the type of the dependent module.
    /// </summary>
    public Type ModuleType { get; }
}
