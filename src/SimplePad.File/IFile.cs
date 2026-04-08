using System.Threading.Tasks;

namespace SimplePad.File;

public interface IFile
{
    Task<string> ReadAllTextAsync();

    Task WriteAllTextAsync(string text);
}
