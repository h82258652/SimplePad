using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using SimplePad.Windowing;
using System;

namespace SimplePad.App;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        InitializeComponent();
    }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        IAppWindowManager appWindowManager = _serviceProvider.GetRequiredService<IAppWindowManager>();

        IAppWindow appWindow = await appWindowManager.ShowNewWindowAsync();
        appWindow.Execute(window => window.TabRoot.AddBlankTab());
    }
}
