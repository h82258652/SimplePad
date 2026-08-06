using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Fonts;

public sealed partial class FontFamilyComboBox : ComboBox
{
    private readonly IFontSettings _fontSettings;
    private readonly string[] _systemFontFamilies;

    public FontFamilyComboBox()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _systemFontFamilies = CanvasTextFormat.GetSystemFontFamilies();
        Array.Sort(_systemFontFamilies);

        DefaultStyleKey = typeof(FontFamilyComboBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Fonts.Settings.WinUI/FontFamilyComboBox.xaml");

        ItemsSource = _systemFontFamilies;
        UpdateSelectedItem();

        IsEditable = true;

        SelectionChanged += OnSelectionChanged;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        UpdateSelectedItem();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;

        UpdateSelectedItem();
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelectedItem is string selectedFontFamily
            && _systemFontFamilies.Contains(selectedFontFamily))
        {
            _fontSettings.FontFamily = selectedFontFamily;
        }
        else
        {
            UpdateSelectedItem();
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _fontSettings.FontFamilyChanged -= OnFontSettingsFontFamilyChanged;
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _fontSettings.FontFamily;
    }
}