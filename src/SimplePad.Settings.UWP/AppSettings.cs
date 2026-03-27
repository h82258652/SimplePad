using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SimplePad.Settings.UWP;

public sealed partial class AppSettings : IAppSettings
{
    private AppTheme _appTheme = AppTheme.UseSystemSettings;
    private string _fontFamily = "Consolas";
    private double _fontSize = 11;
    private AppFontStyle _fontStyle = AppFontStyle.Regular;
    private bool _isSpellCheckEnabled = true;
    private bool _isStatusBarVisible = true;
    private bool _isWordWrap = true;
    private OpenFileBehavior _openFileBehavior = OpenFileBehavior.NewTab;

    public event PropertyChangedEventHandler? PropertyChanged;

    public AppTheme AppTheme
    {
        get => _appTheme;
        set
        {
            if (_appTheme != value)
            {
                _appTheme = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AppTheme)));
            }
        }
    }

    public string FontFamily
    {
        get => _fontFamily;
        set
        {
            if (_fontFamily != value)
            {
                _fontFamily = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontFamily)));
            }
        }
    }

    public double FontSize
    {
        get => _fontSize;
        set
        {
            if (_fontSize != value)
            {
                _fontSize = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontSize)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FontStyle)));
            }
        }
    }

    public bool IsSpellCheckEnabled
    {
        get => _isSpellCheckEnabled;
        set
        {
            if (_isSpellCheckEnabled != value)
            {
                _isSpellCheckEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSpellCheckEnabled)));
            }
        }
    }

    public bool IsStatusBarVisible
    {
        get => _isStatusBarVisible;
        set
        {
            if (_isStatusBarVisible != value)
            {
                _isStatusBarVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsStatusBarVisible)));
            }
        }
    }

    public bool IsWordWrap
    {
        get => _isWordWrap;
        set
        {
            if (_isWordWrap != value)
            {
                _isWordWrap = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsWordWrap)));
            }
        }
    }

    public OpenFileBehavior OpenFileBehavior
    {
        get => _openFileBehavior;
        set
        {
            if (_openFileBehavior != value)
            {
                _openFileBehavior = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OpenFileBehavior)));
            }
        }
    }

    public Task LoadAsync()
    {
        IPropertySet settingValues = ApplicationData.Current.LocalSettings.Values;

        if (settingValues.TryGetValue(nameof(AppTheme), out object? appTheme))
        {
            AppTheme = (AppTheme)appTheme;
        }

        if (settingValues.TryGetValue(nameof(FontFamily), out object? fontFamily))
        {
            FontFamily = (string)fontFamily;
        }

        if (settingValues.TryGetValue(nameof(FontSize), out object? fontSize))
        {
            FontSize = (double)fontSize;
        }

        if (settingValues.TryGetValue(nameof(FontStyle), out object? fontStyle))
        {
            FontStyle = (AppFontStyle)fontStyle;
        }

        if (settingValues.TryGetValue(nameof(IsSpellCheckEnabled), out object? isSpellCheckEnabled))
        {
            IsSpellCheckEnabled = (bool)isSpellCheckEnabled;
        }

        if (settingValues.TryGetValue(nameof(IsStatusBarVisible), out object? isStatusBarVisible))
        {
            IsStatusBarVisible = (bool)isStatusBarVisible;
        }

        if (settingValues.TryGetValue(nameof(IsWordWrap), out object? isWordWrap))
        {
            IsWordWrap = (bool)isWordWrap;
        }

        if (settingValues.TryGetValue(nameof(OpenFileBehavior), out object? openFileBehavior))
        {
            OpenFileBehavior = (OpenFileBehavior)openFileBehavior;
        }

        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        IPropertySet settingValues = ApplicationData.Current.LocalSettings.Values;

        settingValues[nameof(AppTheme)] = (int)AppTheme;
        settingValues[nameof(FontFamily)] = FontFamily;
        settingValues[nameof(FontSize)] = FontSize;
        settingValues[nameof(FontStyle)] = (int)FontStyle;
        settingValues[nameof(IsSpellCheckEnabled)] = IsSpellCheckEnabled;
        settingValues[nameof(IsStatusBarVisible)] = IsStatusBarVisible;
        settingValues[nameof(IsWordWrap)] = IsWordWrap;
        settingValues[nameof(OpenFileBehavior)] = (int)OpenFileBehavior;

        return Task.CompletedTask;
    }
}