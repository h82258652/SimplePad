using SimplePad.Tabs;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class AvaloniaAppWindow : IAppWindow
{
    private readonly IAppWindowManager _appWindowManager;

    internal AvaloniaAppWindow(IAppWindowManager appWindowManager)
    {
        _appWindowManager = appWindowManager;

        TabRoot.Tabs.CollectionChanged += OnTabsCollectionChanged;
    }

    public TabRoot TabRoot { get; } = new TabRoot();

    public void Execute(Action<IAppWindow> action)
    {
        throw new NotImplementedException();
    }

    public Task ShowAsync()
    {
        throw new NotImplementedException();
    }

    private void OnTabsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }
}