using SimplePad.Core.Extensions;
using SimplePad.Tabs;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class WinUIAppWindow : IAppWindow
{
    private readonly IAppWindowManager _appWindowManager;

    internal WinUIAppWindow(IAppWindowManager appWindowManager)
    {
        _appWindowManager = appWindowManager;

        TabRoot.Tabs.CollectionChanged += OnTabsCollectionChanged;
    }

    public TabRoot TabRoot { get; } = new TabRoot();

    internal ShellWindow? ShellWindow { get; set; }

    public void Execute(Action<IAppWindow> action)
    {
        ShellWindow?.DispatcherQueue.SafeRunAsync(() => action(this));
    }

    public Task ShowAsync()
    {
        ShellWindow?.Activate();
        return Task.CompletedTask;
    }

    private void OnTabsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (TabRoot.Tabs.Count == 0)
        {
            _appWindowManager.CloseAsync(this);
        }
    }
}