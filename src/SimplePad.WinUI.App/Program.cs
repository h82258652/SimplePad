using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using System.Threading;
using System.Threading.Tasks;
using WinRT;

namespace SimplePad.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        ComWrappersSupport.InitializeComWrappers();
        Application.Start((p) => 
        {
            var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);
            new App();
        });
    }
}
