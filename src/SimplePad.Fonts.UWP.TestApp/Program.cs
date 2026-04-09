using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePad.Core;
using SimplePad.Core.Modularity;
using Windows.System;
using Windows.UI.Xaml;

namespace SimplePad.Fonts.TestApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = ApplicationFactory
            .Create<SimplePadFontsUWPTestAppModule>(() => Host.CreateDefaultBuilder(args))
            .Build();
        host.Start();

        await host.Services.GetRequiredService<IFontSettings>().LoadAsync();

        Application.Start(
            (p) =>
            {
                DispatcherQueueSynchronizationContext context = new(
                    DispatcherQueue.GetForCurrentThread()
                );
                SynchronizationContext.SetSynchronizationContext(context);
                _ = new App(host.Services);
            }
        );
    }
}
