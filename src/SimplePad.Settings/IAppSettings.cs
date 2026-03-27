using System.ComponentModel;

namespace SimplePad.Settings;

public interface IAppSettings
{
    event PropertyChangedEventHandler? PropertyChanged;

    AppTheme AppTheme { get; set; }

    string FontFamily { get; set; }

    double FontSize { get; set; }

    AppFontStyle FontStyle { get; set; }

    bool IsSpellCheckEnabled { get; set; }

    bool IsStatusBarVisible { get; set; }

    bool IsWordWrap { get; set; }

    OpenFileBehavior OpenFileBehavior { get; set; }

    Task LoadAsync();

    Task SaveAsync();
}
