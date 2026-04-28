using System;
using System.IO;
using System.Linq;
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

    public IFile? File
    {
        get => _file;
        internal set
        {
            if (_file != value)
            {
                _file = value;
                UpdateTitle();
            }
        }
    }

    public bool IsModified
    {
        get => _isModified;
        private set
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

    internal static Tab CreateBlank(TabRoot root)
    {
        return new Tab(root);
    }

    internal static Tab CreateClone(TabRoot newRoot, Tab tab)
    {
        Tab clonedTab = new(newRoot)
        {
            _content = tab._content,
            _originalContent = tab._originalContent,
            _title = tab._title,
            _isModified = tab._isModified,
            _file = tab._file
        };

        return clonedTab;
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

        firstLine = new string([.. firstLine.Where(character => !Path.GetInvalidFileNameChars().Contains(character))]);
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