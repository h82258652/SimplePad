using System.Collections.ObjectModel;

namespace SimplePad.Tabs;

public sealed class TabRoot
{
    public ObservableCollection<Tab> Tabs { get; } = [];

    public void Add()
    {
        Tabs.Add(new Tab(this));
    }

    public void AddTabFromFile(object file)
    {
        throw new System.NotImplementedException();
    }
}
