using System;

namespace SimplePad.Menu;

public sealed class ToggleMenuItem : MenuItemBase
{
    public ToggleMenuItem(
        string text,
        Func<IServiceProvider, bool> isChecked,
        Action<IServiceProvider, bool> action
    )
    {
        Text = text;
        IsChecked = isChecked;
        Action = action;
    }

    public Action<IServiceProvider, bool> Action { get; }

    public Func<IServiceProvider, bool> IsChecked { get; set; }

    public string Text { get; }
}
