using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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

    private readonly Dispatcher _dispatcher;
    private readonly IFontSettings _fontSettings;

    public FontSizeComboBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();

        for (int i = AppFontSizeConstants.Minimum; i <= AppFontSizeConstants.Maximum; i++)
        {
            ComboBoxItem comboBoxItem = new()
            {
                Visibility = VisibleFontSizeList.Contains(i) ? Visibility.Visible : Visibility.Collapsed,
                Content = i,
                IsSelected = _fontSettings.FontSize == i
            };
            Items.Add(comboBoxItem);
        }

        IsEditable = true;

        _fontSettings.FontSizeChanged += OnFontSettingsFontSizeChanged;
        SelectionChanged += OnSelectionChanged;
    }

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        _dispatcher.Invoke(UpdateSelectedItem);
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

    private void UpdateSelectedItem()
    {
        SelectedItem = Items
            .OfType<ComboBoxItem>()
            .FirstOrDefault(comboBoxItem => Equals(comboBoxItem.Content, _fontSettings.FontSize));
    }
}