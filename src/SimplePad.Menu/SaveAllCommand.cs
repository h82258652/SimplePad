using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public sealed class SaveAllCommand : ICommand
{
    private readonly IAppWindowManager _appWindowManager;
    private readonly TabManager _tabManager;

    public SaveAllCommand()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();
    }

    public event EventHandler? CanExecuteChanged
    {
        add
        {
        }
        remove
        {
        }
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        if (_appWindowManager.CurrentWindow is not { } currentWindow)
        {
            return;
        }

        TabRoot tabRoot = currentWindow.TabRoot;
        foreach (Tab tab in tabRoot.Tabs)
        {
            if (!await _tabManager.SaveAsync(tab))
            {
                return;
            }
        }
    }
}