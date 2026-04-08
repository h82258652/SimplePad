using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Settings;

public sealed partial class BackButton : Button
{
    public BackButton()
    {
        DefaultStyleKey = typeof(BackButton);
        DefaultStyleResourceUri = new System.Uri("ms-appx:///SimplePad.Settings.UWP/BackButton.xaml");

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
    }
}
