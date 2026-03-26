using CommunityToolkit.Mvvm.ComponentModel;

namespace SimplePad.ViewModels;

public sealed partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    public partial bool IsFontSettingsExpanded { get; set; }
}
