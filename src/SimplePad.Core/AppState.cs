using CommunityToolkit.Mvvm.ComponentModel;

namespace SimplePad.Core;

public sealed partial class AppState : ObservableObject
{
    [ObservableProperty]
    public partial double ZoomFactor { get; set; } = 1d;
}
