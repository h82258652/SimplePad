using System;
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

    public UWPFile(StorageFile storageFile)
    {
        _storageFile = storageFile;
    }

    public string FileName => _storageFile.Name;

    public LineEndings LineEndings { get; private set; } = LineEndings.CRLF;

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

    public async Task WriteAllTextAsync(string text)
    {
        text = string.Join(LineEndings.NewLine, text.Split([LineEndings.CRLF.NewLine, LineEndings.CR.NewLine, LineEndings.LF.NewLine], StringSplitOptions.None));
        await FileIO.WriteTextAsync(_storageFile, text);
    }
}