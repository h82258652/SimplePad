using SimplePad.Settings;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

public interface IAppWindow
{
    SettingsState SettingsState { get; }

    TabRoot TabRoot { get; }

    void Close();
}