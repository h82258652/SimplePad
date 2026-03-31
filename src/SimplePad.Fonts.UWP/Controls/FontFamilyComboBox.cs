using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graphics.Canvas.Text;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Fonts.Settings;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Fonts.UWP.Controls;

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
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Fonts.UWP/Controls/FontFamilyComboBox.xaml"
        );

        ItemsSource = _systemFontFamilies;
        SelectedItem = _fontSettings.FontFamily;

        IsEditable = true;

        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private async void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        await Dispatcher.SafeRunAsync(() =>
        {
            SelectedItem = e;
        });
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
            SelectedItem = _fontSettings.FontFamily;
        }
    }
}
