using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePad.Core.Modularity;

namespace SimplePad.Fonts.TestApp;

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var host = ApplicationFactory
            .Create<SimplePadFontsWPFTestAppModule>(() => Host.CreateDefaultBuilder(args))
            .Build();
        host.Start();

        host.Services.GetRequiredService<IFontSettings>().LoadAsync().GetAwaiter().GetResult();

        App app = new(host.Services);
        app.Run();
    }
}
