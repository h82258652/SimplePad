using SimplePad.File;
using System;
using System.Threading.Tasks;

namespace SimplePad.Tabs;

public sealed class Tab
{
    private bool _isModified;

    private Tab(TabRoot root)
    {
        Root = root;
    }

    public event EventHandler<bool>? IsModifiedChanged;

    public bool IsModified
    {
        get => _isModified;
        set
        {
            if (_isModified != value)
            {
                _isModified = value;
                IsModifiedChanged?.Invoke(this, value);
            }
        }
    }

    public TabRoot Root { get; }

    private IFile? File { get; set; }

    private string OriginalContent { get; set; } = string.Empty;

    public void Close()
    {
        Root.Tabs.Remove(this);
    }

    internal static Tab CreateBlank(TabRoot root)
    {
        return new Tab(root);
    }

    internal static Tab CreateFromFile(TabRoot root, IFile file)
    {
        Tab tab = new(root) { File = file };

        _ = tab.LoadContentAsync(file);

        return tab;
    }

    private async Task LoadContentAsync(IFile file)
    {
        _ = await file.ReadAllTextAsync();
    }
}