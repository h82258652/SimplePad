using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SimplePad.Core;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Services;

namespace SimplePad.ViewModels;

public sealed partial class EditorViewModel : ObservableObject
{
    private readonly IFileService _fileService;

    private EditorViewModel(ShellViewModel shellViewModel)
    {
        _fileService = ServiceLocator.Current.GetRequiredService<IFileService>();

        ShellViewModel = shellViewModel;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    [NotifyPropertyChangedFor(nameof(IsModified))]
    public partial string Content { get; set; } = string.Empty;

    public bool IsModified => Content != OriginalContent;

    public string OriginalContent { get; set; } = string.Empty;

    public ShellViewModel ShellViewModel { get; }

    public string Title
    {
        get
        {
            if (File is not null)
            {
                return File.FileName;
            }

            foreach (var line in Content.Split("\r"))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    return line;
                }
            }

            return "Untitled";
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    private partial IFile? File { get; set; }

    public static EditorViewModel CreateBlank(ShellViewModel shellViewModel)
    {
        return new EditorViewModel(shellViewModel);
    }

    public static EditorViewModel CreateFromFile(ShellViewModel shellViewModel, IFile file)
    {
        EditorViewModel editorViewModel = new(shellViewModel)
        {
            File = file
        };

        _ = editorViewModel.LoadContentAsync(file);

        return editorViewModel;
    }

    public async Task<bool> SaveAsync()
    {
        if (!IsModified)
        {
            return true;
        }

        if (File is null)
        {
            IFile? file = await _fileService.PickSaveFileAsync();
            if (file is null)
            {
                return false;
            }

            File = file;
        }

        string content = Content;
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        await File.WriteAllBytesAsync(bytes);
        OriginalContent = content;
        return true;
    }

    private async Task LoadContentAsync(IFile file)
    {
        byte[] bytes = await file.ReadAllBytesAsync();
        string text = Encoding.UTF8.GetString(bytes);
        OriginalContent = text;
        Content = text;
    }
}
