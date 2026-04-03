using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Windowing;

public sealed partial class ShellView : UserControl
{
    public ShellView(IAppWindow appWindow)
    {
        InitializeComponent();

        Window.Current.SetTitleBar(TabView.TitleBar);
    }
}