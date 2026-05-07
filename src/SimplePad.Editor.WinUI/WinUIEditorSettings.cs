using Microsoft.Windows.Storage;
using SimplePad.Core.Settings;
using System;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace SimplePad.Editor;

internal sealed class WinUIEditorSettings : AppSettingsBase, IEditorSettings
{
    private bool _isSpellCheckEnabled = true;
    private bool _isWordWrap = true;

    public event EventHandler<bool>? IsSpellCheckEnabledChanged;

    public event EventHandler<bool>? IsWordWrapChanged;

    public bool IsSpellCheckEnabled
    {
        get => _isSpellCheckEnabled;
        set
        {
            if (_isSpellCheckEnabled != value)
            {
                _isSpellCheckEnabled = value;
                IsSpellCheckEnabledChanged?.Invoke(this, value);
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
                IsWordWrapChanged?.Invoke(this, value);
            }
        }
    }

    public override Task LoadAsync()
    {
        IPropertySet settingsValue = GetSettings();
        if (settingsValue.TryGetValue(nameof(IsSpellCheckEnabled), out object? isSpellCheckEnabled))
        {
            IsSpellCheckEnabled = (bool)isSpellCheckEnabled;
        }

        if (settingsValue.TryGetValue(nameof(IsWordWrap), out object? isWordWrap))
        {
            IsWordWrap = (bool)isWordWrap;
        }

        return Task.CompletedTask;
    }

    public override Task SaveAsync()
    {
        IPropertySet settingsValue = GetSettings();
        settingsValue[nameof(IsSpellCheckEnabled)] = IsSpellCheckEnabled;
        settingsValue[nameof(IsWordWrap)] = IsWordWrap;

        return Task.CompletedTask;
    }

    private static IPropertySet GetSettings()
    {
        return ApplicationData.GetDefault().LocalSettings.CreateContainer("Editor", ApplicationDataCreateDisposition.Always).Values;
    }
}
