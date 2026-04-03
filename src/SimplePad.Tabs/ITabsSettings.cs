using System;
using SimplePad.Core.Settings;

namespace SimplePad.Tabs;

public interface ITabsSettings : IAppSettings
{
    OpenFileBehavior OpenFileBehavior { get; set; }

    event EventHandler<OpenFileBehavior>? OpenFileBehaviorChanged;
}
