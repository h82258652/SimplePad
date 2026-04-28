using System;
using System.Collections.ObjectModel;
using SimplePad.File;

namespace SimplePad.Tabs;

public sealed class TabRoot
{
    private Tab? _selectedTab;

    public event EventHandler<Tab?>? SelectedTabChanged;

    public Tab? SelectedTab
    {
        get => _selectedTab;
        set
        {
            if (_selectedTab != value)
            {
                _selectedTab = value;
                SelectedTabChanged?.Invoke(this, value);
            }
        }
    }

    public ObservableCollection<Tab> Tabs { get; } = [];

    public void AddBlankTab()
    {
        Tab newTab = Tab.CreateBlank(this);
        Tabs.Add(newTab);
        SelectedTab = newTab;
    }

    public void AddCloneOfTab(Tab tab)
    {
        Tab newTab = Tab.CreateClone(this, tab);
        Tabs.Add(newTab);
        SelectedTab = newTab;
    }

    public void AddTabFromFile(IFile file)
    {
        Tab newTab = Tab.CreateFromFile(this, file);
        Tabs.Add(newTab);
        SelectedTab = newTab;
    }
}