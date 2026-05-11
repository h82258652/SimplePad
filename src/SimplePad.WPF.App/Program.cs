using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimplePad.Core.Modularity;
using SimplePad.Fonts;

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

        host.Services.GetRequiredService<IFontSettings>().LoadAsync().GetAwaiter().GetResult();

        App app = new(host.Services);
        app.Run();
    }
}
