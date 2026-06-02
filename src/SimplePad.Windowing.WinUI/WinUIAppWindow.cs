using System;
using System.Threading.Tasks;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

internal sealed class WinUIAppWindow : IAppWindow
{
    public TabRoot TabRoot => throw new NotImplementedException();

    public void Execute(Action<IAppWindow> action)
    {
        throw new NotImplementedException();
    }

    public Task ShowAsync()
    {
        throw new NotImplementedException();
    }
}
