using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SimplePad.Core;
using SimplePad.Core.Extensions;

namespace SimplePad.Fonts;

public sealed partial class PreviewFontTextControl : UserControl
{
    private readonly DispatcherQueue _dispatcherQueue;
    private readonly IFontSettings _fontSettings;

    public PreviewFontTextControl()
    {
        _dispatcherQueue = DispatcherQueue;
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
        await _dispatcherQueue.SafeRunAsync(UpdateFontFamily);
    }

    private async void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        await _dispatcherQueue.SafeRunAsync(UpdateFontSize);
    }

    private async void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        await _dispatcherQueue.SafeRunAsync(UpdateFontStyle);
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
        PreviewText.FontStyle = _fontSettings.FontStyle.GetWinUIFontStyle();
        PreviewText.FontWeight = _fontSettings.FontStyle.GetWinUIFontWeight();
    }
}