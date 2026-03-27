using CommunityToolkit.Mvvm.ComponentModel;
using Windows.Storage;

namespace SimplePad.Settings.UWP;

public sealed partial class AppSettings : ObservableObject, IAppSettings
{
    public AppTheme AppTheme
    {
        get
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(nameof(AppTheme), out object? value))
            {
                return (AppTheme)value;
            }

            return AppTheme.UseSystemSettings;
        }
        set
        {
            ApplicationData.Current.LocalSettings.Values[nameof(AppTheme)] = (int)value;
            OnPropertyChanged();
        }
    }

    public double FontSize
    {
        get => ApplicationData.Current.LocalSettings.Values.TryGetValue(nameof(FontSize), out object? value) ? (double)value : 11;
        set
        {
            ApplicationData.Current.LocalSettings.Values[nameof(FontSize)] = value;
            OnPropertyChanged();
        }
    }

    public AppFontStyle FontStyle
    {
        get
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(nameof(FontStyle), out object? value))
            {
                return (AppFontStyle)value;
            }

            return AppFontStyle.Regular;
        }
        set
        {
            if (FontStyle != value)
            {
                ApplicationData.Current.LocalSettings.Values[nameof(FontStyle)] = (int)value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsSpellCheckEnabled
    {
        get => !ApplicationData.Current.LocalSettings.Values.TryGetValue(nameof(IsSpellCheckEnabled), out object? value) || (bool)value;
        set
        {
            ApplicationData.Current.LocalSettings.Values[nameof(IsSpellCheckEnabled)] = value;
            OnPropertyChanged();
        }
    }

    public bool IsStatusBarVisible
    {
        get => !ApplicationData.Current.LocalSettings.Values.TryGetValue(nameof(IsStatusBarVisible), out object? value) || (bool)value;
        set
        {
            ApplicationData.Current.LocalSettings.Values[nameof(IsStatusBarVisible)] = value;
            OnPropertyChanged();
        }
    }

    public bool IsWordWrap
    {
        get => !ApplicationData.Current.LocalSettings.Values.TryGetValue(nameof(IsWordWrap), out object? value) || (bool)value;
        set
        {
            ApplicationData.Current.LocalSettings.Values[nameof(IsWordWrap)] = value;
            OnPropertyChanged();
        }
    }

    public OpenFileBehavior OpenFileBehavior
    {
        get
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(nameof(OpenFileBehavior), out object? value))
            {
                return (OpenFileBehavior)value;
            }

            return OpenFileBehavior.NewTab;
        }
        set
        {
            if (OpenFileBehavior != value)
            {
                ApplicationData.Current.LocalSettings.Values[nameof(OpenFileBehavior)] = (int)value;
                OnPropertyChanged();
            }
        }
    }
}
