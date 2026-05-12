using System.Windows.Controls;

namespace SimplePad.Tabs;

public partial class AppTabViewItem : TabItem
{
    public AppTabViewItem()
    {
        InitializeComponent();
        StatusBar.TextBox = TextBox;
    }
}
