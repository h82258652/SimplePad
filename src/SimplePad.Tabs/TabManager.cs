using System.Threading.Tasks;
using SimplePad.File;

namespace SimplePad.Tabs;

public sealed class TabManager
{
    private readonly IFilePickerService _filePickerService;

    internal TabManager(IFilePickerService filePickerService)
    {
        _filePickerService = filePickerService;
    }

    public async Task<bool> SaveAsync(Tab tab)
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

    public async Task<bool> CloseAsync(Tab tab)
    {
        if (!tab.IsModified)
        {
            tab.Root.Tabs.Remove(tab);
            return true;
        }

        // TODO dialog

        return false;
    }
}