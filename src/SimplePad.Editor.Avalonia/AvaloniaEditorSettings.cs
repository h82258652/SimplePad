using System;
using System.Threading.Tasks;

namespace SimplePad.Editor;

internal sealed class AvaloniaEditorSettings : IEditorSettings
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
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}