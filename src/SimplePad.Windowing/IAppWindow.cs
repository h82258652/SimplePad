using SimplePad.Tabs;

namespace SimplePad.Windowing;

public interface IAppWindow
{
    object Id { get; }

    TabRoot TabRoot { get; }

    void Execute(Action<IAppWindow> action);

    Task ShowAsync();
}