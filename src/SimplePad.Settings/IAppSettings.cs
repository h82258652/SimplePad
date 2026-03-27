using System.ComponentModel;

namespace SimplePad.Settings;

public interface IAppSettings : INotifyPropertyChanged
{
    AppTheme AppTheme { get; set; }

    AppFontStyle FontStyle { get; set; }

    double FontSize { get; set; }

    bool IsSpellCheckEnabled { get; set; }

    bool IsStatusBarVisible { get; set; }

    bool IsWordWrap { get; set; }

    OpenFileBehavior OpenFileBehavior { get; set; }
}
