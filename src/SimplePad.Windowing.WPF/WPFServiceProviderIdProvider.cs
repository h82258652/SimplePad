using SimplePad.Core;

namespace SimplePad.Windowing;

internal sealed class WPFServiceProviderIdProvider : IServiceProviderIdProvider
{
    private readonly IAppWindowManager _appWindowManager;

    public WPFServiceProviderIdProvider(IAppWindowManager appWindowManager)
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
