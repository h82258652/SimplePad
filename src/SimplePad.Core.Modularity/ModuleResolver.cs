using System;
using System.Collections.Generic;

namespace SimplePad.Core.Modularity;

/// <summary>
/// Resolves and instantiates all modules in a dependency tree starting from a root module descriptor.
/// </summary>
internal sealed class ModuleResolver
{
    private readonly DependentDescriptor _rootModuleDescriptor;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleResolver"/> class with the specified root module descriptor.
    /// </summary>
    /// <param name="rootModuleDescriptor">The root module descriptor to resolve from.</param>
    internal ModuleResolver(DependentDescriptor rootModuleDescriptor)
    {
        _rootModuleDescriptor = rootModuleDescriptor;
    }

    /// <summary>
    /// Resolves all modules in the dependency tree, starting from the root module.
    /// </summary>
    /// <returns>
    /// A read-only list of <see cref="AppModuleBase"/> instances representing the resolved modules.
    /// The root module is included as the last element in the list.
    /// </returns>
    internal IReadOnlyList<AppModuleBase> Resolve()
    {
        AppModuleBase rootModule = _rootModuleDescriptor.CreationDelegate.Invoke();
        HashSet<Type> resolvedModuleTypes = [_rootModuleDescriptor.ModuleType];
        List<AppModuleBase> modules = [];

        ResolveInternal(rootModule, resolvedModuleTypes, modules);

        modules.Add(rootModule);

        return modules;
    }

    /// <summary>
    /// Recursively resolves dependent modules and adds them to the provided list.
    /// </summary>
    /// <param name="module">The current module to resolve dependencies for.</param>
    /// <param name="resolvedModuleTypes">A set of already resolved module types to prevent duplicates.</param>
    /// <param name="modules">The list to which resolved modules are added.</param>
    private static void ResolveInternal(AppModuleBase module, HashSet<Type> resolvedModuleTypes, List<AppModuleBase> modules)
    {
        foreach (DependentDescriptor dependent in module.DependModules.Dependents)
        {
            if (resolvedModuleTypes.Contains(dependent.ModuleType))
            {
                continue;
            }

            AppModuleBase dependentModule = dependent.CreationDelegate.Invoke();
            resolvedModuleTypes.Add(dependent.ModuleType);

            ResolveInternal(dependentModule, resolvedModuleTypes, modules);

            modules.Add(dependentModule);
        }
    }
}
