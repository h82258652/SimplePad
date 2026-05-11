using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

internal sealed class WPFAppWindowManager : IAppWindowManager
{
    private readonly SemaphoreSlim _closeLock = new(1);
    private readonly List<WPFAppWindow> _instances = [];

    public IAppWindow? CurrentWindow => throw new NotImplementedException();

    public IReadOnlyList<IAppWindow> Instances => _instances;
    private readonly TabManager _tabManager;

    public WPFAppWindowManager(TabManager tabManager)
    {
        _tabManager = tabManager;
    }

    public async Task<bool> CloseAsync(IAppWindow window)
    {
        if (window is not WPFAppWindow wpfWindow)
        {
            throw new InvalidOperationException();
        }

        if (!_instances.Contains(wpfWindow))
        {
            return false;
        }

        await _closeLock.WaitAsync();
        try
        {
            TaskCompletionSource<bool> tcs = new();
            wpfWindow.Execute(async wpfWindowInstance =>
            {
                foreach (Tab tab in wpfWindowInstance.TabRoot.Tabs.ToList())
                {
                    if (!await _tabManager.CloseAsync(tab))
                    {
                        tcs.SetResult(false);
                        return;
                    }
                }

                // TODO close window
                tcs.SetResult(true);
            });
            bool allTabsClosed = await tcs.Task;
            if (allTabsClosed)
            {
                _instances.Remove(wpfWindow);
            }

            return allTabsClosed;
        }
        finally
        {
            _closeLock.Release();
        }
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
