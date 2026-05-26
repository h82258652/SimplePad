using System.Windows.Controls;

namespace SimplePad.Tabs;

public sealed partial class AppTabContentControl : UserControl
{
    public AppTabContentControl()
    {
        InitializeComponent();
        AppMenuBar.TextBox = TextBox;
        StatusBar.TextBox = TextBox;
    }
}
