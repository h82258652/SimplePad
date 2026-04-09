using System;
using SimplePad.Core.Extensions;
using SimplePad.Tabs;
using Windows.UI.Core;

namespace SimplePad.Windowing;

public sealed class UWPAppWindow : IAppWindow
{
    private readonly CoreDispatcher _dispatcher;

    private readonly IAppWindowManager _windowManager;

    public UWPAppWindow(IAppWindowManager windowManager, CoreDispatcher dispatcher)
    {
        _windowManager = windowManager;
        _dispatcher = dispatcher;
    }

    public TabRoot TabRoot { get; } = new TabRoot();

    public void Close()
    {
        throw new NotImplementedException();
    }

    public async void Execute(Action<IAppWindow> action)
    {
        await _dispatcher.SafeRunAsync(() =>
        {
            action(this);
        });
    }
}