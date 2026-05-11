using System;
using System.Threading.Tasks;

namespace SimplePad.Tabs;

internal sealed class WPFConfirmCloseService : IConfirmCloseService
{
    public Task<ConfirmCloseResult> ConfirmCloseAsync(Tab tab)
    {
        throw new NotImplementedException();
    }
}
