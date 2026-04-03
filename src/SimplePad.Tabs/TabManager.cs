using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimplePad.Tabs;

public sealed class TabManager
{
    private readonly ObservableCollection<Tab> _tabs = [];

    public IReadOnlyList<Tab> Tabs
    {
        get
        {
            return _tabs;
        }
    }

    public void Add()
    {
        _tabs.Add(new Tab());
    }
}
