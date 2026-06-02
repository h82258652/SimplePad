using System;
using System.Linq;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Fonts;

public sealed class FontSizeComboBox : ComboBox
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

        for (int i = AppFontSizeConstants.Minimum; i <= AppFontSizeConstants.Maximum; i++)
        {
            ComboBoxItem comboBoxItem = new()
            {
                IsVisible = VisibleFontSizeList.Contains(i),
                Content = i,
                IsSelected = _fontSettings.FontSize == i,
            };
            Items.Add(comboBoxItem);
        }

        IsEditable = true;

        _fontSettings.FontSizeChanged += OnFontSettingsFontSizeChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        UpdateSelectedItem();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (
            SelectedItem is ComboBoxItem selectedItem
            && selectedItem.Content is int fontSize
            && fontSize >= AppFontSizeConstants.Minimum
            && fontSize <= AppFontSizeConstants.Maximum)
        {
            _fontSettings.FontSize = fontSize;
        }
    }

    private void UpdateSelectedItem()
    {
        SelectedItem = Items.OfType<ComboBoxItem>().FirstOrDefault(comboBoxItem => Equals(comboBoxItem.Content, _fontSettings.FontSize));
    }
}
