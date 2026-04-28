using System;
using System.Threading.Tasks;

namespace SimplePad.File;

public interface IFile
{
    string FileName { get; }

    string Path { get; }

    Task<DateTimeOffset> GetModificationTimeAsync();

    Task<string> ReadAllTextAsync();

    Task WriteAllTextAsync(string text);
}
