using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace SimplePad.Services.UWP;

public sealed class UWPFile : IFile
{
    private readonly StorageFile _storageFile;

    public UWPFile(StorageFile storageFile)
    {
        _storageFile = storageFile;
    }

    public string FileName => _storageFile.Name;

    public async Task<byte[]> ReadAllBytesAsync()
    {
        IBuffer buffer = await FileIO.ReadBufferAsync(_storageFile);
        return buffer.ToArray();
    }

    public async Task WriteAllBytesAsync(byte[] bytes)
    {
        await FileIO.WriteBytesAsync(_storageFile, bytes);
    }
}
