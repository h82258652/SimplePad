namespace SimplePad.Windowing;

public interface IAppWindowManager
{
    IAppWindow? CurrentWindow { get; }

    IReadOnlyList<IAppWindow> Instances { get; }

    Task<bool> CloseAsync(IAppWindow window);

    IAppWindow CreateAppWindow();

    Task<IAppWindow> ShowNewWindowAsync();
}