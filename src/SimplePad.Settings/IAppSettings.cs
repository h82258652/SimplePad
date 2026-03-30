using System.ComponentModel;

namespace SimplePad.Settings;

public interface IAppSettings
{
    event PropertyChangedEventHandler? PropertyChanged;

    bool IsSpellCheckEnabled { get; set; }

    bool IsStatusBarVisible { get; set; }

    bool IsWordWrap { get; set; }

    OpenFileBehavior OpenFileBehavior { get; set; }

    Task LoadAsync();

    Task SaveAsync();
}
