using System;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SimplePad.File;

internal sealed class WPFFilePickerService : IFilePickerService
{
    public Task<IFile?> PickOpenFileAsync()
    {
        OpenFileDialog openFileDialog = new();
        throw new NotImplementedException();
    }

    public Task<IFile?> PickSaveFileAsync()
    {
        SaveFileDialog saveFileDialog = new();
        throw new NotImplementedException();
    }
}
