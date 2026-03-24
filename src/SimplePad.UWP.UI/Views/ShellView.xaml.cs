using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class ShellView : UserControl
{
    public ShellView()
    {
        InitializeComponent();
    }

    private void OnTabViewAddTabButtonClick(TabView sender, object args)
    {
        TabViewItem tabViewItem = new()
        {
            Header = "Untitled",
            Content = new TextView()
        };

        sender.TabItems.Add(tabViewItem);
    }
}
