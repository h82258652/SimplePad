using System;
using SimplePad.Core.Extensions;
using SimplePad.Tabs;
using Windows.UI.Core;

namespace SimplePad.Windowing;

public sealed class UWPAppWindow : IAppWindow
{
    private readonly IAppWindowManager _windowManager;

    public UWPAppWindow(IAppWindowManager windowManager, CoreDispatcher dispatcher)
    {
        _windowManager = windowManager;
        Dispatcher = dispatcher;
    }

    public TabRoot TabRoot { get; } = new TabRoot();

    internal CoreDispatcher Dispatcher { get; }

    public async void Execute(Action<IAppWindow> action)
    {
        await Dispatcher.SafeRunAsync(() =>
        {
            action(this);
        });
    }
}