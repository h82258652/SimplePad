using System;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Fonts.Settings;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SimplePad.Fonts.UWP.Controls;

public sealed partial class PreviewFontTextControl : UserControl
{
    private readonly IFontSettings _fontSettings;

    public PreviewFontTextControl()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();

        InitializeComponent();

        UpdateFontFamily();
        UpdateFontStyle();
        UpdateFontSize();

        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;
        _fontSettings.FontStyleChanged += OnFontSettingsFontStyleChanged;
        _fontSettings.FontSizeChanged += OnFontSettingsFontSizeChanged;
    }

    private async void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        await Dispatcher.SafeRunAsync(UpdateFontFamily);
    }

    private async void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        await Dispatcher.SafeRunAsync(UpdateFontSize);
    }

    private async void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        await Dispatcher.SafeRunAsync(UpdateFontStyle);
    }

    private void UpdateFontFamily()
    {
        PreviewText.FontFamily = new FontFamily(_fontSettings.FontFamily);
    }

    private void UpdateFontSize()
    {
        PreviewText.FontSize = _fontSettings.FontSize;
    }

    private void UpdateFontStyle()
    {
        PreviewText.FontStyle = _fontSettings.FontStyle switch
        {
            AppFontStyle.Regular or AppFontStyle.Bold => FontStyle.Normal,
            AppFontStyle.Italic or AppFontStyle.BoldItalic => FontStyle.Italic,
            _ => throw new ArgumentOutOfRangeException()
        };

        PreviewText.FontWeight = _fontSettings.FontStyle switch
        {
            AppFontStyle.Regular or AppFontStyle.Italic => FontWeights.Normal,
            AppFontStyle.Bold or AppFontStyle.BoldItalic => FontWeights.Bold,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
