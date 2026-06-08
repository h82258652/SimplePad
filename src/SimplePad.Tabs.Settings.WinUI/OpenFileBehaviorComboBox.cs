using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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
        throw new NotImplementedException();
    }

    private void OnTabsSettingsOpenFileBehaviorChanged(object sender, OpenFileBehavior e)
    {
        throw new NotImplementedException();
    }

    private void UpdateSelectedItem()
    {
        throw new NotImplementedException();
    }
}
