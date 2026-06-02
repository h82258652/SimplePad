using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Editor;
using SimplePad.Fonts;
using SimplePad.Search;
using SimplePad.StatusBar;
using SimplePad.Tabs;
using SimplePad.Themes;
using SimplePad.Windowing;

namespace SimplePad.App;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        InitializeComponent();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Task.WhenAll(
            _serviceProvider.GetRequiredService<IEditorSettings>().SaveAsync(),
            _serviceProvider.GetRequiredService<IFontSettings>().SaveAsync(),
            _serviceProvider.GetRequiredService<ISearchSettings>().SaveAsync(),
            _serviceProvider.GetRequiredService<IStatusBarSettings>().SaveAsync(),
            _serviceProvider.GetRequiredService<ITabsSettings>().SaveAsync(),
            _serviceProvider.GetRequiredService<IThemeSettings>().SaveAsync()).GetAwaiter().GetResult();

        base.OnExit(e);
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        IAppWindowManager appWindowManager = _serviceProvider.GetRequiredService<IAppWindowManager>();

        IAppWindow appWindow = await appWindowManager.ShowNewWindowAsync();
        appWindow.Execute(window => window.TabRoot.AddBlankTab());
    }
}
