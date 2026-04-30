using System.Threading;
using System.Threading.Tasks;
using SimplePad.File;

namespace SimplePad.Tabs;

public sealed class TabManager
{
    private readonly IConfirmCloseService _confirmCloseService;
    private readonly IFilePickerService _filePickerService;
    private readonly SemaphoreSlim _saveLock = new(1);

    internal TabManager(IFilePickerService filePickerService, IConfirmCloseService confirmCloseService)
    {
        _filePickerService = filePickerService;
        _confirmCloseService = confirmCloseService;
    }

    public async Task<bool> CloseAsync(Tab tab)
    {
        if (!tab.IsModified)
        {
            tab.Root.Tabs.Remove(tab);
            return true;
        }

        ConfirmCloseResult confirmCloseResult = await _confirmCloseService.ConfirmCloseAsync(tab);
        if (confirmCloseResult == ConfirmCloseResult.Save)
        {
            bool saveResult = await SaveAsync(tab);
            if (!saveResult)
            {
                return false;
            }

            tab.Root.Tabs.Remove(tab);
            return true;
        }

        if (confirmCloseResult == ConfirmCloseResult.Discard)
        {
            tab.Root.Tabs.Remove(tab);
            return true;
        }

        return false;
    }

    public async Task<bool> SaveAsync(Tab tab)
    {
        if (!tab.IsModified)
        {
            return true;
        }

        await _saveLock.WaitAsync();
        try
        {
            if (!tab.IsModified)
            {
                return true;
            }

            if (tab.File is not null)
            {
                await tab.File.WriteAllTextAsync(tab.Content);
                tab.OriginalContent = tab.Content;
                return true;
            }
        }
        finally
        {
            _saveLock.Release();
        }

        return await SaveToAnotherFileAsync(tab);
    }

    public async Task<bool> SaveToAnotherFileAsync(Tab tab)
    {
        await _saveLock.WaitAsync();
        try
        {
            IFile? file = await _filePickerService.PickSaveFileAsync();
            if (file is null)
            {
                return false;
            }

            await file.WriteAllTextAsync(tab.Content);
            tab.File = file;
            tab.OriginalContent = tab.Content;
            return true;
        }
        finally
        {
            _saveLock.Release();
        }
    }
}