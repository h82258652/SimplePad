using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SimplePad.Settings.UWP;

public sealed partial class AppSettings : IAppSettings
{
    private bool _isSpellCheckEnabled = true;
    private bool _isStatusBarVisible = true;
    private bool _isWordWrap = true;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsSpellCheckEnabled
    {
        get => _isSpellCheckEnabled;
        set
        {
            if (_isSpellCheckEnabled != value)
            {
                _isSpellCheckEnabled = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(IsSpellCheckEnabled))
                );
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
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(IsStatusBarVisible))
                );
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

    public Task LoadAsync()
    {
        IPropertySet settingValues = ApplicationData.Current.LocalSettings.Values;

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

        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        IPropertySet settingValues = ApplicationData.Current.LocalSettings.Values;

        settingValues[nameof(IsSpellCheckEnabled)] = IsSpellCheckEnabled;
        settingValues[nameof(IsStatusBarVisible)] = IsStatusBarVisible;
        settingValues[nameof(IsWordWrap)] = IsWordWrap;

        return Task.CompletedTask;
    }
}
