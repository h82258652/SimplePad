using System;
using System.Threading.Tasks;

namespace SimplePad.File;

public sealed class WPFFile : IFile
{
    private readonly string _path;
    private LineEndings _lineEndings = LineEndings.CRLF;

    public WPFFile(string path)
    {
        _path = path;
    }

    public event EventHandler<LineEndings>? LineEndingsChanged;

    public string FileName => System.IO.Path.GetFileName(Path);

    public LineEndings LineEndings
    {
        get => _lineEndings;
        private set
        {
            if (_lineEndings != value)
            {
                _lineEndings = value;
                LineEndingsChanged?.Invoke(this, value);
            }
        }
    }

    public string Path => _path;

    public Task<DateTimeOffset> GetModificationTimeAsync()
    {
        DateTime lastWriteTime = System.IO.File.GetLastWriteTime(_path);
        return Task.FromResult<DateTimeOffset>(lastWriteTime);
    }

    public Task<string> ReadAllTextAsync()
    {
        return System.IO.File.ReadAllTextAsync(_path);
    }

    public Task WriteAllTextAsync(string text)
    {
        return System.IO.File.WriteAllTextAsync(_path, text);
    }
}