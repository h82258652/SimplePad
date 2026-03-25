using System.Threading.Tasks;

namespace SimplePad.Services;

public interface IFileService
{
    Task<IFile?> PickSaveFileAsync();
}
