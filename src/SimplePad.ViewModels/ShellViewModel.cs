using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SimplePad.Services;
using Windows.UI.Xaml;

namespace SimplePad.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    private readonly ObservableCollection<EditorViewModel> _editors = [];

    public IReadOnlyList<EditorViewModel> Editors => _editors;

    [ObservableProperty]
    public partial bool IsSettingsViewVisible { get; set; }

    [ObservableProperty]
    public partial EditorViewModel? SelectedEditor { get; set; }

    public void AddBlankEditor()
    {
        EditorViewModel newEditorViewModel = EditorViewModel.CreateBlank(this);
        _editors.Add(newEditorViewModel);
        SelectedEditor = newEditorViewModel;
    }

    public void AddEditorFromFile(IFile file)
    {
        EditorViewModel newEditorViewModel = EditorViewModel.CreateFromFile(this, file);
        _editors.Add(newEditorViewModel);
        SelectedEditor = newEditorViewModel;
    }

    public async Task CloseEditorAsync(EditorViewModel editorViewModel)
    {
        if (editorViewModel.IsModified)
        {
            // TODO: Prompt the user to save changes before closing the editor.

            bool saveSuccess = await editorViewModel.SaveAsync();
            if (!saveSuccess)
            {
                return;
            }
        }

        _editors.Remove(editorViewModel);
        if (_editors.Count <= 0)
        {
            Window.Current.Close(); // TODO this is UWP limit API, need to find a better way to close the app
            return;
        }

        if (SelectedEditor == editorViewModel)
        {
            SelectedEditor = _editors[^1];
        }
    }
}
