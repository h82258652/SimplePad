using System.Threading.Tasks;

namespace SimplePad.Core.Settings;

public interface IAppSettings
{
    Task LoadAsync();

    Task SaveAsync();
}
