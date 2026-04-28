using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace SimplePad.File;

public sealed class UWPFile : IFile
{
    private readonly StorageFile _storageFile;

    public UWPFile(StorageFile storageFile)
    {
        _storageFile = storageFile;
    }

    public string FileName => _storageFile.Name;

    public string Path => _storageFile.Path;

    public async Task<string> ReadAllTextAsync()
    {
        IBuffer buffer = await FileIO.ReadBufferAsync(_storageFile);
        return Encoding.UTF8.GetString(buffer.ToArray());
    }

    public async Task WriteAllTextAsync(string text)
    {
        await FileIO.WriteTextAsync(_storageFile, text);
    }
}
