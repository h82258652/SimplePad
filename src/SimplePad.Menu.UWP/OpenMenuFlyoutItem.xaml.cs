using System;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.File;
using SimplePad.Tabs;
using SimplePad.Windowing;
using Windows.Storage;
using Windows.Storage.Pickers;
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

    public OpenMenuFlyoutItem()
    {
        _tabSettings = ServiceLocator.Current.GetRequiredService<ITabsSettings>();
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

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

        FileOpenPicker fileOpenPicker = new();
        fileOpenPicker.FileTypeFilter.Add(".txt");
        fileOpenPicker.FileTypeFilter.Add("*");
        StorageFile? file = await fileOpenPicker.PickSingleFileAsync();
        if (file is null)
        {
            return;
        }

        if (_tabSettings.OpenFileBehavior == OpenFileBehavior.NewTab)
        {
            tabRoot.AddTabFromFile(new UWPFile(file));
        }
        else if (_tabSettings.OpenFileBehavior == OpenFileBehavior.NewWindow)
        {
            IAppWindow newAppWindow = await _appWindowManager.ShowNewWindowAsync();
            newAppWindow.Execute(appWindow => appWindow.TabRoot.AddTabFromFile(new UWPFile(file)));
        }
    }
}