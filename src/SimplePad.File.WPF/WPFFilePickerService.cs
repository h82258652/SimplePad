using System.Threading.Tasks;
using Microsoft.Win32;

namespace SimplePad.File;

internal sealed class WPFFilePickerService : IFilePickerService
{
    public Task<IFile?> PickOpenFileAsync()
    {
        OpenFileDialog openFileDialog = new()
        {
            DefaultExt = ".txt",
            Filter = "Text documents (.txt)|*.txt"
        };

        if (openFileDialog.ShowDialog() is true)
        {
            return Task.FromResult<IFile?>(new WPFFile(openFileDialog.FileName));
        }

        return Task.FromResult<IFile?>(null);
    }

    public Task<IFile?> PickSaveFileAsync()
    {
        SaveFileDialog saveFileDialog = new()
        {
            DefaultExt = ".txt",
            Filter = "Text documents (.txt)|*.txt"
        };

        if (saveFileDialog.ShowDialog() is true)
        {
            return Task.FromResult<IFile?>(new WPFFile(saveFileDialog.FileName));
        }

        return Task.FromResult<IFile?>(null);
    }
}
