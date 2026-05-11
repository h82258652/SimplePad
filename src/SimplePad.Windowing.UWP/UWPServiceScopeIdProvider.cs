using System;
using SimplePad.Core;

namespace SimplePad.Windowing;

internal sealed class UWPServiceScopeIdProvider : IServiceScopeIdProvider
{
    public int? Get()
    {
        return Environment.CurrentManagedThreadId;
    }
}
