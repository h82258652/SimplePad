using Windows.UI.Xaml;

namespace SimplePad.UWP.UI.Helpers;

public static class UIHelper
{
    public static Visibility InverseBoolToVisibility(bool isVisible)
    {
        return isVisible ? Visibility.Collapsed : Visibility.Visible;
    }
}
