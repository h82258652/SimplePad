using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Windowing;

namespace SimplePad.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        InitializeComponent();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        IAppWindowManager appWindowManager = _serviceProvider.GetRequiredService<IAppWindowManager>();

        IAppWindow appWindow = await appWindowManager.ShowNewWindowAsync();
        appWindow.Execute(window => window.TabRoot.AddBlankTab());
    }
}
