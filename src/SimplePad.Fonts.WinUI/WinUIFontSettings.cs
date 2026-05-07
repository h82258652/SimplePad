using Microsoft.Windows.Storage;
using SimplePad.Core.Settings;
using System;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace SimplePad.Fonts;

internal sealed class WinUIFontSettings : AppSettingsBase, IFontSettings
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

    public override Task LoadAsync()
    {
        IPropertySet settingsValue = GetSettings();
        if (settingsValue.TryGetValue(nameof(FontFamily), out object? fontFamily))
        {
            FontFamily = (string)fontFamily;
        }

        if (settingsValue.TryGetValue(nameof(FontSize), out object? fontSize))
        {
            FontSize = (int)Convert.ChangeType(fontSize, typeof(int));
        }

        if (settingsValue.TryGetValue(nameof(FontStyle), out object? fontStyle))
        {
            FontStyle = AppFontStyle.FromValue((int)fontStyle);
        }

        return Task.CompletedTask;
    }

    public override Task SaveAsync()
    {
        IPropertySet settingsValue = GetSettings();
        settingsValue[nameof(FontFamily)] = FontFamily;
        settingsValue[nameof(FontSize)] = FontSize;
        settingsValue[nameof(FontStyle)] = FontStyle.Value;

        return Task.CompletedTask;
    }

    private static IPropertySet GetSettings()
    {
        return ApplicationData.GetDefault().LocalSettings.CreateContainer("Fonts", ApplicationDataCreateDisposition.Always).Values;
    }
}