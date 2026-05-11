using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

internal sealed class WPFAppWindowManager : IAppWindowManager
{
    private readonly SemaphoreSlim _closeLock = new(1);
    private readonly List<WPFAppWindow> _instances = [];
    private readonly IServiceProvider _serviceProvider;
    private readonly TabManager _tabManager;
    private ShellWindow? _currentActivatedWindow;

    public WPFAppWindowManager(IServiceProvider serviceProvider, TabManager tabManager)
    {
        _serviceProvider = serviceProvider;
        _tabManager = tabManager;
    }

    public IAppWindow? CurrentWindow
    {
        get
        {
            if (_currentActivatedWindow is null)
            {
                return null;
            }

            return _instances.FirstOrDefault(instance => instance.ShellWindow == _currentActivatedWindow);
        }
    }

    public IReadOnlyList<IAppWindow> Instances => _instances;

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

                ((WPFAppWindow)wpfWindowInstance).ShellWindow.Close();
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
        return CreateAppWindowInternal();
    }

    public async Task<IAppWindow> ShowNewWindowAsync()
    {
        WPFAppWindow newAppWindow = CreateAppWindowInternal();
        await newAppWindow.ShowAsync();
        return newAppWindow;
    }

    private WPFAppWindow CreateAppWindowInternal()
    {
        IServiceScope scope = _serviceProvider.CreateScope();

        IServiceProvider scopeServiceProvder = scope.ServiceProvider;
        ServiceLocator.SetLocatorProvider(scopeServiceProvder);

        ShellWindow shellWindow = new ShellWindow();
        shellWindow.Activated += OnShellWindowActivated;

        WPFAppWindow instance = new WPFAppWindow(shellWindow);
        _instances.Add(instance);
        return instance;
    }

    private void OnShellWindowActivated(object? sender, EventArgs e)
    {
        if (sender is ShellWindow shellWindow)
        {
            _currentActivatedWindow = shellWindow;
        }
    }
}