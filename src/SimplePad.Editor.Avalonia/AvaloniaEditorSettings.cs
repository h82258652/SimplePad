using SimplePad.Core.Settings;
using System;
using System.Collections.Generic;

namespace SimplePad.Editor;

internal sealed class AvaloniaEditorSettings : AppSettingsBase, IEditorSettings
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

    protected override Dictionary<string, object?> GetSettings()
    {
        return new Dictionary<string, object?>()
        {
            { nameof(IsSpellCheckEnabled), IsSpellCheckEnabled },
            { nameof(IsWordWrap), IsWordWrap }
        };
    }

    protected override void SetSettings(Dictionary<string, object?> settings)
    {
        if (settings.TryGetValue(nameof(IsSpellCheckEnabled), out var isSpellCheckEnabled) && isSpellCheckEnabled is bool spellCheckEnabled)
        {
            IsSpellCheckEnabled = spellCheckEnabled;
        }

        if (settings.TryGetValue(nameof(IsWordWrap), out var isWordWrap) && isWordWrap is bool wordWrap)
        {
            IsWordWrap = wordWrap;
        }
    }
}