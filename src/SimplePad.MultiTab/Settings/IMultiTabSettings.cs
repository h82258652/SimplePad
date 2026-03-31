using SimplePad.Core.Settings;

namespace SimplePad.MultiTab.Settings;

public interface IMultiTabSettings : IAppSettings
{
    OpenFileBehavior OpenFileBehavior { get; set; }

    event EventHandler<OpenFileBehavior>? OpenFileBehaviorChanged;
}
