using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SimplePad.Editor;

namespace SimplePad.Menu;

public partial class UndoMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<UndoMenuItem, IAppTextBox?>(nameof(TextBox));

    static UndoMenuItem()
    {
        TextBoxProperty.Changed.AddClassHandler<UndoMenuItem>(OnTextBoxChanged);
    }

    public UndoMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTextBoxChanged(UndoMenuItem item, AvaloniaPropertyChangedEventArgs args)
    {
        IAppTextBox? oldTextBox = (IAppTextBox?)args.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.CanUndoChanged -= item.OnTextBoxCanUndoChanged;
        }

        IAppTextBox? newTextBox = (IAppTextBox?)args.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.CanUndoChanged += item.OnTextBoxCanUndoChanged;
        }

        item.UpdateIsEnabled();
    }

    private void OnClick(object? sender, RoutedEventArgs e)
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