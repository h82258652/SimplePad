using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Settings;

public sealed partial class SettingsView : UserControl
{
    public static readonly DependencyProperty SettingsStateProperty = DependencyProperty.Register(
        nameof(SettingsState),
        typeof(SettingsState),
        typeof(SettingsView),
        null);

    public SettingsView()
    {
        InitializeComponent();
    }

    public SettingsState? SettingsState
    {
        get => (SettingsState?)GetValue(SettingsStateProperty);
        set => SetValue(SettingsStateProperty, value);
    }

    public UIElement TitleBar => TitleBarElement;
}