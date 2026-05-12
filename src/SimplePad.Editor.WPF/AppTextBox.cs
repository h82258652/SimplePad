using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Fonts;

namespace SimplePad.Editor;

public sealed class AppTextBox : TextBox, IAppTextBox
{
    public static readonly DependencyProperty CursorPositionProperty = DependencyProperty.Register(
        nameof(CursorPosition),
        typeof(CursorPosition),
        typeof(AppTextBox),
        new PropertyMetadata(null, OnCursorPositionChanged));

    private static readonly DependencyProperty ZoomedFontSizeProperty = DependencyProperty.Register(
        nameof(ZoomedFontSize),
        typeof(double),
        typeof(AppTextBox),
        new PropertyMetadata(14d));

    private readonly Dispatcher _dispatcher;
    private readonly IEditorSettings _editorSettings;
    private readonly EditorZoomState _editorZoomState;
    private readonly IFontSettings _fontSettings;
    private readonly List<EventHandler?> _selectionChagnedHandler = [];
    private readonly List<EventHandler<string>?> _textChangedHandler = [];

    static AppTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AppTextBox), new FrameworkPropertyMetadata(typeof(AppTextBox)));
    }

    public AppTextBox()
    {
        _dispatcher = Dispatcher;
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        CursorPosition = new CursorPosition(1, 1);

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

    private double ZoomedFontSize
    {
        get => (double)GetValue(ZoomedFontSizeProperty);
        set => SetValue(ZoomedFontSizeProperty, value);
    }

    public void CopySelectionToClipboard()
    {
        Copy();
    }

    public void CutSelectionToClipboard()
    {
        Cut();
    }

    void IAppTextBox.Focus()
    {
        _ = Focus();
    }

    public Task GoToLineAsync()
    {
        throw new NotImplementedException();
    }

    public void PasteFromClipboard()
    {
        Paste();
    }

    void IAppTextBox.Undo()
    {
        _ = Undo();
    }

    private static void OnCursorPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AppTextBox self = (AppTextBox)d;
        CursorPosition cursorPosition = (CursorPosition)e.NewValue;
        self.CursorPositionChanged?.Invoke(self, cursorPosition);
    }

    private void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateIsSpellCheckEnabled);
    }

    private void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateTextWrapping);
    }

    private void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        _dispatcher.Invoke(UpdateZoomedFontSize);
    }

    private void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        _dispatcher.Invoke(UpdateFontFamily);
    }

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        _dispatcher.Invoke(UpdateFontSize);
    }

    private void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        _dispatcher.Invoke(UpdateFontStyle);
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        foreach (EventHandler? handler in _selectionChagnedHandler)
        {
            handler?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        foreach (EventHandler<string>? handler in _textChangedHandler)
        {
            handler?.Invoke(this, Text);
        }
    }

    private void UpdateFontFamily()
    {
        FontFamily = new FontFamily(_fontSettings.FontFamily);
    }

    private void UpdateFontSize()
    {
        FontSize = _fontSettings.FontSize;
    }

    private void UpdateFontStyle()
    {
        FontStyle = _fontSettings.FontStyle.GetWPFFontStyle();
        FontWeight = _fontSettings.FontStyle.GetWPFFontWeight();
    }

    private void UpdateIsSpellCheckEnabled()
    {
        SpellCheck.IsEnabled = _editorSettings.IsSpellCheckEnabled;
    }

    private void UpdateTextWrapping()
    {
        TextWrapping = _editorSettings.IsWordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
    }

    private void UpdateZoomedFontSize()
    {
        ZoomedFontSize = FontSize * _editorZoomState.ZoomFactor;
    }
}