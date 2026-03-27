using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using SimplePad.Settings;

namespace SimplePad.ViewModels;

public sealed partial class SettingsViewModel : ObservableObject
{
    public SettingsViewModel(ShellViewModel shellViewModel)
    {
        ShellViewModel = shellViewModel;
    }

    public IReadOnlyList<AppFontStyle> FontStyles { get; } = Enum.GetValues<AppFontStyle>();

    [ObservableProperty]
    public partial bool IsFontSettingsExpanded { get; set; }

    public ShellViewModel ShellViewModel { get; }
}
