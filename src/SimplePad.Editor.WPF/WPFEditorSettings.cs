using System;
using System.Threading.Tasks;

namespace SimplePad.Editor;

internal sealed class WPFEditorSettings : IEditorSettings
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

    public Task LoadAsync()
    {
        IsSpellCheckEnabled = Properties.Settings.Default.IsSpellCheckEnabled;
        IsWordWrap = Properties.Settings.Default.IsWordWrap;
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        Properties.Settings.Default.IsSpellCheckEnabled = IsSpellCheckEnabled;
        Properties.Settings.Default.IsWordWrap = IsWordWrap;
        Properties.Settings.Default.Save();
        return Task.CompletedTask;
    }
}