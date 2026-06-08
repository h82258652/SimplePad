using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Fonts;
using SimplePad.Search;
using SimplePad.StatusBar;
using SimplePad.Tabs;
using SimplePad.Themes;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;

namespace SimplePad.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        IHost host = ApplicationFactory
            .Create<SimplePadUWPAppModule>(() => Host.CreateDefaultBuilder(args))
            .Build();
        host.Start();

        await Task.WhenAll(
            host.Services.GetRequiredService<IEditorSettings>().LoadAsync(),
            host.Services.GetRequiredService<IFontSettings>().LoadAsync(),
            host.Services.GetRequiredService<ISearchSettings>().LoadAsync(),
            host.Services.GetRequiredService<IStatusBarSettings>().LoadAsync(),
            host.Services.GetRequiredService<ITabsSettings>().LoadAsync(),
            host.Services.GetRequiredService<IThemeSettings>().LoadAsync());

        Application.Start(
            (p) =>
            {
                DispatcherQueueSynchronizationContext context = new(DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(context);
                _ = new App(host.Services);
            }
        );
    }
}
