using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class AppMenuBar : UserControl
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(TextBox),
        typeof(AppMenuBar),
        null);

    public AppMenuBar()
    {
        InitializeComponent();
    }

    public TextBox? TextBox
    {
        get => (TextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnCopyClick(object sender, RoutedEventArgs e)
    {
        TextBox?.CopySelectionToClipboard();
    }

    private void OnCutClick(object sender, RoutedEventArgs e)
    {
        TextBox?.CutSelectionToClipboard();
    }

    private void OnPasteClick(object sender, RoutedEventArgs e)
    {
        TextBox?.PasteFromClipboard();
    }

    private void OnSaveAsClick(object sender, RoutedEventArgs e)
    {
        FileSavePicker fileSavePicker = new();

        // TODO
    }

    private void OnSaveClick(object sender, RoutedEventArgs e)
    {
    }
}
