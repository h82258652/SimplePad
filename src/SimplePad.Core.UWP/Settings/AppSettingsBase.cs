using System.Threading.Tasks;

namespace SimplePad.Core.Settings;

public abstract class AppSettingsBase : IAppSettings
{
    public abstract Task LoadAsync();

    public abstract Task SaveAsync();
}
