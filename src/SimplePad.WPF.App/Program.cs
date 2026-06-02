using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Fonts;
using SimplePad.Search;
using SimplePad.StatusBar;
using SimplePad.Tabs;
using SimplePad.Themes;

namespace SimplePad.App;

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var host = ApplicationFactory
            .Create<SimplePadWPFAppModule>(() => Host.CreateDefaultBuilder(args))
            .Build();
        host.Start();

        // WPF application can't not use async Main method, so we need to load settings synchronously.
        Task.WhenAll(
             host.Services.GetRequiredService<IEditorSettings>().LoadAsync(),
             host.Services.GetRequiredService<IFontSettings>().LoadAsync(),
             host.Services.GetRequiredService<ISearchSettings>().LoadAsync(),
             host.Services.GetRequiredService<IStatusBarSettings>().LoadAsync(),
             host.Services.GetRequiredService<ITabsSettings>().LoadAsync(),
             host.Services.GetRequiredService<IThemeSettings>().LoadAsync()).GetAwaiter().GetResult();

        App app = new(host.Services);
        app.Run();
    }
}
