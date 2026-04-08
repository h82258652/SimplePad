using System;
using SimplePad.Tabs;
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

    public OpenMenuFlyoutItem()
    {
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

        // TODO

        tabRoot.AddTabFromFile(file);
    }
}