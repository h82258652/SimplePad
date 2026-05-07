using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimplePad.Editor;

internal sealed class WPFEditorSettings : IEditorSettings
{
    public bool IsSpellCheckEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsWordWrap { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event EventHandler<bool>? IsSpellCheckEnabledChanged;
    public event EventHandler<bool>? IsWordWrapChanged;

    public Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
