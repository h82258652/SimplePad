using System;
using SimplePad.Core;

namespace SimplePad.Windowing;

internal sealed class UWPServiceProviderIdProvider : IServiceProviderIdProvider
{
    public int? Get()
    {
        return Environment.CurrentManagedThreadId;
    }
}
