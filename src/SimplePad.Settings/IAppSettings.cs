using System.ComponentModel;

namespace SimplePad.Settings;

public interface IAppSettings : INotifyPropertyChanged
{
    double FontSize { get; set; }
    
    bool IsSpellCheckEnabled { get; set; }

    bool IsStatusBarVisible { get; set; }

    bool IsWordWrap { get; set; }
}
