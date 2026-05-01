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
    private readonly AppFontStyle[] _items;

    public FontStyleComboBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _items =
        [
            AppFontStyle.Regular,
            AppFontStyle.Italic,
            AppFontStyle.Bold,
            AppFontStyle.BoldItalic
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
        if (SelectedItem is AppFontStyle selectedItem)
        {
            _fontSettings.FontStyle = selectedItem;
        }
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _items.FirstOrDefault(item => item.Value == _fontSettings.FontStyle.Value);
    }
}