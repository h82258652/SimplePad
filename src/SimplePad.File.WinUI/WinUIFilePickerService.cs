using Microsoft.UI;
using Microsoft.Windows.Storage.Pickers;
using SimplePad.Windowing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace SimplePad.File;

internal sealed class WinUIFilePickerService : IFilePickerService
{
    private readonly IAppWindowManager _appWindowManager;

    public WinUIFilePickerService(IAppWindowManager appWindowManager)
    {
        _appWindowManager = appWindowManager;
    }

    public async Task<IFile?> PickOpenFileAsync()
    {
        if (_appWindowManager.CurrentWindow?.Id is WindowId currentWindowId)
        {
            FileOpenPicker fileOpenPicker = new(currentWindowId);
            fileOpenPicker.FileTypeFilter.Add(".txt");
            fileOpenPicker.FileTypeFilter.Add("*");
            PickFileResult result = await fileOpenPicker.PickSingleFileAsync();
            if (result is not null)
            {
                StorageFile file = await StorageFile.GetFileFromPathAsync(result.Path);
                return new WinUIFile(file);
            }
        }

        return null;
    }

    public async Task<IFile?> PickSaveFileAsync()
    {
        if (_appWindowManager.CurrentWindow?.Id is WindowId currentWindowId)
        {
            FileSavePicker fileSavePicker = new(currentWindowId);
            fileSavePicker.FileTypeChoices.Add("Text documents", new List<string>() { ".txt" });
            PickFileResult result = await fileSavePicker.PickSaveFileAsync();
            if (result is not null)
            {
                StorageFile file = await StorageFile.GetFileFromPathAsync(result.Path);
                return new WinUIFile(file);
            }
        }

        return null;
    }
}
