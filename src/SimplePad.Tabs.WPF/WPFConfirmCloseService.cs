using System.Threading.Tasks;
using SimplePad.Settings;

namespace SimplePad.Tabs;

internal sealed class WPFConfirmCloseService : IConfirmCloseService
{
    private readonly SettingsState _settingsState;

    public WPFConfirmCloseService(SettingsState settingsState)
    {
        _settingsState = settingsState;
    }

    public Task<ConfirmCloseResult> ConfirmCloseAsync(Tab tab)
    {
        // Ensure the tab is selected before showing the dialog.
        _settingsState.IsVisible = false;
        tab.Root.SelectedTab = tab;

        ConfirmCloseDialog dialog = new(tab);
        dialog.ShowDialog();
        return Task.FromResult(dialog.Result ?? ConfirmCloseResult.Cancel);
    }
}
