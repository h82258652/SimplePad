using CommunityToolkit.Mvvm.ComponentModel;

namespace SimplePad.UWP.UI.ViewModels;

public sealed partial class EditorViewModel : ObservableObject
{
    public EditorViewModel(ShellViewModel shellViewModel)
    {
        ShellViewModel = shellViewModel;
    }

    [ObservableProperty]
    public partial string Content { get; set; } = string.Empty;

    public ShellViewModel ShellViewModel { get; }

    [ObservableProperty]
    public partial string Title { get; set; } = "Untitled";
}
