using SimplePad.Tabs;
using System;
using System.Threading.Tasks;

namespace SimplePad.Windowing;

internal sealed class WPFAppWindow : IAppWindow
{
    public TabRoot TabRoot => throw new NotImplementedException();

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
}