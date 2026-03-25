using SimplePad.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class SettingsView : UserControl
{
    public static readonly DependencyProperty ShellViewModelProperty = DependencyProperty.Register(
        nameof(ShellViewModel),
        typeof(ShellViewModel),
        typeof(SettingsView),
        null);

    public SettingsView()
    {
        InitializeComponent();
    }

    public ShellViewModel? ShellViewModel
    {
        get => (ShellViewModel?)GetValue(ShellViewModelProperty);
        set => SetValue(ShellViewModelProperty, value);
    }

    private void OnBackButtonClick(object sender, RoutedEventArgs e)
    {
        ShellViewModel?.IsSettingsViewVisible = false;
    }
}
