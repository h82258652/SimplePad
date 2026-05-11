using SimplePad.Tabs;
using System;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class WPFAppWindow : IAppWindow
{
    public TabRoot TabRoot => throw new NotImplementedException();

    public void Execute(Action<IAppWindow> action)
    {
        ShellWindow.Dispatcher.Invoke(() => action(this));
    }

    public WPFAppWindow(ShellWindow shellWindow)
    {
        ShellWindow = shellWindow;
    }

    internal ShellWindow ShellWindow { get; }

    public Task ShowAsync()
    {
        ShellWindow.Show();
        return Task.CompletedTask;
    }
}
