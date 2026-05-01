using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Tabs;

public sealed partial class OpenFileBehaviorComboBox : ComboBox
{
    private readonly CoreDispatcher _dispatcher;
    private readonly OpenFileBehavior[] _items;
    private readonly ITabsSettings _multiTabSettings;

    public OpenFileBehaviorComboBox()
    {
        _dispatcher = Dispatcher;
        _multiTabSettings = ServiceLocator.Current.GetRequiredService<ITabsSettings>();
        _items =
        [
            OpenFileBehavior.NewTab,
            OpenFileBehavior.NewWindow
        ];

        DefaultStyleKey = typeof(OpenFileBehaviorComboBox);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Tabs.Settings.UWP/OpenFileBehaviorComboBox.xaml"
        );

        ItemsSource = _items;
        UpdateSelectedItem();

        _multiTabSettings.OpenFileBehaviorChanged += OnMultiTabSettingsOpenFileBehaviorChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private async void OnMultiTabSettingsOpenFileBehaviorChanged(object? sender, OpenFileBehavior e)
    {
        await _dispatcher.SafeRunAsync(UpdateSelectedItem);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectedItem is OpenFileBehavior selectedItem)
        {
            _multiTabSettings.OpenFileBehavior = selectedItem;
        }
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item =>
            item.Value == _multiTabSettings.OpenFileBehavior.Value
        );
    }
}