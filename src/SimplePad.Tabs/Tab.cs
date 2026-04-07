namespace SimplePad.Tabs;

public sealed class Tab
{
    internal Tab(TabRoot root)
    {
        Root = root;
    }

    public TabRoot Root { get; }
}
