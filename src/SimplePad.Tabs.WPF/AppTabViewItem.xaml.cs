using System.Windows;
using System.Windows.Controls;

namespace SimplePad.Tabs;

public partial class AppTabViewItem : TabItem
{
    public AppTabViewItem()
    {
        InitializeComponent();
        AppMenuBar.TextBox = TextBox;
        StatusBar.TextBox = TextBox;

        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        Tab? oldTab = e.OldValue as Tab;
        if (oldTab is not null)
        {

        }

        Tab? newTab = e.NewValue as Tab;
        if (newTab is not null)
        {

        } 
    }
}
