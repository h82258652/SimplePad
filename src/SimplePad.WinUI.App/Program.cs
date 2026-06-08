using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using SimplePad.Core.Modularity;
using SimplePad.Tabs;
using System.Threading;
using System.Threading.Tasks;
using WinRT;

namespace SimplePad.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        IHost host = ApplicationFactory
            .Create<SimplePadWinUIAppModule>(() => Host.CreateDefaultBuilder(args))
            .Build();
        host.Start();

        await Task.WhenAll(
            host.Services.GetRequiredService<IEditorSettings>().LoadAsync(),
            host.Services.GetRequiredService<IFontSettings>().LoadAsync(),
            host.Services.GetRequiredService<ISearchSettings>().LoadAsync(),
            host.Services.GetRequiredService<IStatusBarSettings>().LoadAsync(),
            host.Services.GetRequiredService<ITabsSettings>().LoadAsync(),
            host.Services.GetRequiredService<IThemeSettings>().LoadAsync());

        ComWrappersSupport.InitializeComWrappers();
        Application.Start((p) => 
        {
            DispatcherQueueSynchronizationContext context = new(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);
            _ = new App();
        });
    }
}
