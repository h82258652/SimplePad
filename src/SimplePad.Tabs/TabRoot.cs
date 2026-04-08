using SimplePad.File;
using System.Collections.ObjectModel;

namespace SimplePad.Tabs;

public sealed class TabRoot
{
    public ObservableCollection<Tab> Tabs { get; } = [];

    public void AddBlankTab()
    {
        Tabs.Add(Tab.CreateBlank(this));
    }

    public void AddTabFromFile(IFile file)
    {
        Tabs.Add(Tab.CreateFromFile(this, file));
    }
}
