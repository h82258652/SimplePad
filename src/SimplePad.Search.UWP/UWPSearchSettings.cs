using System;
using System.Threading.Tasks;
using SimplePad.Core.Settings;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SimplePad.Search;

internal sealed class UWPSearchSettings : AppSettingsBase, ISearchSettings
{
    private bool _isMatchCase;
    private bool _isWrapAround = true;

    public event EventHandler<bool>? IsMatchCaseChanged;

    public event EventHandler<bool>? IsWrapAroundChanged;

    public bool IsMatchCase
    {
        get => _isMatchCase;
        set
        {
            if (_isMatchCase != value)
            {
                _isMatchCase = value;
                IsMatchCaseChanged?.Invoke(this, value);
            }
        }
    }

    public bool IsWrapAround
    {
        get => _isWrapAround;
        set
        {
            if (_isWrapAround != value)
            {
                _isWrapAround = value;
                IsWrapAroundChanged?.Invoke(this, value);
            }
        }
    }

    public override Task LoadAsync()
    {
        IPropertySet settingsValue = ApplicationData.Current.LocalSettings.Values;
        if (settingsValue.TryGetValue(nameof(IsMatchCase), out object? isMatchCase))
        {
            IsMatchCase = (bool)isMatchCase;
        }

        if (settingsValue.TryGetValue(nameof(IsWrapAround), out object? isWrapAround))
        {
            IsWrapAround = (bool)isWrapAround;
        }

        return Task.CompletedTask;
    }

    public override Task SaveAsync()
    {
        IPropertySet settingsValue = ApplicationData.Current.LocalSettings.Values;
        settingsValue[nameof(IsMatchCase)] = IsMatchCase;
        settingsValue[nameof(IsWrapAround)] = IsWrapAround;

        return Task.CompletedTask;
    }
}