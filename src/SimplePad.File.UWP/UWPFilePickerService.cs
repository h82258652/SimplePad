using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace SimplePad.File;

internal sealed class UWPFilePickerService : IFilePickerService
{
    public async Task<IFile?> PickSaveFileAsync()
    {
        FileSavePicker fileSavePicker = new();
        fileSavePicker.FileTypeChoices.Add("Text documents", new List<string>() { ".txt" });
        StorageFile? file = await fileSavePicker.PickSaveFileAsync();
        if (file is null)
        {
            return null;
        }

        return new UWPFile(file);
    }
}
