using System.ComponentModel;

namespace SimplePad.Settings;

public interface IAppSettings : INotifyPropertyChanged
{
    bool IsStatusBarVisible { get; set; }

    bool IsWordWrap { get; set; }
}
