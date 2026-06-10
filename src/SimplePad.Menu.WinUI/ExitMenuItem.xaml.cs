using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public sealed partial class ExitMenuItem : MenuFlyoutItem
{
    private readonly IAppWindowManager _appWindowManager;

    public ExitMenuItem()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        await Task.WhenAll(_appWindowManager.Instances.Select(_appWindowManager.CloseAsync));
    }
}