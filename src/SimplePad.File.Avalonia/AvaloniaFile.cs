using System;
using System.Threading.Tasks;

namespace SimplePad.File;

public sealed class AvaloniaFile : IFile
{
    public event EventHandler<LineEndings>? LineEndingsChanged;

    public string FileName => throw new NotImplementedException();

    public LineEndings LineEndings => throw new NotImplementedException();

    public string Path => throw new NotImplementedException();

    public Task<DateTimeOffset> GetModificationTimeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> ReadAllTextAsync()
    {
        throw new NotImplementedException();
    }

    public Task WriteAllTextAsync(string text)
    {
        throw new NotImplementedException();
    }
}