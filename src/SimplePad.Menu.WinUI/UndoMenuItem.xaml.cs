using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class UndoMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(UndoMenuItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public UndoMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        UndoMenuItem self = (UndoMenuItem)d;
        IAppTextBox? oldTextBox = (IAppTextBox?)e.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.CanUndoChanged -= self.OnTextBoxCanUndoChanged;
        }

        IAppTextBox? newTextBox = (IAppTextBox?)e.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.CanUndoChanged += self.OnTextBoxCanUndoChanged;
        }

        self.UpdateIsEnabled();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        if (TextBox is { } textBox)
        {
            textBox.Undo();
            textBox.Focus();
        }
    }

    private void OnTextBoxCanUndoChanged(object? sender, bool e)
    {
        UpdateIsEnabled();
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = TextBox is { CanUndo: true };
    }
}