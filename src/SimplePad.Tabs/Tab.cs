using System;
using System.Threading.Tasks;
using SimplePad.File;

namespace SimplePad.Tabs;

public sealed class Tab
{
    private string _content = string.Empty;
    private IFile? _file;
    private bool _isModified;
    private string _originalContent = string.Empty;
    private string _title = TabConstants.DefaultTabTitle;

    private Tab(TabRoot root)
    {
        Root = root;
    }

    public event EventHandler<string>? ContentChanged;

    public event EventHandler<bool>? IsModifiedChanged;

    public event EventHandler<string>? TitleChanged;

    public string Content
    {
        get => _content;
        set
        {
            if (_content != value)
            {
                _content = value;
                ContentChanged?.Invoke(this, value);
                UpdateTitle();
                UpdateIsModified();
            }
        }
    }

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

    public string Title
    {
        get => _title;
        private set
        {
            if (_title != value)
            {
                _title = value;
                TitleChanged?.Invoke(this, value);
            }
        }
    }

    internal IFile? File
    {
        get => _file;
        set
        {
            if (_file != value)
            {
                _file = value;
                UpdateTitle();
            }
        }
    }

    internal string OriginalContent
    {
        get => _originalContent;
        set
        {
            if (_originalContent != value)
            {
                _originalContent = value;
                UpdateIsModified();
            }
        }
    }

    public void Close()
    {
        if (IsModified)
        {
            // TODO: Prompt to save changes
        }

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
        string text = await file.ReadAllTextAsync();
        OriginalContent = text;
        Content = text;
    }

    private void UpdateIsModified()
    {
        IsModified = Content != OriginalContent;
    }

    private void UpdateTitle()
    {
        if (File is { } file)
        {
            Title = file.FileName;
            return;
        }

        string firstLine;
        int firstLineBreakIndex = Content.IndexOf('\r');
        if (firstLineBreakIndex >= 0)
        {
            firstLine = Content[..firstLineBreakIndex];
        }
        else
        {
            firstLine = Content;
        }

        firstLine = firstLine.Trim();
        if (firstLine.Length > 0)
        {
            Title = firstLine;
        }
        else
        {
            Title = TabConstants.DefaultTabTitle;
        }
    }
}