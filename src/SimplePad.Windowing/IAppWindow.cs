using SimplePad.Settings;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

public interface IAppWindow
{
    SettingsState SettingsState { get; }

    TabManager TabManager { get; }

    void Close();
}