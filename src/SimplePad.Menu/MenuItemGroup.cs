using System.Collections.Generic;

namespace SimplePad.Menu;

public sealed class MenuItemGroup : MenuItemBase
{
    public MenuItemGroup(string text)
    {
        Text = text;
    }

    public List<MenuItemBase> Children { get; set; } = [];

    public string Text { get; }
}
