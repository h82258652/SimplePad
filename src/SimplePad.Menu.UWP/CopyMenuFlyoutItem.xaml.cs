using System;
using SimplePad.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class CopyMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(CopyMenuFlyoutItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public CopyMenuFlyoutItem()
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
        CopyMenuFlyoutItem self = (CopyMenuFlyoutItem)d;
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
        TextBox?.CopySelectionToClipboard();
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