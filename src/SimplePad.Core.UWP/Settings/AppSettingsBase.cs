using System.Threading.Tasks;
using SimplePad.Core.Settings;

namespace SimplePad.Core.UWP.Settings;

public abstract class AppSettingsBase : IAppSettings
{
    public abstract Task LoadAsync();

    public abstract Task SaveAsync();
}
