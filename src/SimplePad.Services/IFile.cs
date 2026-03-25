using System.Threading.Tasks;

namespace SimplePad.Services;

public interface IFile
{
    string FileName { get; }

    Task<byte[]> ReadAllBytesAsync();

    Task WriteAllBytesAsync(byte[] bytes);
}
