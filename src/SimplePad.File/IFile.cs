using System.Threading.Tasks;

namespace SimplePad.File;

public interface IFile
{
    string FileName { get; }

    string Path { get; }

    Task<string> ReadAllTextAsync();

    Task WriteAllTextAsync(string text);
}
