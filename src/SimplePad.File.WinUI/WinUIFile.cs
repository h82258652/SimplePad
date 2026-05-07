using System;
using System.Threading.Tasks;

namespace SimplePad.File;

public sealed class WinUIFile : IFile
{
    private LineEndings _lineEndings = LineEndings.CRLF;

    public event EventHandler<LineEndings>? LineEndingsChanged;

    public string FileName => throw new NotImplementedException();

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