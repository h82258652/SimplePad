using System;
using System.Threading.Tasks;

namespace SimplePad.File;

internal sealed class WinUIFilePickerService : IFilePickerService
{
    public async Task<IFile?> PickOpenFileAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IFile?> PickSaveFileAsync()
    {
        throw new NotImplementedException();
    }
}
