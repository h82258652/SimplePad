using System;
using System.Threading.Tasks;

namespace SimplePad.Tabs;

internal sealed class UWPConfirmCloseService : IConfirmCloseService
{
    public async Task<ConfirmCloseResult> ConfirmCloseAsync(Tab tab)
    {
        ConfirmCloseDialog dialog = new(tab);
        await dialog.ShowAsync();
        return dialog.Result ?? ConfirmCloseResult.Cancel;
    }
}
