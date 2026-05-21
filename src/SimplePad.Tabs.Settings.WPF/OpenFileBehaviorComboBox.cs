using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Tabs;

public sealed class OpenFileBehaviorComboBox : ComboBox
{
    private readonly Dispatcher _dispatcher;
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

    private void OnTabsSettingsOpenFileBehaviorChanged(object? sender, OpenFileBehavior e)
    {
        _dispatcher.Invoke(UpdateSelectedItem);
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item => item.Value == _tabsSettings.OpenFileBehavior.Value);
    }
}