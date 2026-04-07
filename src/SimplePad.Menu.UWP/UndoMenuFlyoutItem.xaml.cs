using SimplePad.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class UndoMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(UndoMenuFlyoutItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public UndoMenuFlyoutItem()
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
        UndoMenuFlyoutItem self = (UndoMenuFlyoutItem)d;
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
        TextBox?.Undo();
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