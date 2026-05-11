using System;
using System.Windows;
using SimplePad.Core;

namespace SimplePad.Fonts.TestApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        ServiceLocator.SetLocatorProvider(serviceProvider);

        InitializeComponent();
    }
}
