using SimplePad.File;
using System.Threading.Tasks;

namespace SimplePad.Tabs;

public sealed class Tab
{
    private Tab(TabRoot root)
    {
        Root = root;
    }

    public TabRoot Root { get; }

    private IFile? File { get; set; }

    private string OriginalContent { get; set; } = string.Empty;

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