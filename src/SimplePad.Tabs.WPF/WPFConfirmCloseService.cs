using System.Threading;
using System.Threading.Tasks;
using SimplePad.Settings;
using Wpf.Ui;

namespace SimplePad.Tabs;

internal sealed class WPFConfirmCloseService : IConfirmCloseService
{
    private readonly SettingsState _settingsState;
    private readonly IContentDialogService _contentDialogService;

    public WPFConfirmCloseService(SettingsState settingsState, IContentDialogService contentDialogService)
    {
        _settingsState = settingsState;
        _contentDialogService = contentDialogService;
    }

    public async Task<ConfirmCloseResult> ConfirmCloseAsync(Tab tab)
    {
        // Ensure the tab is selected before showing the dialog.
        _settingsState.IsVisible = false;
        tab.Root.SelectedTab = tab;

        ConfirmCloseDialog dialog = new(tab);
        await _contentDialogService.ShowAsync(dialog, CancellationToken.None);
        return dialog.Result ?? ConfirmCloseResult.Cancel;
    }
}
