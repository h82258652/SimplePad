using SimplePad.Tabs;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class WPFAppWindow : IAppWindow
{
    private readonly IAppWindowManager _appWindowManager;

    public WPFAppWindow(IAppWindowManager appWindowManager)
    {
        _appWindowManager = appWindowManager;

        TabRoot.Tabs.CollectionChanged += OnTabsCollectionChanged;
    }

    public TabRoot TabRoot { get; } = new TabRoot();

    internal ShellWindow? ShellWindow { get; set; }

    public void Execute(Action<IAppWindow> action)
    {
        ShellWindow?.Dispatcher.Invoke(() => action(this));
    }

    public Task ShowAsync()
    {
        ShellWindow?.Show();
        return Task.CompletedTask;
    }

    private async void OnTabsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (TabRoot.Tabs.Count == 0)
        {
            await _appWindowManager.CloseAsync(this);
        }
    }
}