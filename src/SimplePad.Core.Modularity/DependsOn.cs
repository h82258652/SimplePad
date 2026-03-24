using System.Collections.Generic;

namespace SimplePad.Core.Modularity;

/// <summary>
/// A class that specifies the modules that a particular module depends on.
/// </summary>
public sealed class DependsOn
{
    public IReadOnlyList<DependentDescriptor> Dependents;

    /// <summary>
    /// Initializes a new instance of the <see cref="DependsOn"/> class.
    /// </summary>
    /// <param name="dependents">The dependencies list.</param>
    public DependsOn(IReadOnlyList<DependentDescriptor> dependents)
    {
        Dependents = dependents;
    }

    /// <summary>
    /// Gets an empty <see cref="DependsOn"/> instance, indicating no dependencies.
    /// </summary>
    public static DependsOn Empty => new([]);

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with one module dependency.
    /// </summary>
    /// <typeparam name="TModule1">The module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1>()
        where TModule1 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with two module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with three module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with four module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with five module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with six module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with seven module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with eight module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with nine module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <typeparam name="TModule9">The ninth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8, TModule9>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
        where TModule9 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
            new DependentDescriptor(typeof(TModule9), () => new TModule9()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with ten module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <typeparam name="TModule9">The ninth module type.</typeparam>
    /// <typeparam name="TModule10">The tenth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8, TModule9, TModule10>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
        where TModule9 : AppModuleBase, new()
        where TModule10 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
            new DependentDescriptor(typeof(TModule9), () => new TModule9()),
            new DependentDescriptor(typeof(TModule10), () => new TModule10()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with eleven module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <typeparam name="TModule9">The ninth module type.</typeparam>
    /// <typeparam name="TModule10">The tenth module type.</typeparam>
    /// <typeparam name="TModule11">The eleventh module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8, TModule9, TModule10, TModule11>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
        where TModule9 : AppModuleBase, new()
        where TModule10 : AppModuleBase, new()
        where TModule11 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
            new DependentDescriptor(typeof(TModule9), () => new TModule9()),
            new DependentDescriptor(typeof(TModule10), () => new TModule10()),
            new DependentDescriptor(typeof(TModule11), () => new TModule11()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with twelve module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <typeparam name="TModule9">The ninth module type.</typeparam>
    /// <typeparam name="TModule10">The tenth module type.</typeparam>
    /// <typeparam name="TModule11">The eleventh module type.</typeparam>
    /// <typeparam name="TModule12">The twelfth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8, TModule9, TModule10, TModule11, TModule12>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
        where TModule9 : AppModuleBase, new()
        where TModule10 : AppModuleBase, new()
        where TModule11 : AppModuleBase, new()
        where TModule12 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
            new DependentDescriptor(typeof(TModule9), () => new TModule9()),
            new DependentDescriptor(typeof(TModule10), () => new TModule10()),
            new DependentDescriptor(typeof(TModule11), () => new TModule11()),
            new DependentDescriptor(typeof(TModule12), () => new TModule12()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with thirteen module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <typeparam name="TModule9">The ninth module type.</typeparam>
    /// <typeparam name="TModule10">The tenth module type.</typeparam>
    /// <typeparam name="TModule11">The eleventh module type.</typeparam>
    /// <typeparam name="TModule12">The twelfth module type.</typeparam>
    /// <typeparam name="TModule13">The thirteenth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8, TModule9, TModule10, TModule11, TModule12, TModule13>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
        where TModule9 : AppModuleBase, new()
        where TModule10 : AppModuleBase, new()
        where TModule11 : AppModuleBase, new()
        where TModule12 : AppModuleBase, new()
        where TModule13 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
            new DependentDescriptor(typeof(TModule9), () => new TModule9()),
            new DependentDescriptor(typeof(TModule10), () => new TModule10()),
            new DependentDescriptor(typeof(TModule11), () => new TModule11()),
            new DependentDescriptor(typeof(TModule12), () => new TModule12()),
            new DependentDescriptor(typeof(TModule13), () => new TModule13()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with fourteen module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <typeparam name="TModule9">The ninth module type.</typeparam>
    /// <typeparam name="TModule10">The tenth module type.</typeparam>
    /// <typeparam name="TModule11">The eleventh module type.</typeparam>
    /// <typeparam name="TModule12">The twelfth module type.</typeparam>
    /// <typeparam name="TModule13">The thirteenth module type.</typeparam>
    /// <typeparam name="TModule14">The fourteenth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8, TModule9, TModule10, TModule11, TModule12, TModule13, TModule14>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
        where TModule9 : AppModuleBase, new()
        where TModule10 : AppModuleBase, new()
        where TModule11 : AppModuleBase, new()
        where TModule12 : AppModuleBase, new()
        where TModule13 : AppModuleBase, new()
        where TModule14 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
            new DependentDescriptor(typeof(TModule9), () => new TModule9()),
            new DependentDescriptor(typeof(TModule10), () => new TModule10()),
            new DependentDescriptor(typeof(TModule11), () => new TModule11()),
            new DependentDescriptor(typeof(TModule12), () => new TModule12()),
            new DependentDescriptor(typeof(TModule13), () => new TModule13()),
            new DependentDescriptor(typeof(TModule14), () => new TModule14()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with fifteen module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <typeparam name="TModule9">The ninth module type.</typeparam>
    /// <typeparam name="TModule10">The tenth module type.</typeparam>
    /// <typeparam name="TModule11">The eleventh module type.</typeparam>
    /// <typeparam name="TModule12">The twelfth module type.</typeparam>
    /// <typeparam name="TModule13">The thirteenth module type.</typeparam>
    /// <typeparam name="TModule14">The fourteenth module type.</typeparam>
    /// <typeparam name="TModule15">The fifteenth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8, TModule9, TModule10, TModule11, TModule12, TModule13, TModule14, TModule15>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
        where TModule9 : AppModuleBase, new()
        where TModule10 : AppModuleBase, new()
        where TModule11 : AppModuleBase, new()
        where TModule12 : AppModuleBase, new()
        where TModule13 : AppModuleBase, new()
        where TModule14 : AppModuleBase, new()
        where TModule15 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
            new DependentDescriptor(typeof(TModule9), () => new TModule9()),
            new DependentDescriptor(typeof(TModule10), () => new TModule10()),
            new DependentDescriptor(typeof(TModule11), () => new TModule11()),
            new DependentDescriptor(typeof(TModule12), () => new TModule12()),
            new DependentDescriptor(typeof(TModule13), () => new TModule13()),
            new DependentDescriptor(typeof(TModule14), () => new TModule14()),
            new DependentDescriptor(typeof(TModule15), () => new TModule15()),
        ]);
    }

    /// <summary>
    /// Creates a <see cref="DependsOn"/> instance with sixteen module dependencies.
    /// </summary>
    /// <typeparam name="TModule1">The first module type.</typeparam>
    /// <typeparam name="TModule2">The second module type.</typeparam>
    /// <typeparam name="TModule3">The third module type.</typeparam>
    /// <typeparam name="TModule4">The fourth module type.</typeparam>
    /// <typeparam name="TModule5">The fifth module type.</typeparam>
    /// <typeparam name="TModule6">The sixth module type.</typeparam>
    /// <typeparam name="TModule7">The seventh module type.</typeparam>
    /// <typeparam name="TModule8">The eighth module type.</typeparam>
    /// <typeparam name="TModule9">The ninth module type.</typeparam>
    /// <typeparam name="TModule10">The tenth module type.</typeparam>
    /// <typeparam name="TModule11">The eleventh module type.</typeparam>
    /// <typeparam name="TModule12">The twelfth module type.</typeparam>
    /// <typeparam name="TModule13">The thirteenth module type.</typeparam>
    /// <typeparam name="TModule14">The fourteenth module type.</typeparam>
    /// <typeparam name="TModule15">The fifteenth module type.</typeparam>
    /// <typeparam name="TModule16">The sixteenth module type.</typeparam>
    /// <returns>The <see cref="DependsOn"/> instance.</returns>
    public static DependsOn Create<TModule1, TModule2, TModule3, TModule4, TModule5, TModule6, TModule7, TModule8, TModule9, TModule10, TModule11, TModule12, TModule13, TModule14, TModule15, TModule16>()
        where TModule1 : AppModuleBase, new()
        where TModule2 : AppModuleBase, new()
        where TModule3 : AppModuleBase, new()
        where TModule4 : AppModuleBase, new()
        where TModule5 : AppModuleBase, new()
        where TModule6 : AppModuleBase, new()
        where TModule7 : AppModuleBase, new()
        where TModule8 : AppModuleBase, new()
        where TModule9 : AppModuleBase, new()
        where TModule10 : AppModuleBase, new()
        where TModule11 : AppModuleBase, new()
        where TModule12 : AppModuleBase, new()
        where TModule13 : AppModuleBase, new()
        where TModule14 : AppModuleBase, new()
        where TModule15 : AppModuleBase, new()
        where TModule16 : AppModuleBase, new()
    {
        return new DependsOn([
            new DependentDescriptor(typeof(TModule1), () => new TModule1()),
            new DependentDescriptor(typeof(TModule2), () => new TModule2()),
            new DependentDescriptor(typeof(TModule3), () => new TModule3()),
            new DependentDescriptor(typeof(TModule4), () => new TModule4()),
            new DependentDescriptor(typeof(TModule5), () => new TModule5()),
            new DependentDescriptor(typeof(TModule6), () => new TModule6()),
            new DependentDescriptor(typeof(TModule7), () => new TModule7()),
            new DependentDescriptor(typeof(TModule8), () => new TModule8()),
            new DependentDescriptor(typeof(TModule9), () => new TModule9()),
            new DependentDescriptor(typeof(TModule10), () => new TModule10()),
            new DependentDescriptor(typeof(TModule11), () => new TModule11()),
            new DependentDescriptor(typeof(TModule12), () => new TModule12()),
            new DependentDescriptor(typeof(TModule13), () => new TModule13()),
            new DependentDescriptor(typeof(TModule14), () => new TModule14()),
            new DependentDescriptor(typeof(TModule15), () => new TModule15()),
            new DependentDescriptor(typeof(TModule16), () => new TModule16()),
        ]);
    }
}
