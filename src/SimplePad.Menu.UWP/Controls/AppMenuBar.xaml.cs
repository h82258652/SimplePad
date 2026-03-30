using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimplePad.Core;
using System;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu.UWP.Controls;

public sealed partial class AppMenuBar : UserControl
{
    private const double MenuItemMinWidth = 230;

    private readonly IServiceProvider _serviceProvider;

    public AppMenuBar()
    {
        _serviceProvider = ServiceLocator.Current;

        IOptionsSnapshot<MenuBarOptions> menuBarOptionsAccessor = _serviceProvider.GetRequiredService<IOptionsSnapshot<MenuBarOptions>>();
        var menuBarOptions = menuBarOptionsAccessor.Value;

        InitializeComponent();

        BuildFileMenu(menuBarOptions);
        BuildEditMenu(menuBarOptions);
        BuildViewMenu(menuBarOptions);
    }

    private void BuildViewMenu(MenuBarOptions menuBarOptions)
    {
        foreach (var item in menuBarOptions.ViewItems)
        {
            ViewMenuBarItem.Items.Add(BuildMenuItem(item));
        }
    }

    private MenuFlyoutItemBase BuildMenuItem(MenuItemBase item)
    {
        MenuFlyoutItemBase menuItem;

        if (item is MenuItem)
        {
            throw new NotImplementedException();

        }
        else if (item is MenuItemSeparator)
        {
            menuItem = new MenuFlyoutSeparator();

        }
        else if (item is MenuItemGroup itemGroup)
        {
            MenuFlyoutSubItem menuItemGroup = new() { Text = itemGroup.Text };
            foreach (var child in itemGroup.Children)
            {
                menuItemGroup.Items.Add(BuildMenuItem(child));
            }

            menuItem = menuItemGroup;
        }
        else if (item is ToggleMenuItem toggleItem)
        {
            ToggleMenuFlyoutItem toggle = new()
            {
                Text = toggleItem.Text,
                IsEnabled = toggleItem.IsChecked(_serviceProvider)
            };
            // TODO register _appsettings.ischecked changed

            toggle.Click += (sender, e) =>
            {
                toggleItem.Action(_serviceProvider, toggle.IsChecked);
            };

            menuItem = toggle;
        }
        else
        {
            throw new NotSupportedException();
        }

        menuItem.MinWidth = MenuItemMinWidth;
        return menuItem;
    }

    private void BuildEditMenu(MenuBarOptions menuBarOptions)
    {
        throw new NotImplementedException();
    }

    private void BuildFileMenu(MenuBarOptions menuBarOptions)
    {
        throw new NotImplementedException();
    }
}
