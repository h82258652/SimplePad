using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

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

    private readonly IFontSettings _fontSettings;

    public FontSizeComboBox()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();

        DefaultStyleKey = typeof(FontSizeComboBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Fonts.Settings.WinUI/FontSizeComboBox.xaml");

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

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        UpdateSelectedItem();
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

    private void UpdateSelectedItem()
    {
        SelectedItem = Items
            .OfType<ComboBoxItem>()
            .FirstOrDefault(comboBoxItem => Equals(comboBoxItem.Content, _fontSettings.FontSize));
    }
}