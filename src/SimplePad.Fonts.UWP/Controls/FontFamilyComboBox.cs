using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graphics.Canvas.Text;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Fonts.Settings;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Fonts.UWP.Controls;

public sealed partial class FontFamilyComboBox : ComboBox
{
    private readonly CoreDispatcher _dispatcher;
    private readonly IFontSettings _fontSettings;
    private readonly string[] _systemFontFamilies;

    public FontFamilyComboBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _systemFontFamilies = CanvasTextFormat.GetSystemFontFamilies();
        Array.Sort(_systemFontFamilies);

        DefaultStyleKey = typeof(FontFamilyComboBox);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Fonts.UWP/Controls/FontFamilyComboBox.xaml"
        );

        ItemsSource = _systemFontFamilies;
        UpdateSelectedItem();

        IsEditable = true;

        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private async void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        await _dispatcher.SafeRunAsync(UpdateSelectedItem);
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
