using System;

namespace SimplePad.StatusBar.Settings;

public interface IStatusBarSettings
{
    bool IsStatusBarVisible { get; set; }

    event EventHandler<bool>? IsStatusBarVisibleChanged;
}
