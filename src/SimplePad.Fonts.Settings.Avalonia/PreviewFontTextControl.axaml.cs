using Avalonia.Controls;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Fonts;

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

    private void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        UpdateFontFamily();
    }

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        UpdateFontSize();
    }

    private void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        UpdateFontStyle();
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
        PreviewText.FontStyle = _fontSettings.FontStyle.GetAvaloniaFontStyle();
        PreviewText.FontWeight = _fontSettings.FontStyle.GetAvaloniaFontWeight();
    }
}