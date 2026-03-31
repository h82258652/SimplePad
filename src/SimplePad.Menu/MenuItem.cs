using System;

namespace SimplePad.Menu;

public sealed class MenuItem : MenuItemBase
{
    public MenuItem(string text, Action<IServiceProvider> action)
    {
        Text = text;
        Action = action;
    }

    public Action<IServiceProvider> Action { get; }

    public string Text { get; }
}
