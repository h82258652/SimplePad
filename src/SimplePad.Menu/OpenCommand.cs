using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.File;
using SimplePad.Tabs;
using SimplePad.Windowing;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimplePad.Menu;

public sealed class OpenCommand : ICommand
{
    private readonly IAppWindowManager _appWindowManager;
    private readonly IFilePickerService _filePickerService;
    private readonly ITabsSettings _tabSettings;

    public OpenCommand()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();
        _filePickerService = ServiceLocator.Current.GetRequiredService<IFilePickerService>();
        _tabSettings = ServiceLocator.Current.GetRequiredService<ITabsSettings>();
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
        if (_appWindowManager.CurrentWindow is not { TabRoot: { } tabRoot })
        {
            return;
        }

        IFile? file = parameter as IFile;
        file ??= await _filePickerService.PickOpenFileAsync();
        if (file is null)
        {
            return;
        }

        if (TryShowExistTab(file))
        {
            return;
        }

        if (_tabSettings.OpenFileBehavior == OpenFileBehavior.NewTab)
        {
            OpenFileOnNewTab(tabRoot, file);
        }
        else if (_tabSettings.OpenFileBehavior == OpenFileBehavior.NewWindow)
        {
            await OpenFileOnNewWindowAsync(file);
        }
    }

    public async void ExecuteWithFile(IFile file)
    {
        if (!TryShowExistTab(file))
        {
            await OpenFileOnNewWindowAsync(file);
        }
    }
    private static void OpenFileOnNewTab(TabRoot tabRoot, IFile file)
    {
        tabRoot.AddTabFromFile(file);
    }

    private async Task OpenFileOnNewWindowAsync(IFile file)
    {
        IAppWindow newAppWindow = await _appWindowManager.ShowNewWindowAsync();
        newAppWindow.Execute(appWindow => appWindow.TabRoot.AddTabFromFile(file));
    }

    private bool TryShowExistTab(IFile file)
    {
        foreach (IAppWindow appWindow in _appWindowManager.Instances)
        {
            foreach (Tab tab in appWindow.TabRoot.Tabs)
            {
                if (tab.File is not { } tabFile)
                {
                    continue;
                }

                if (tabFile.Path == file.Path)
                {
                    appWindow.Execute(async window =>
                    {
                        window.TabRoot.SelectedTab = tab;
                        await window.ShowAsync();
                    });
                    return true;
                }
            }
        }

        return false;
    }
}