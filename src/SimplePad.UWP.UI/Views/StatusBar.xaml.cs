using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.UWP.UI.Helpers;
using SimplePad.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class StatusBar : UserControl
{
    public static readonly DependencyProperty EditorViewModelProperty = DependencyProperty.Register(
        nameof(EditorViewModel),
        typeof(EditorViewModel),
        typeof(StatusBar),
        null);

    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(TextBox),
        typeof(StatusBar),
        new PropertyMetadata(null, OnTextBoxChanged));

    private readonly AppState _appState;

    public StatusBar()
    {
        _appState = ServiceLocator.Current.GetRequiredService<AppState>();

        InitializeComponent();
    }

    public EditorViewModel? EditorViewModel
    {
        get => (EditorViewModel?)GetValue(EditorViewModelProperty);
        set => SetValue(EditorViewModelProperty, value);
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
    }

    private string GetEncodingName(Encoding encoding)
    {
        return encoding.EncodingName;
    }

    private string GetZoomFactorText(double zoomFactor)
    {
        return zoomFactor.ToString("P0");
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

        StringBuilder characterIndicatorTextBuilder = new();
        if (TextBox.SelectionLength > 0)
        {
            characterIndicatorTextBuilder.Append(TextBox.SelectionLength.ToString("N0"));
            characterIndicatorTextBuilder.Append(" of ");
        }

        int textLength = TextBox.Text.Length;
        characterIndicatorTextBuilder.Append(textLength.ToString("N0"));
        if (textLength == 1)
        {
            characterIndicatorTextBuilder.Append(" character");
        }
        else
        {
            characterIndicatorTextBuilder.Append(" characters");
        }

        CharacterIndicator.Text = characterIndicatorTextBuilder.ToString();
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
}
