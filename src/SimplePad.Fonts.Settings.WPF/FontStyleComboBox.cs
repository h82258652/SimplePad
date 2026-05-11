using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Fonts;

public sealed class FontStyleComboBox : ComboBox
{
    private readonly Dispatcher _dispatcher;
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

        ItemsSource = _items;
        UpdateSelectedItem();

        _fontSettings.FontStyleChanged += OnFontSettingsFontStyleChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        _dispatcher.Invoke(UpdateSelectedItem);
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