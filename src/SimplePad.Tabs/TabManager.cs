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

    public async Task SaveAsync(Tab tab)
    {
        if (!tab.IsModified)
        {
            return;
        }

        if (tab.File is not null)
        {
            await tab.File.WriteAllTextAsync(tab.Content);
            tab.OriginalContent = tab.Content;
            return;
        }

        IFile? file = await _filePickerService.PickSaveFileAsync();
        if (file is null)
        {
            return;
        }

        await file.WriteAllTextAsync(tab.Content);
        tab.File = file;
        tab.OriginalContent = tab.Content;
    }
}