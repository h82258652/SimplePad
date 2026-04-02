using System;
using SimplePad.Core.Settings;

namespace SimplePad.StatusBar;

public interface IStatusBarSettings : IAppSettings
{
    bool IsStatusBarVisible { get; set; }

    event EventHandler<bool>? IsStatusBarVisibleChanged;
}
