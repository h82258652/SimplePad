using SimplePad.Settings;
using System;
using System.Threading.Tasks;

namespace SimplePad.Tabs;

internal sealed class UWPConfirmCloseService : IConfirmCloseService
{
    private readonly SettingsState _settingsState;

    public UWPConfirmCloseService(SettingsState settingsState)
    {
        _settingsState = settingsState;
    }

    public async Task<ConfirmCloseResult> ConfirmCloseAsync(Tab tab)
    {
        // Ensure the tab is selected before showing the dialog.
        _settingsState.IsVisible = false;
        tab.Root.SelectedTab = tab;

        ConfirmCloseDialog dialog = new(tab);
        await dialog.ShowAsync();
        return dialog.Result ?? ConfirmCloseResult.Cancel;
    }
}
