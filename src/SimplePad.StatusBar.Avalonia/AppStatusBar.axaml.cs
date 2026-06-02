using System;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;

namespace SimplePad.StatusBar;

public sealed partial class AppStatusBar : UserControl
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty = AvaloniaProperty.Register<AppStatusBar, IAppTextBox?>(nameof(TextBox));
    private readonly EditorZoomState _editorZoomState;
    private readonly IStatusBarSettings _statusBarSettings;

    static AppStatusBar()
    {
        TextBoxProperty.Changed.AddClassHandler<AppStatusBar>(OnTextBoxChanged);
    }

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
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTextBoxChanged(AppStatusBar bar, AvaloniaPropertyChangedEventArgs args)
    {
        IAppTextBox? oldTextBox = (IAppTextBox?)args.OldValue;
        if (oldTextBox is not null)
        {
            oldTextBox.CursorPositionChanged -= bar.OnTextBoxCursorPositionChanged;
            oldTextBox.TextChanged -= bar.OnTextBoxTextChanged;
            oldTextBox.SelectionChanged -= bar.OnTextBoxSelectionChanged;
        }

        IAppTextBox? newTextBox = (IAppTextBox?)args.NewValue;
        if (newTextBox is not null)
        {
            newTextBox.CursorPositionChanged += bar.OnTextBoxCursorPositionChanged;
            newTextBox.TextChanged += bar.OnTextBoxTextChanged;
            newTextBox.SelectionChanged += bar.OnTextBoxSelectionChanged;
        }

        bar.UpdateCursorPositionIndicator();
        bar.UpdateCharacterIndicator();
    }

    private void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        UpdateZoomFactorIndicator();
    }

    private void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void OnTextBoxCursorPositionChanged(object? sender, CursorPosition e)
    {
        UpdateCursorPositionIndicator();
    }

    private void OnTextBoxSelectionChanged(object? sender, EventArgs e)
    {
        UpdateCharacterIndicator();
    }

    private void OnTextBoxTextChanged(object? sender, string e)
    {
        UpdateCharacterIndicator();
    }

    private void UpdateCharacterIndicator()
    {
        throw new NotImplementedException();
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

    private void UpdateVisibility()
    {
        IsVisible = _statusBarSettings.IsStatusBarVisible;
    }

    private void UpdateZoomFactorIndicator()
    {
        ZoomFactorIndicator.Text = $"{_editorZoomState.ZoomFactor:P0}";
    }
}