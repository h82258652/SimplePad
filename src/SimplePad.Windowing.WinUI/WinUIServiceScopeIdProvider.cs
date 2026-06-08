using SimplePad.Core;
using System;

namespace SimplePad.Windowing;

internal sealed class WinUIServiceScopeIdProvider : IServiceScopeIdProvider
{
    private readonly IAppWindowManager _appWindowManager;

    public WinUIServiceScopeIdProvider(IAppWindowManager appWindowManager)
    {
        _appWindowManager = appWindowManager;
    }

    public int? Get()
    {
        if (_appWindowManager.CurrentWindow is { } currentWindow)
        {
            return currentWindow.GetHashCode();
        }

        return null;
    }
}
