using SimplePad.Settings;
using System;
using System.Threading.Tasks;

namespace SimplePad.Tabs;

internal sealed class WinUIConfirmCloseService : IConfirmCloseService
{
    public WinUIConfirmCloseService(SettingsState settingsState)
    {
        
    }

    public Task<ConfirmCloseResult> ConfirmCloseAsync(Tab tab)
    {
        throw new NotImplementedException();
    }
}
