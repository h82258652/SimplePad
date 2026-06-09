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
    private readonly ITabsSettings _tabsSettings;

    public OpenFileBehaviorComboBox()
    {
        _dispatcher = Dispatcher;
        _tabsSettings = ServiceLocator.Current.GetRequiredService<ITabsSettings>();
        _items =
        [
            OpenFileBehavior.NewTab,
            OpenFileBehavior.NewWindow
        ];

        DefaultStyleKey = typeof(OpenFileBehaviorComboBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Tabs.Settings.UWP/OpenFileBehaviorComboBox.xaml");

        ItemsSource = _items;
        UpdateSelectedItem();

        _tabsSettings.OpenFileBehaviorChanged += OnTabsSettingsOpenFileBehaviorChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectedItem is OpenFileBehavior selectedItem)
        {
            _tabsSettings.OpenFileBehavior = selectedItem;
        }
    }

    private async void OnTabsSettingsOpenFileBehaviorChanged(object? sender, OpenFileBehavior e)
    {
        await _dispatcher.SafeRunAsync(UpdateSelectedItem);
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item => item.Value == _tabsSettings.OpenFileBehavior.Value);
    }
}