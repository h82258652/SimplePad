using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Tabs;

public sealed partial class OpenFileBehaviorComboBox : ComboBox
{
    private readonly OpenFileBehavior[] _items;
    private readonly ITabsSettings _tabsSettings;

    public OpenFileBehaviorComboBox()
    {
        _tabsSettings = ServiceLocator.Current.GetRequiredService<ITabsSettings>();
        _items =
        [
            OpenFileBehavior.NewTab,
            OpenFileBehavior.NewWindow
        ];

        DefaultStyleKey = typeof(OpenFileBehaviorComboBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Tabs.Settings.WinUI/OpenFileBehaviorComboBox.xaml");

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

    private void OnTabsSettingsOpenFileBehaviorChanged(object sender, OpenFileBehavior e)
    {
        UpdateSelectedItem();
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item => item.Value == _tabsSettings.OpenFileBehavior.Value);
    }
}
