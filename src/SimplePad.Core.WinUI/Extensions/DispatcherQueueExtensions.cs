using Microsoft.UI.Dispatching;
using System.Threading.Tasks;

namespace SimplePad.Core.Extensions;

public static class DispatcherQueueExtensions
{
    public static async Task SaveRunAsync(this DispatcherQueue dispatcherQueue, DispatcherQueueHandler callback)
    {
        if (dispatcherQueue.HasThreadAccess)
        {
            callback();
        }
        else
        {
            TaskCompletionSource<object?> tcs = new();
            _ = dispatcherQueue.TryEnqueue(() =>
            {
                callback();
                tcs.SetResult(null);
            });
            await tcs.Task;
        }
    }
}