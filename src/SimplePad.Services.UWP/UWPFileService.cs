using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace SimplePad.Services.UWP;

public sealed class UWPFileService : IFileService
{
    public async Task<IFile?> PickSaveFileAsync()
    {
        FileSavePicker fileSavePicker = new();
        fileSavePicker.FileTypeChoices.Add("Text documents", new List<string>() { ".txt" });
        StorageFile? file = await fileSavePicker.PickSaveFileAsync();
        return file is null ? null : new UWPFile(file);
    }
}
