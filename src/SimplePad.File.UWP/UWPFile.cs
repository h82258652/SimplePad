using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

namespace SimplePad.File;

public sealed class UWPFile : IFile
{
    private readonly StorageFile _storageFile;
    private LineEndings _lineEndings = LineEndings.CRLF;

    public UWPFile(StorageFile storageFile)
    {
        _storageFile = storageFile;
    }

    public event EventHandler<LineEndings>? LineEndingsChanged;

    public string FileName => _storageFile.Name;

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

    public string Path => _storageFile.Path;

    public async Task<DateTimeOffset> GetModificationTimeAsync()
    {
        BasicProperties basicProperties = await _storageFile.GetBasicPropertiesAsync();
        return basicProperties.DateModified;
    }

    public async Task<string> ReadAllTextAsync()
    {
        IBuffer buffer = await FileIO.ReadBufferAsync(_storageFile);
        string text = Encoding.UTF8.GetString(buffer.ToArray());
        DetectLineEndings(text);

        // Use CR \r in app to adapt the UWP TextBox
        if (LineEndings == LineEndings.CRLF)
        {
            text = text.Replace(LineEndings.CRLF.NewLine, LineEndings.CR.NewLine);
        }
        else if (LineEndings == LineEndings.LF)
        {
            text = text.Replace(LineEndings.LF.NewLine, LineEndings.CR.NewLine);
        }

        return text;
    }

    public async Task WriteAllTextAsync(string text)
    {
        text = string.Join(LineEndings.NewLine, text.Split([LineEndings.CRLF.NewLine, LineEndings.CR.NewLine, LineEndings.LF.NewLine], StringSplitOptions.None));

        // We don't use FileIO.WriteTextAsync here, for dragged into file, it is readonly. But we can workaround it.
        // https://github.com/microsoft/microsoft-ui-xaml/issues/2421
        if (IsFileReadOnly() || !await IsFileWritableAsync())
        {
            Encoding encoding = Encoding.UTF8;
            byte[] content = encoding.GetBytes(text);
            await PathIO.WriteBytesAsync(_storageFile.Path, content);
        }
        else
        {
            await FileIO.WriteTextAsync(_storageFile, text);
        }
    }

    private void DetectLineEndings(string text)
    {
        if (text.Contains(LineEndings.CRLF.NewLine))
        {
            LineEndings = LineEndings.CRLF;
        }
        else if (text.Contains(LineEndings.CR.NewLine))
        {
            LineEndings = LineEndings.CR;
        }
        else if (text.Contains(LineEndings.LF.NewLine))
        {
            LineEndings = LineEndings.LF;
        }
    }

    private bool IsFileReadOnly()
    {
        return (_storageFile.Attributes & Windows.Storage.FileAttributes.ReadOnly) != 0;
    }

    private async Task<bool> IsFileWritableAsync()
    {
        try
        {
            using var stream = await _storageFile.OpenStreamForWriteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}