using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePad.Core.Modularity;
using SimplePad.Editor;
using SimplePad.Search;
using SimplePad.StatusBar;
using SimplePad.Tabs;
using System;
using System.Threading.Tasks;

namespace SimplePad.App;

public static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static async Task Main(string[] args)
    {
        IHost host = ApplicationFactory
            .Create<SimplePadAvaloniaAppModule>(() => Host.CreateDefaultBuilder(args))
            .Build();
        host.Start();

        await Task.WhenAll(
            host.Services.GetRequiredService<IEditorSettings>().LoadAsync(),
            host.Services.GetRequiredService<IFontSettings>().LoadAsync(),
            host.Services.GetRequiredService<ISearchSettings>().LoadAsync(),
            host.Services.GetRequiredService<IStatusBarSettings>().LoadAsync(),
            host.Services.GetRequiredService<ITabsSettings>().LoadAsync(),
            host.Services.GetRequiredService<IThemeSettings>().LoadAsync());

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
