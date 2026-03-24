using Microsoft.UI.Xaml.Controls;
using SimplePad.UWP.UI.ViewModels;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class ShellView : UserControl
{
    public ShellView()
    {
        InitializeComponent();
    }

    public ShellViewModel ViewModel { get; } = new ShellViewModel();

    private void OnTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
    }

    private void OnTabViewAddTabButtonClick(TabView sender, object args)
    {
        TabViewItem tabViewItem = new()
        {
            Header = "Untitled",
            Content = new EditorView(ViewModel)
        };

        sender.TabItems.Add(tabViewItem);

        sender.SelectedItem = tabViewItem;
    }
}
