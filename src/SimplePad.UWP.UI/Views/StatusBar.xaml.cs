using SimplePad.UWP.UI.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class StatusBar : UserControl
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(TextBox),
        typeof(StatusBar),
        new PropertyMetadata(null, OnTextBoxChanged));

    public StatusBar()
    {
        InitializeComponent();
    }

    public TextBox? TextBox
    {
        get => (TextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        StatusBar self = (StatusBar)d;

        TextBox? oldTextBox = (TextBox?)e.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.TextChanged -= self.OnTextBoxTextChanged;
            oldTextBox.SelectionChanged -= self.OnSelectionChanged;
        }

        TextBox? newTextBox = (TextBox?)e.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.TextChanged += self.OnTextBoxTextChanged;
            newTextBox.SelectionChanged += self.OnSelectionChanged;
        }

        self.UpdatePositionIndicator();
        self.UpdateCharacterIndicator();
        self.UpdateZoomFactorIndicator();
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        UpdatePositionIndicator();
        UpdateCharacterIndicator();
    }

    private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateCharacterIndicator();
    }

    private void UpdateCharacterIndicator()
    {
        if (TextBox is null)
        {
            CharacterIndicator.Text = string.Empty;
            return;
        }

        if (TextBox.SelectionLength > 0)
        {
            CharacterIndicator.Text = $"{TextBox.SelectionLength} of {TextBox.Text.Length} characters";
        }
        else
        {
            CharacterIndicator.Text = $"{TextBox.Text.Length} characters";
        }
    }

    private void UpdatePositionIndicator()
    {
        if (TextBox is null)
        {
            PositionIndicator.Text = string.Empty;
            return;
        }

        CursorPosition cursorPosition = TextBoxHelper.GetCursorPosition(TextBox);
        PositionIndicator.Text = $"Ln {cursorPosition.Row}, Col {cursorPosition.Column}";
    }

    private void UpdateZoomFactorIndicator()
    {
    }
}
