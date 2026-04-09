using System.Threading.Tasks;

namespace SimplePad.File;

public interface IFilePickerService
{
    Task<IFile?> PickSaveFileAsync();
}
