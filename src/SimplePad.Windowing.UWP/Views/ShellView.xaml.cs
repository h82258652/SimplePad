using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Windowing.UWP.Views;

public sealed partial class ShellView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel),
        typeof(AppWindowViewModel),
        typeof(ShellView),
        new PropertyMetadata(null));

    public ShellView(AppWindowViewModel viewModel)
    {
        InitializeComponent();

        //SettingsView.SettingsState = viewModel.SettingsState;
    }

    public AppWindowViewModel? ViewModel
    {
        get => (AppWindowViewModel?)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
}