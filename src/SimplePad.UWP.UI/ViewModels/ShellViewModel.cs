using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimplePad.UWP.UI.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    private readonly ObservableCollection<EditorViewModel> _editors = [];

    public IReadOnlyList<EditorViewModel> Editors => _editors;

    [ObservableProperty]
    public partial bool IsSettingsViewVisible { get; set; }

    [ObservableProperty]
    public partial EditorViewModel? SelectedEditor { get; set; }

    public void AddEditor()
    {
        EditorViewModel newEditorViewModel = new(this);
        _editors.Add(newEditorViewModel);
        SelectedEditor = newEditorViewModel;
    }
}
