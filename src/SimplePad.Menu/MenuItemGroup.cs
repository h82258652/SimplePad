using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePad.Menu;

public sealed class MenuItemGroup : MenuItemBase
{
    public List<MenuItemBase> Children { get; set; } = [];

    public MenuItemGroup(string text)
    {
        Text = text;
    }

    public string Text { get; }
}
