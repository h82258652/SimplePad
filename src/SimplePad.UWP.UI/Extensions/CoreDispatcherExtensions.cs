using System;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace SimplePad.UWP.UI.Extensions;

public static class CoreDispatcherExtensions
{
    public static async Task SafeRunAsync(this CoreDispatcher coreDispatcher, DispatchedHandler agileCallback)
    {
        if (coreDispatcher.HasThreadAccess)
        {
            agileCallback();
        }
        else
        {
            await coreDispatcher.RunAsync(CoreDispatcherPriority.Normal, agileCallback);
        }
    }
}
