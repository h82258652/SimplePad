using CommunityToolkit.Mvvm.ComponentModel;

namespace SimplePad.UWP.UI.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial bool IsSettingsViewVisible { get; set; }
}
