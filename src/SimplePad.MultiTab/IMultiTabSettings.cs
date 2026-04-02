using SimplePad.Core.Settings;

namespace SimplePad.MultiTab;

public interface IMultiTabSettings : IAppSettings
{
    OpenFileBehavior OpenFileBehavior { get; set; }

    event EventHandler<OpenFileBehavior>? OpenFileBehaviorChanged;
}
