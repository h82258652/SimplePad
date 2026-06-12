using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SimplePad.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class WinUIAppWindowManager : IAppWindowManager
{
    private readonly List<WinUIAppWindow> _instances = [];
    private readonly IServiceProvider _serviceProvider;
    private readonly TabManager _tabManager;
    private readonly SemaphoreSlim _closeLock = new(1);

    public WinUIAppWindowManager(IServiceProvider serviceProvider, TabManager tabManager)
    {
        _serviceProvider = serviceProvider;
        _tabManager = tabManager;
    }

    public IAppWindow? CurrentWindow { get; private set; }

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

        await _closeLock.WaitAsync();
        try
        {
            TaskCompletionSource<bool> tcs = new();
            winuiWindow.Execute(async winuiWindowInstance => 
            {
                foreach (Tab tab in winuiWindowInstance.TabRoot.Tabs.ToList())
                {
                    if (!await _tabManager.CloseAsync(tab))
                    {
                        tcs.SetResult(false);
                        return;
                    }
                }

                ((WinUIAppWindow)winuiWindowInstance).ShellWindow?.Close();
                tcs.SetResult(true);
            });
            bool allTabsClosed = await tcs.Task;
            if (allTabsClosed)
            {
                _instances.Remove(winuiWindow);
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
        return CreateAppWindowInternal();
    }

    public async Task<IAppWindow> ShowNewWindowAsync()
    {
        WinUIAppWindow newAppWindow = CreateAppWindowInternal();
        await newAppWindow.ShowAsync();
        return newAppWindow;
    }

    private WinUIAppWindow CreateAppWindowInternal()
    {
        IServiceScope scope = _serviceProvider.CreateScope();

        IServiceProvider scopeServiceProvider = scope.ServiceProvider;

        WinUIAppWindow instance = new(this);
        CurrentWindow = instance;

        ShellWindow shellWindow = new(instance, scopeServiceProvider);

        shellWindow.Activated += OnShellWindowActivated;

        _instances.Add(instance);
        return instance;
    }

    private void OnShellWindowActivated(object sender, WindowActivatedEventArgs args)
    {
        ShellWindow shellWindow = (ShellWindow)sender;
        CurrentWindow = shellWindow.AppWindowInstance;
    }
}