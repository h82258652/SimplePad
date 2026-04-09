namespace SimplePad.Windowing;

public interface IAppWindowManager
{
    IReadOnlyList<IAppWindow> Instances { get; }

    IAppWindow CreateAppWindow();

    Task<IAppWindow> ShowNewWindowAsync();
}
