using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class RestoreDefaultZoomMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(RestoreDefaultZoomMenuItem),
        null);

    private readonly EditorZoomState _editorZoomState;

    public RestoreDefaultZoomMenuItem()
    {
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        _editorZoomState.ResetZoomFactor();
        await Task.Yield();
        TextBox?.Focus();
    }
}
