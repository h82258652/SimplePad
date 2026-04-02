using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Fonts;

public sealed partial class FontSizeComboBox : ComboBox
{
    private static readonly int[] VisibleFontSizeList =
    [
        8,
        9,
        10,
        11,
        12,
        14,
        16,
        18,
        20,
        22,
        24,
        26,
        28,
        36,
        48,
        72,
    ];

    private readonly CoreDispatcher _dispatcher;
    private readonly IFontSettings _fontSettings;

    public FontSizeComboBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();

        DefaultStyleKey = typeof(FontSizeComboBox);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Fonts.Settings.UWP/Controls/FontSizeComboBox.xaml"
        );

        for (int i = AppFontSizeConstants.Minimum; i <= AppFontSizeConstants.Maximum; i++)
        {
            ComboBoxItem comboBoxItem = new()
            {
                Visibility = VisibleFontSizeList.Contains(i)
                    ? Visibility.Visible
                    : Visibility.Collapsed,
                Content = i,
                IsSelected = _fontSettings.FontSize == i,
            };
            Items.Add(comboBoxItem);
        }

        IsEditable = true;

        _fontSettings.FontSizeChanged += OnFontSettingsFontSizeChanged;
        TextSubmitted += OnTextSubmitted;
        SelectionChanged += OnSelectionChanged;
    }

    private async void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        await _dispatcher.SafeRunAsync(UpdateSelectedItem);
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = Items
            .OfType<ComboBoxItem>()
            .FirstOrDefault(comboBoxItem => Equals(comboBoxItem.Content, _fontSettings.FontSize));
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (
            SelectedItem is ComboBoxItem selectedItem
            && selectedItem.Content is int fontSize
            && fontSize >= AppFontSizeConstants.Minimum
            && fontSize <= AppFontSizeConstants.Maximum
        )
        {
            _fontSettings.FontSize = fontSize;
        }
    }

    private void OnTextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
    {
        if (int.TryParse(args.Text, out int fontSize))
        {
            fontSize = Math.Clamp(
                fontSize,
                AppFontSizeConstants.Minimum,
                AppFontSizeConstants.Maximum
            );
            _fontSettings.FontSize = fontSize;
        }

        UpdateSelectedItem();
    }
}