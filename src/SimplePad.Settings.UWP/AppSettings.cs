using CommunityToolkit.Mvvm.ComponentModel;
using Windows.Storage;

namespace SimplePad.Settings.UWP;

public sealed partial class AppSettings : ObservableObject, IAppSettings
{
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
}
