using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Settings;

public sealed partial class SettingsView : UserControl
{
    public static readonly DependencyProperty SettingsStateProperty = DependencyProperty.Register(
        nameof(SettingsState),
        typeof(SettingsState),
        typeof(SettingsView),
        new PropertyMetadata(null, OnSettingsStateChanged));

    public SettingsView()
    {
        InitializeComponent();

        UpdateVisibility();
    }

    public SettingsState? SettingsState
    {
        get => (SettingsState?)GetValue(SettingsStateProperty);
        set => SetValue(SettingsStateProperty, value);
    }

    public UIElement TitleBar => TitleBarElement;

    private static void OnSettingsStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        SettingsView self = (SettingsView)d;
        self.UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        if (SettingsState is { IsVisible: true })
        {
            Visibility = Visibility.Visible;
        }
        else
        {
            Visibility = Visibility.Collapsed;
        }
    }
}