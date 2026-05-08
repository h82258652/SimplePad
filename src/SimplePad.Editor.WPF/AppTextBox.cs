using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Fonts;

namespace SimplePad.Editor;

public sealed class AppTextBox : TextBox, IAppTextBox
{
    private readonly IEditorSettings _editorSettings;
    private readonly EditorZoomState _editorZoomState;
    private readonly IFontSettings _fontSettings;
    private readonly List<EventHandler?> _selectionChagnedHandler = [];
    private readonly List<EventHandler<string>?> _textChangedHandler = [];

    public AppTextBox()
    {
        _fontSettings = ServiceLocator.Current.GetRequiredService<IFontSettings>();
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

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

    public CursorPosition CursorPosition => throw new NotImplementedException();

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

    private void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        throw new NotImplementedException();
    }

    private void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        throw new NotImplementedException();
    }

    private void OnEditorZoomStateZoomFactorChanged(object? sender, double e)
    {
        throw new NotImplementedException();
    }

    private void OnFontSettingsFontFamilyChanged(object? sender, string e)
    {
        throw new NotImplementedException();
    }

    private void OnFontSettingsFontSizeChanged(object? sender, int e)
    {
        throw new NotImplementedException();
    }

    private void OnFontSettingsFontStyleChanged(object? sender, AppFontStyle e)
    {
        throw new NotImplementedException();
    }

    private void OnSelectionChanged(object sender, System.Windows.RoutedEventArgs e)
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
        throw new NotImplementedException();
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
        TextWrapping = _editorSettings.IsWordWrap ? System.Windows.TextWrapping.Wrap : System.Windows.TextWrapping.NoWrap;
    }

    private void UpdateZoomedFontSize()
    {
        throw new NotImplementedException();
    }
}