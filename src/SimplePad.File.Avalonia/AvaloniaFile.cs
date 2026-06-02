using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace SimplePad.File;

public sealed class AvaloniaFile : IFile
{
    private readonly IStorageFile _storageFile;
    private LineEndings _lineEndings = LineEndings.CRLF;

    public AvaloniaFile(IStorageFile storageFile)
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

    public string Path => _storageFile.Path.ToString();

    public async Task<DateTimeOffset> GetModificationTimeAsync()
    {
        StorageItemProperties basicProperties = await _storageFile.GetBasicPropertiesAsync();
        return basicProperties.DateModified ?? throw new NotImplementedException();
    }

    public async Task<string> ReadAllTextAsync()
    {
        using Stream stream = await _storageFile.OpenReadAsync();
        using StreamReader streamReader = new(stream);
        string text = await streamReader.ReadToEndAsync();
        DetectLineEndings(text);

        return text;
    }

    public async Task WriteAllTextAsync(string text)
    {
        using Stream stream = await _storageFile.OpenWriteAsync();
        using StreamWriter streamWriter = new(stream);
        await streamWriter.WriteAsync(text);
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
}