using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using SimplePad.Core;
using SimplePad.Fonts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.System;

namespace SimplePad.Editor;

public sealed partial class AppTextBox : TextBox, IAppTextBox
{
    public static readonly DependencyProperty CursorPositionProperty = DependencyProperty.Register(
        nameof(CursorPosition),
        typeof(CursorPosition),
        typeof(AppTextBox),
        PropertyMetadata.Create(() => new CursorPosition(1, 1), OnCursorPositionChanged));

    private readonly IEditorSettings _editorSettings;
    private readonly EditorZoomState _editorZoomState;
    private readonly IFontSettings _fontSettings;
    private readonly List<EventHandler?> _selectionChagnedHandler = [];
    private readonly List<EventHandler<string>?> _textChangedHandler = [];
    private bool _internalCanUndo;

    public AppTextBox()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        DefaultStyleKey = typeof(AppTextBox);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Editor.WinUI/AppTextBox.xaml");

        _internalCanUndo = CanUndo;
        UpdateFontFamily();
        UpdateFontStyle();
        UpdateFontSize();
        UpdateTextWrapping();
        UpdateIsSpellCheckEnabled();
        UpdateZoomedFontSize();

        _fontSettings.FontFamilyChanged += OnFontSettingsFontFamilyChanged;
        _fontSettings.FontStyleChanged += OnFontSettingsFontStyleChanged;
        _fontSettings.FontSizeChanged += OnFontSettingsFontSizeChanged;
        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
        _editorSettings.IsSpellCheckEnabledChanged += OnEditorSettingsIsSpellCheckEnabledChanged;
        _editorZoomState.ZoomFactorChanged += OnEditorZoomStateZoomFactorChanged;
        TextChanged += OnTextChanged;
        SelectionChanged += OnSelectionChanged;
        KeyDown += OnKeyDown;
        RegisterPropertyChangedCallback(FontSizeProperty, OnFontSizeChanged);
    }

    public event EventHandler<bool>? CanUndoChanged;

    public event EventHandler<CursorPosition>? CursorPositionChanged;

    event EventHandler? IAppTextBox.SelectionChanged
    {
        add => _selectionChagnedHandler.Add(value);
        remove => _selectionChagnedHandler.Remove(value);
    }

    event EventHandler<string>? IAppTextBox.TextChanged
    {
        add => _textChangedHandler.Add(value);
        remove => _textChangedHandler.Remove(value);
    }

    public CursorPosition CursorPosition
    {
        get => (CursorPosition)GetValue(CursorPositionProperty);
        private set => SetValue(CursorPositionProperty, value);
    }

    public void Focus()
    {
        Focus(FocusState.Programmatic);
    }

    public async Task GoToLineAsync()
    {
        string text = Text;
        int totalLines = text.Split('\r').Length;

        GoToLineDialog goToLineDialog = new(CursorPosition.Row, totalLines);
        ContentDialogResult dialogResult = await goToLineDialog.ShowAsync();
        if (dialogResult == ContentDialogResult.Primary)
        {
            int selectionStart = 0;
            int row = 1;

            for (int i = 0; i < text.Length && row < goToLineDialog.LineNumber; i++)
            {
                char c = text[i];
                selectionStart++;

                if (c == '\r')
                {
                    row++;
                }
            }

            SelectionStart = selectionStart;
        }
    }

    private static void OnCursorPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTextBox self = (AppTextBox)d;
        CursorPosition cursorPosition = (CursorPosition)e.NewValue;
        self.CursorPositionChanged?.Invoke(self, cursorPosition);
    }

    private void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        UpdateIsSpellCheckEnabled();
    }

    private void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        UpdateTextWrapping();
    }

    private void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        UpdateZoomedFontSize();
    }

    private void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        UpdateFontFamily();
    }

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        UpdateFontSize();
    }

    private void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        UpdateFontStyle();
    }

    private void OnFontSizeChanged(DependencyObject sender, DependencyProperty dp)
    {
        UpdateZoomedFontSize();
    }

    private void OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Tab)
        {
            e.Handled = true;

            string tabText = "\t";

            SelectedText = tabText;
            SelectionLength = 0;
            SelectionStart = SelectionStart + tabText.Length;
        }
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        UpdateCursorPosition();

        foreach (EventHandler? handler in _selectionChagnedHandler)
        {
            handler?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateInternalCanUndo();

        foreach (EventHandler<string>? handler in _textChangedHandler)
        {
            handler?.Invoke(this, Text);
        }
    }

    private void UpdateInternalCanUndo()
    {
        throw new NotImplementedException();
    }

    private void UpdateCursorPosition()
    {
        int endMarker = SelectionStart + SelectionLength;

        if (endMarker == 0)
        {
            CursorPosition = new CursorPosition(1, 1);
            return;
        }

        int i = 0;
        int col = 1;
        int row = 1;

        foreach (char c in Text)
        {
            i++;
            col++;

            if (c == '\r')
            {
                row++;
                col = 1;
            }

            if (i == endMarker)
            {
                CursorPosition = new CursorPosition(row, col);
                return;
            }
        }

        CursorPosition = new CursorPosition(row, col);
    }

    private void UpdateFontFamily()
    {
        throw new NotImplementedException();
    }

    private void UpdateFontSize()
    {
        throw new NotImplementedException();
    }

    private void UpdateFontStyle()
    {
        FontStyle = _fontSettings.FontStyle.GetWinUIFontStyle();
        FontWeight = _fontSettings.FontStyle.GetWinUIFontWeight();
    }

    private void UpdateIsSpellCheckEnabled()
    {
        IsSpellCheckEnabled = _editorSettings.IsSpellCheckEnabled;
    }

    private void UpdateTextWrapping()
    {
        TextWrapping = _editorSettings.IsWordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }

    private void UpdateZoomedFontSize()
    {
        throw new NotImplementedException();
    }
}