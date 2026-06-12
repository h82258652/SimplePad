using SimplePad.Core;

namespace SimplePad.Windowing;

internal sealed class WinUIServiceScopeIdProvider : IServiceScopeIdProvider
{
    private readonly IAppWindowManager _appWindowManager;

    public WinUIServiceScopeIdProvider(IAppWindowManager appWindowManager)
    {
        _appWindowManager = appWindowManager;
    }

    public object? Get()
    {
        if (_appWindowManager.CurrentWindow is WinUIAppWindow currentWindow)
        {
            return currentWindow.ShellWindow?.AppWindow.Id;
        }

        return null;
    }
}
