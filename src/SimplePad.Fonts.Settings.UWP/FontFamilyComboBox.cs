using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graphics.Canvas.Text;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Fonts;

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
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Fonts.Settings.UWP/FontFamilyComboBox.xaml");

        ItemsSource = _systemFontFamilies;
        UpdateSelectedItem();

        IsEditable = true;

        SelectionChanged += OnSelectionChanged;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private async void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        await _dispatcher.SafeRunAsync(UpdateSelectedItem);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;

        UpdateSelectedItem();
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

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _fontSettings.FontFamilyChanged -= OnFontSettingsFontFamilyChanged;
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = _fontSettings.FontFamily;
    }
}