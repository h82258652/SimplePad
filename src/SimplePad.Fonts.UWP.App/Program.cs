using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePad.Core;
using SimplePad.Core.Modularity;
using SimplePad.Fonts.Settings;
using Windows.System;
using Windows.UI.Xaml;

namespace SimplePad.Fonts.UWP.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = ApplicationFactory
            .Create<SimplePadFontsUWPAppModule>(() => Host.CreateDefaultBuilder(args))
            .Build();
        host.Start();
        ServiceLocator.SetLocatorProvider(host.Services);

        await host.Services.GetRequiredService<IFontSettings>().LoadAsync();

        Application.Start(
            (p) =>
            {
                DispatcherQueueSynchronizationContext context = new(
                    DispatcherQueue.GetForCurrentThread()
                );
                SynchronizationContext.SetSynchronizationContext(context);
                _ = new App();
            }
        );
    }
}
