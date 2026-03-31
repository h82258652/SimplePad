using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Editor;
using SimplePad.StatusBar.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.StatusBar.UWP.Controls;

public sealed partial class AppStatusBar : UserControl
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnTextBoxChanged));

    private readonly EditorZoomState _editorZoomState;

    private readonly IStatusBarSettings _statusBarSettings;

    public AppStatusBar()
    {
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateVisibility();
        UpdateCursorPositionIndicator();
        UpdateCharacterIndicator();
        UpdateZoomFactorIndicator();

        _statusBarSettings.IsStatusBarVisibleChanged += OnStatusBarSettingsIsStatusBarVisibleChanged;
        _editorZoomState.ZoomFactorChanged += OnEditorZoomStateZoomFactorChanged;
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
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

    private async void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        await Dispatcher.SafeRunAsync(UpdateZoomFactorIndicator);
    }

    private async void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        await Dispatcher.SafeRunAsync(UpdateVisibility);
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
            CursorPositionText.Text = $"Ln {TextBox.CursorPosition.Row}, Col {TextBox.CursorPosition.Column}";
        }
    }

    private void UpdateVisibility()
    {
        Visibility = _statusBarSettings.IsStatusBarVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateZoomFactorIndicator()
    {
        ZoomFactorIndicator.Text = $"{_editorZoomState.ZoomFactor:P0}";
    }
}