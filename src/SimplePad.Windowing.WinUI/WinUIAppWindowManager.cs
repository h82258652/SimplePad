using SimplePad.Tabs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class WinUIAppWindowManager : IAppWindowManager
{
    private readonly List<WinUIAppWindow> _instances = [];
    private readonly IServiceProvider _serviceProvider;
    private readonly TabManager _tabManager;

    public WinUIAppWindowManager(IServiceProvider serviceProvider, TabManager tabManager)
    {
        _serviceProvider = serviceProvider;
        _tabManager = tabManager;
    }

    public IAppWindow CurrentWindow => throw new NotImplementedException();

    public IReadOnlyList<IAppWindow> Instances => _instances;

    public async Task<bool> CloseAsync(IAppWindow window)
    {
        if (window is not WinUIAppWindow winuiWindow)
        {
            throw new InvalidOperationException();
        }

        if (!_instances.Contains(winuiWindow))
        {
            return false;
        }

        throw new NotImplementedException();
    }

    public IAppWindow CreateAppWindow()
    {
        throw new NotImplementedException();
    }

    public Task<IAppWindow> ShowNewWindowAsync()
    {
        throw new NotImplementedException();
    }
}