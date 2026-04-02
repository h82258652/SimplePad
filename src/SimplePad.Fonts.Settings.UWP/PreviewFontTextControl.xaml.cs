using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SimplePad.Fonts;

public sealed partial class PreviewFontTextControl : UserControl
{
    private readonly IFontSettings _fontSettings;
    private readonly CoreDispatcher _dispatcher;

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

    private async void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        await _dispatcher.SafeRunAsync(UpdateFontFamily);
    }

    private async void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        await _dispatcher.SafeRunAsync(UpdateFontSize);
    }

    private async void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        await _dispatcher.SafeRunAsync(UpdateFontStyle);
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
        PreviewText.FontStyle = _fontSettings.FontStyle.GetUWPFontStyle();
        PreviewText.FontWeight = _fontSettings.FontStyle.GetUWPFontWeight();
    }
}
