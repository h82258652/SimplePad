using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.File;
using SimplePad.Tabs;
using SimplePad.Windowing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class OpenMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TabRootProperty = DependencyProperty.Register(
        nameof(TabRoot),
        typeof(TabRoot),
        typeof(OpenMenuFlyoutItem),
        null);

    private readonly ITabsSettings _tabSettings;
    private readonly IAppWindowManager _appWindowManager;
    private readonly IFilePickerService _filePickerService;

    public OpenMenuFlyoutItem()
    {
        _tabSettings = ServiceLocator.Current.GetRequiredService<ITabsSettings>();
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();
        _filePickerService = ServiceLocator.Current.GetRequiredService<IFilePickerService>();

        InitializeComponent();
    }

    public TabRoot? TabRoot
    {
        get => (TabRoot?)GetValue(TabRootProperty);
        set => SetValue(TabRootProperty, value);
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        if (TabRoot is not { } tabRoot)
        {
            return;
        }

        IFile? file = await _filePickerService.PickOpenFileAsync();
        if (file is null)
        {
            return;
        }

        if (_tabSettings.OpenFileBehavior == OpenFileBehavior.NewTab)
        {
            tabRoot.AddTabFromFile(file);
        }
        else if (_tabSettings.OpenFileBehavior == OpenFileBehavior.NewWindow)
        {
            IAppWindow newAppWindow = await _appWindowManager.ShowNewWindowAsync();
            newAppWindow.Execute(appWindow => appWindow.TabRoot.AddTabFromFile(file));
        }
    }
}