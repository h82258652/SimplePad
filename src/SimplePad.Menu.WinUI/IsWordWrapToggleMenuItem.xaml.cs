using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class IsWordWrapToggleMenuItem : ToggleMenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(IsWordWrapToggleMenuItem),
        null);

    private readonly IEditorSettings _editorSettings;

    public IsWordWrapToggleMenuItem()
    {
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateIsChecked();

        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrap = IsChecked;
        TextBox?.Focus();
    }

    private void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        UpdateIsChecked();
    }

    private void UpdateIsChecked()
    {
        IsChecked = _editorSettings.IsWordWrap;
    }
}