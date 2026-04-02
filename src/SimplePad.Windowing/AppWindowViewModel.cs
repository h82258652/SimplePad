using SimplePad.MultiTab;
using SimplePad.Settings;

namespace SimplePad.Windowing;

public sealed class AppWindowViewModel
{
    private readonly List<AppTabViewModel> _tabs = [];

    public SettingsState SettingsState { get; } = new SettingsState();

    public IReadOnlyList<AppTabViewModel> Tabs => _tabs;
}