using SimplePad.Tabs;

namespace SimplePad.Windowing;

public interface IAppWindow
{
    TabRoot TabRoot { get; }

    void Close();

    void Execute(Action<IAppWindow> action);
}