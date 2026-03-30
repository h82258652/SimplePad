using System;
using System.Collections.Generic;
using System.Text;
using SimplePad.Themes.Settings;

namespace SimplePad.Themes.UWP.Settings;

public sealed class UWPThemeSettings : IThemeSettings
{
    public AppTheme AppTheme { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event EventHandler<AppTheme>? AppThemeChanged;
}
