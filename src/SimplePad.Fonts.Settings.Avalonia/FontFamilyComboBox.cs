using Avalonia.Controls;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using System.Linq;

namespace SimplePad.Fonts;

public sealed class FontFamilyComboBox : ComboBox
{
    private readonly IFontSettings _fontSettings;
    private readonly string[] _systemFontFamilies;

    public FontFamilyComboBox()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _systemFontFamilies = [.. FontManager.Current.SystemFonts.Select(font => font.Name)];
        Array.Sort(_systemFontFamilies);

        ItemsSource = _systemFontFamilies;
        UpdateSelectedItem();

        IsEditable = true;

        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        UpdateSelectedItem();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (
            SelectedItem is string selectedFontFamily
            && _systemFontFamilies.Contains(selectedFontFamily)
        )
        {
            _fontSettings.FontFamily = selectedFontFamily;
        }
        else
        {
            UpdateSelectedItem();
        }
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _fontSettings.FontFamily;
    }
}