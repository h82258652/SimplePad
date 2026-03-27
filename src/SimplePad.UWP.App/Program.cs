using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePad.Core;
using SimplePad.Core.Modularity;
using SimplePad.Settings;
using Windows.System;
using Windows.UI.Xaml;

namespace SimplePad.UWP.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = ApplicationFactory.Create<SimplePadUWPAppModule>(() => Host.CreateDefaultBuilder(args)).Build();
        host.Start();
        ServiceLocator.SetLocatorProvider(host.Services);

        await host.Services.GetRequiredService<IAppSettings>().LoadAsync();

        Application.Start((p) =>
        {
            DispatcherQueueSynchronizationContext context = new(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);
            _ = new App();
        });
    }
}
