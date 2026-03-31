using System;
using SimplePad.Core.Settings;

namespace SimplePad.StatusBar.Settings;

public interface IStatusBarSettings : IAppSettings
{
    bool IsStatusBarVisible { get; set; }

    event EventHandler<bool>? IsStatusBarVisibleChanged;
}
