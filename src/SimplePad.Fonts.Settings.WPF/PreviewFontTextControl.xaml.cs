using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Fonts;

public partial class PreviewFontTextControl : UserControl
{
    private readonly Dispatcher _dispatcher;
    private readonly IFontSettings _fontSettings;

    public PreviewFontTextControl()
    {
        _dispatcher = Dispatcher;
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
        _dispatcher.Invoke(UpdateFontFamily);
    }

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        _dispatcher.Invoke(UpdateFontSize);
    }

    private void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        _dispatcher.Invoke(UpdateFontStyle);
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
        PreviewText.FontStyle = _fontSettings.FontStyle.GetWPFFontStyle();
        PreviewText.FontWeight = _fontSettings.FontStyle.GetWPFFontWeight();
    }
}