using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Fonts.Settings;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Fonts.UWP.Controls;

public sealed partial class FontStyleComboBox : ComboBox
{
    private readonly IFontSettings _fontSettings;
    private readonly FontStyleComboBoxItem[] _items;

    public FontStyleComboBox()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _items =
        [
            new(AppFontStyle.Regular, "Regular"),
            new(AppFontStyle.Italic, "Italic"),
            new(AppFontStyle.Bold, "Bold"),
            new(AppFontStyle.BoldItalic, "Bold Italic"),
        ];

        DefaultStyleKey = typeof(FontStyleComboBox);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Fonts.UWP/Controls/FontStyleComboBox.xaml"
        );

        ItemsSource = _items;
        UpdateSelectedItem();

        _fontSettings.FontStyleChanged += OnFontSettingsFontStyleChanged;

        SelectionChanged += OnSelectionChanged;
    }

    private async void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        await Dispatcher.SafeRunAsync(UpdateSelectedItem);
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item => item.Value == _fontSettings.FontStyle);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectedItem is FontStyleComboBoxItem selectedItem)
        {
            _fontSettings.FontStyle = selectedItem.Value;
        }
    }
}
