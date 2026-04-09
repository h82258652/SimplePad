using System;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

public sealed class UWPAppWindow : IAppWindow
{
    public UWPAppWindow(IAppWindowManager windowManager)
    {
        _windowManager = windowManager;
    }

    private readonly IAppWindowManager _windowManager;

    public TabRoot TabRoot { get; } = new TabRoot();

    public void Close()
    {
        throw new NotImplementedException();
    }
}
