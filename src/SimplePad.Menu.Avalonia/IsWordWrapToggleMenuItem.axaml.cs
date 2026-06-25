using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;

namespace SimplePad.Menu;

public partial class IsWordWrapToggleMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<IsWordWrapToggleMenuItem, IAppTextBox?>(nameof(TextBox));

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
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnClick(object? sender, RoutedEventArgs e)
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