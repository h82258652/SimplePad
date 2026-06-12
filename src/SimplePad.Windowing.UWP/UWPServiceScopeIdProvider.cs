using System;
using SimplePad.Core;

namespace SimplePad.Windowing;

internal sealed class UWPServiceScopeIdProvider : IServiceScopeIdProvider
{
    public object? Get()
    {
        return Environment.CurrentManagedThreadId;
    }
}
