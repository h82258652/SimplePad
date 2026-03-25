using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SimplePad.ViewModels;

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

    public async Task CloseEditorAsync(EditorViewModel editorViewModel)
    {
        if (editorViewModel.IsModified)
        {
            // TODO Save
        }

        _editors.Remove(editorViewModel);
        if (_editors.Count <= 0)
        {
            // TODO close current window instead of add editor
            AddEditor();
            return;
        }

        if (SelectedEditor == editorViewModel)
        {
            SelectedEditor = _editors[^1];
        }
    }
}
