using SimplePad.Settings;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

public interface IAppWindow
{
    SettingsState SettingsState { get; }

    IReadOnlyList<AppTabViewModel> Tabs { get; }

    void Close();
}