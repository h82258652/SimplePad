using SimplePad.Core;

namespace SimplePad.Windowing;

internal sealed class WPFServiceScopeIdProvider : IServiceScopeIdProvider
{
    private readonly IAppWindowManager _appWindowManager;

    public WPFServiceScopeIdProvider(IAppWindowManager appWindowManager)
    {
        _appWindowManager = appWindowManager;
    }

    public object? Get()
    {
        if (_appWindowManager.CurrentWindow is { } currentWindow)
        {
            return currentWindow.GetHashCode();
        }

        return null;
    }
}
