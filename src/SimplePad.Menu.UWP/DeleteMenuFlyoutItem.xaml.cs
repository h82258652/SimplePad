using System;
using SimplePad.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class DeleteMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(DeleteMenuFlyoutItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public DeleteMenuFlyoutItem()
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
        DeleteMenuFlyoutItem self = (DeleteMenuFlyoutItem)d;
        IAppTextBox? oldTextBox = (IAppTextBox?)e.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.SelectionChanged -= self.OnTextBoxSelectionChanged;
        }

        IAppTextBox? newTextBox = (IAppTextBox?)e.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.SelectionChanged += self.OnTextBoxSelectionChanged;
        }

        self.UpdateIsEnabled();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        if (TextBox is { SelectionLength: > 0 } textBox)
        {
            textBox.SelectedText = string.Empty;
            textBox.Focus();
        }
    }

    private void OnTextBoxSelectionChanged(object? sender, EventArgs e)
    {
        UpdateIsEnabled();
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = TextBox is { SelectionLength: > 0 };
    }
}