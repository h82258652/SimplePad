using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Fonts;

public sealed class FontFamilyComboBox : ComboBox
{
    private readonly Dispatcher _dispatcher;
    private readonly IFontSettings _fontSettings;
    private readonly string[] _systemFontFamilies;

    public FontFamilyComboBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _systemFontFamilies = [.. System.Windows.Media.Fonts.SystemFontFamilies.Select(fontFamily => fontFamily.Source)];
        Array.Sort(_systemFontFamilies);

        ItemsSource = _systemFontFamilies;
        UpdateSelectedItem();

        IsEditable = true;

        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        _dispatcher.Invoke(UpdateSelectedItem);
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
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