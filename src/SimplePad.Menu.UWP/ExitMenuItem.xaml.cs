using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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