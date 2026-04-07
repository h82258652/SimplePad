using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Fonts;

public sealed partial class FontStyleComboBox : ComboBox
{
    private readonly CoreDispatcher _dispatcher;
    private readonly IFontSettings _fontSettings;
    private readonly FontStyleComboBoxItem[] _items;

    public FontStyleComboBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _items =
        [
            new FontStyleComboBoxItem(AppFontStyle.Regular, "Regular"),
            new FontStyleComboBoxItem(AppFontStyle.Italic, "Italic"),
            new FontStyleComboBoxItem(AppFontStyle.Bold, "Bold"),
            new FontStyleComboBoxItem(AppFontStyle.BoldItalic, "Bold Italic"),
        ];

        DefaultStyleKey = typeof(FontStyleComboBox);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Fonts.Settings.UWP/FontStyleComboBox.xaml"
        );

        ItemsSource = _items;
        UpdateSelectedItem();

        _fontSettings.FontStyleChanged += OnFontSettingsFontStyleChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private async void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        await _dispatcher.SafeRunAsync(UpdateSelectedItem);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectedItem is FontStyleComboBoxItem selectedItem)
        {
            _fontSettings.FontStyle = selectedItem.Value;
        }
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item => item.Value == _fontSettings.FontStyle);
    }
}