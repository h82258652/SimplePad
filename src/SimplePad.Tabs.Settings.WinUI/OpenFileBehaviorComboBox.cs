using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using System.Linq;

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

        ItemsSource = _items;
        UpdateSelectedItem();

        SelectionChanged += OnSelectionChanged;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _tabsSettings.OpenFileBehaviorChanged += OnTabsSettingsOpenFileBehaviorChanged;

        UpdateSelectedItem();
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

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _tabsSettings.OpenFileBehaviorChanged -= OnTabsSettingsOpenFileBehaviorChanged;
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item => item.Value == _tabsSettings.OpenFileBehavior.Value);
    }
}