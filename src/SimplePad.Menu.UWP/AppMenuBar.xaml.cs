using SimplePad.Editor;
using SimplePad.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class AppMenuBar : MenuBar
{
    public static readonly DependencyProperty SettingsStateProperty = DependencyProperty.Register(
        nameof(SettingsState),
        typeof(SettingsState),
        typeof(AppMenuBar),
        null);

    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(AppMenuBar),
        null);

    public AppMenuBar()
    {
        InitializeComponent();
    }

    public SettingsState? SettingsState
    {
        get => (SettingsState?)GetValue(SettingsStateProperty);
        set => SetValue(SettingsStateProperty, value);
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}