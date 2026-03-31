using System.Collections.Generic;

namespace SimplePad.Menu;

public sealed class MenuBarOptions
{
    public List<MenuItemBase> EditItems { get; set; } = [];

    public List<MenuItemBase> FileItems { get; set; } = [];

    public List<MenuItemBase> ViewItems { get; set; } = [];
}
