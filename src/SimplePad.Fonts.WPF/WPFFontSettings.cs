using System;
using System.Threading.Tasks;
using SimplePad.Fonts.Properties;

namespace SimplePad.Fonts;

internal sealed class WPFFontSettings : IFontSettings
{
    private string _fontFamily = "Consolas";
    private int _fontSize = 14;
    private AppFontStyle _fontStyle = AppFontStyle.Regular;

    public event EventHandler<string>? FontFamilyChanged;

    public event EventHandler<int>? FontSizeChanged;

    public event EventHandler<AppFontStyle>? FontStyleChanged;

    public string FontFamily
    {
        get => _fontFamily;
        set
        {
            if (_fontFamily != value)
            {
                _fontFamily = value;
                FontFamilyChanged?.Invoke(this, value);
            }
        }
    }

    public int FontSize
    {
        get => _fontSize;
        set
        {
            if (_fontSize != value)
            {
                _fontSize = value;
                FontSizeChanged?.Invoke(this, value);
            }
        }
    }

    public AppFontStyle FontStyle
    {
        get => _fontStyle;
        set
        {
            if (_fontStyle != value)
            {
                _fontStyle = value;
                FontStyleChanged?.Invoke(this, value);
            }
        }
    }

    public Task LoadAsync()
    {
        FontFamily = Settings.Default.FontFamily;
        FontSize = Settings.Default.FontSize;
        FontStyle = AppFontStyle.FromValue(Settings.Default.FontStyle);
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        Settings.Default.FontFamily = FontFamily;
        Settings.Default.FontSize = FontSize;
        Settings.Default.FontStyle = FontStyle.Value;
        Settings.Default.Save();
        return Task.CompletedTask;
    }
}