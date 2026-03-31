using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.MultiTab.Settings;
using System;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace SimplePad.MultiTab.UWP.Controls;

public sealed partial class OpenFileBehaviorComboBox : ComboBox
{
    private readonly OpenFileBehaviorComboBoxItem[] _items;

    private readonly IMultiTabSettings _multiTabSettings;

    public OpenFileBehaviorComboBox()
    {
        _multiTabSettings = ServiceLocator.Current.GetRequiredService<IMultiTabSettings>();
        _items =
            [
            new( OpenFileBehavior.NewTab, "Open in a new tab"),
            new (OpenFileBehavior.NewWindow , "Open in a new window"),
            ];

        DefaultStyleKey = typeof(OpenFileBehaviorComboBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.MultiTab.UWP/Controls/OpenFileBehaviorComboBox.xaml");

        ItemsSource = _items;
        UpdateSelectedItem();

        _multiTabSettings.OpenFileBehaviorChanged += OnMultiTabSettingsOpenFileBehaviorChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item => item.Value == _multiTabSettings.OpenFileBehavior);

    }

    private async void OnMultiTabSettingsOpenFileBehaviorChanged(object? sender, OpenFileBehavior e)
    {
await        Dispatcher.SafeRunAsync(UpdateSelectedItem);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectedItem is OpenFileBehaviorComboBoxItem selectedItem)
        {
            _multiTabSettings.OpenFileBehavior = selectedItem.Value;
        }
    }
}
