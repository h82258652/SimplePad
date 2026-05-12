using System.Text;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using SimplePad.File;

namespace SimplePad.StatusBar;

public sealed partial class AppStatusBar : System.Windows.Controls.Primitives.StatusBar
{
    public static readonly DependencyProperty LineEndingsProperty = DependencyProperty.Register(
        nameof(LineEndings),
        typeof(LineEndings),
        typeof(AppStatusBar),
        new PropertyMetadata(LineEndings.CRLF, OnLineEndingsChanged));

    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnTextBoxChanged)
    );

    private readonly Dispatcher _dispatcher;
    private readonly EditorZoomState _editorZoomState;
    private readonly IStatusBarSettings _statusBarSettings;

    public AppStatusBar()
    {
        _dispatcher = Dispatcher;
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateVisibility();
        UpdateCursorPositionIndicator();
        UpdateCharacterIndicator();
        UpdateZoomFactorIndicator();
        UpdateLineEndingsNameText();

        _statusBarSettings.IsStatusBarVisibleChanged += OnStatusBarSettingsIsStatusBarVisibleChanged;
        _editorZoomState.ZoomFactorChanged += OnEditorZoomStateZoomFactorChanged;
    }

    public LineEndings LineEndings
    {
        get => (LineEndings)GetValue(LineEndingsProperty);
        set => SetValue(LineEndingsProperty, value);
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnLineEndingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppStatusBar self = (AppStatusBar)d;
        self.UpdateLineEndingsNameText();
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppStatusBar self = (AppStatusBar)d;
        IAppTextBox? oldTextBox = (IAppTextBox?)e.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.CursorPositionChanged -= self.OnTextBoxCursorPositionChanged;
            oldTextBox.TextChanged -= self.OnTextBoxTextChanged;
            oldTextBox.SelectionChanged -= self.OnTextBoxSelectionChanged;
        }

        IAppTextBox? newTextBox = (IAppTextBox?)e.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.CursorPositionChanged += self.OnTextBoxCursorPositionChanged;
            newTextBox.TextChanged += self.OnTextBoxTextChanged;
            newTextBox.SelectionChanged += self.OnTextBoxSelectionChanged;
        }

        self.UpdateCursorPositionIndicator();
        self.UpdateCharacterIndicator();
    }

    private void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        _dispatcher.Invoke(UpdateZoomFactorIndicator);
    }

    private void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateVisibility);
    }

    private void OnTextBoxCursorPositionChanged(object? sender, CursorPosition e)
    {
        UpdateCursorPositionIndicator();
    }

    private void OnTextBoxSelectionChanged(object? sender, System.EventArgs e)
    {
        UpdateCharacterIndicator();
    }

    private void OnTextBoxTextChanged(object? sender, string e)
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

    private void UpdateCursorPositionIndicator()
    {
        if (TextBox is null)
        {
            CursorPositionText.Text = string.Empty;
        }
        else
        {
            CursorPositionText.Text =
                $"Ln {TextBox.CursorPosition.Row}, Col {TextBox.CursorPosition.Column}";
        }
    }

    private void UpdateLineEndingsNameText()
    {
        LineEndingsNameText.Text = LineEndings.Name;
    }

    private void UpdateVisibility()
    {
        Visibility = _statusBarSettings.IsStatusBarVisible
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void UpdateZoomFactorIndicator()
    {
        ZoomFactorIndicator.Text = $"{_editorZoomState.ZoomFactor:P0}";
    }
}