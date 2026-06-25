using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePad.Menu;

public partial class ExitMenuItem : MenuItem
{
    private readonly IAppWindowManager _appWindowManager;

    public ExitMenuItem()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
    }

    private async void OnClick(object? sender, RoutedEventArgs e)
    {
        await Task.WhenAll(_appWindowManager.Instances.Select(_appWindowManager.CloseAsync));
    }
}