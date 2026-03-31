using CommunityToolkit.Mvvm.ComponentModel;

namespace SimplePad.ViewModels;

public sealed partial class SettingsViewModel : ObservableObject
{
    public SettingsViewModel(ShellViewModel shellViewModel)
    {
        ShellViewModel = shellViewModel;
    }

    [ObservableProperty]
    public partial bool IsFontSettingsExpanded { get; set; }

    public ShellViewModel ShellViewModel { get; }
}
