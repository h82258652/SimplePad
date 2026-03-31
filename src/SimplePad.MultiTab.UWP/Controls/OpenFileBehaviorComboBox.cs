using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.MultiTab.Settings;
using System;
using Windows.UI.Xaml.Controls;

namespace SimplePad.MultiTab.UWP.Controls;

public sealed partial class OpenFileBehaviorComboBox : ComboBox
{
    private readonly OpenFileBehaviorComboBoxItem[] _items;

    public OpenFileBehaviorComboBox()
    {
        _multiTabSettings = ServiceLocator.Current.GetRequiredService<IMultiTabSettings>();
        _items =
            [
            new( OpenFileBehavior.NewTab, ""),
            new (OpenFileBehavior.NewWindow , ""),
            ];

        DefaultStyleKey = typeof(OpenFileBehaviorComboBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.MultiTab.UWP/Controls/OpenFileBehaviorComboBox.xaml");

        _multiTabSettings.OpenFileBehaviorChanged += OnMultiTabSettingsOpenFileBehaviorChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnMultiTabSettingsOpenFileBehaviorChanged(object? sender, OpenFileBehavior e)
    {
        throw new NotImplementedException();
    }

    private readonly IMultiTabSettings _multiTabSettings;
}
