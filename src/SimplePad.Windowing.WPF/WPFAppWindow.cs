using SimplePad.Tabs;
using System;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class WPFAppWindow : IAppWindow
{
    public TabRoot TabRoot => throw new NotImplementedException();

    public void Execute(Action<IAppWindow> action)
    {
        throw new NotImplementedException();
    }

    public WPFAppWindow(ShellWindow shellWindow)
    {
        ShellWindow = shellWindow;
    }

    internal ShellWindow ShellWindow
    {
        get;
    }

    public Task ShowAsync()
    {
        throw new NotImplementedException();
    }
}
