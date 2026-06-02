using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using SimplePad.Core;

namespace SimplePad.File;

internal sealed class AvaloniaFilePickerService : IFilePickerService
{
    private readonly ITopLevelProvider _topLevelProvider;

    public AvaloniaFilePickerService(ITopLevelProvider topLevelProvider)
    {
        _topLevelProvider = topLevelProvider;
    }

    public async Task<IFile?> PickOpenFileAsync()
    {
        TopLevel topLevel = _topLevelProvider.Get();
        if (!topLevel.StorageProvider.CanOpen)
        {
            throw new NotSupportedException();
        }

        FilePickerOpenOptions options = new()
        {
            SuggestedFileType = FilePickerFileTypes.TextPlain,
        };
        IReadOnlyList<IStorageFile> storageFiles = await topLevel.StorageProvider.OpenFilePickerAsync(options);

        if (storageFiles.Count == 1)
        {
            return new AvaloniaFile(storageFiles[0]);
        }

        return null;
    }

    public async Task<IFile?> PickSaveFileAsync()
    {
        TopLevel topLevel = _topLevelProvider.Get();
        if (!topLevel.StorageProvider.CanSave)
        {
            throw new NotSupportedException();
        }

        FilePickerSaveOptions options = new() { };
        IStorageFile? file = await topLevel.StorageProvider.SaveFilePickerAsync(options);
        if (file is null)
        {
            return null;
        }

        return new AvaloniaFile(file);
    }
}
