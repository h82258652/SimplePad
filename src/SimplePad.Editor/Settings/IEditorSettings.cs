using System;
using SimplePad.Core.Settings;

namespace SimplePad.Editor.Settings;

public interface IEditorSettings : IAppSettings
{
    event EventHandler<bool>? IsSpellCheckEnabledChanged;

    event EventHandler<bool>? IsWordWrapChanged;

    bool IsSpellCheckEnabled { get; set; }

    bool IsWordWrap { get; set; }
}
